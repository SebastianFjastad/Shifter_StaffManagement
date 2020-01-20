using Framework;
using Framework.Domain;
using Framework.Notifications;
using Framework.Notifications.Extensions;
using Framework.PubSub;
using Framework.PubSub.Messages;
using LinqKit;
using Shifter.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Shifter.Domain.Services
{
    public class ShiftDomainService : IShiftDomainService
    {
        #region Constructors

        public ShiftDomainService(IRepository repository, IPublisher publisher)
        {
            this.repository = repository;
            this.publisher = publisher;
        }

        #endregion

        #region Locals

        private readonly IRepository repository;
        private readonly IPublisher publisher;

        #endregion

        #region Implementation of IShiftService

        public IEnumerable<Shift> LoadShifts(int restaurantId, int? timeslotId, DateTime? fromDate, DateTime? toDate, bool? isAssigned, bool? isAvailable, IEnumerable<int> staffTypeIds, int? staffId)
        {
            var predicate = BuildPredicate(restaurantId, timeslotId, fromDate, toDate, isAssigned, isAvailable, staffTypeIds, staffId);

            var result = repository.FindBy(predicate);

            if (staffTypeIds.IsNotNull() && staffTypeIds.Any())
            {
                result = result.Where(s => staffTypeIds.Any(id => id == s.Staff.StaffType.Id)).ToList();
            }

            return result;
        }

        public IEnumerable<TrackedShiftAssignment> LoadShiftAssignments(int restaurantId, int waiterId)
        {
            return repository.FindBy<TrackedShiftAssignment>(s => s.RestaurantId == restaurantId && s.StaffId == waiterId && s.DateRecieved == null && s.IsStillValid);
        }

        public IEnumerable<TrackedDeletedShift> LoadDeletedShiftNotifications(int restaurantId, int staffId, DateTime afterDate)
        {
            return repository.FindBy<TrackedDeletedShift>(s => s.RestaurantId == restaurantId && s.StaffId == staffId && s.DateStaffConfirmed == null && s.ShiftEndTime > afterDate);
        }

        public Shift LoadShift(int shiftId)
        {
            return repository.FindById<Shift>(shiftId);
        }

        public NotificationCollection SaveShift(Shift shift)
        {
            Guard.ArgumentNotNull(shift, "shift");

            var waiterIds = new List<int>();
            //Existing shift may already be assigned
            if (!shift.IsTransient())
            {
                var existingShift = LoadShift(shift.Id);
                if (existingShift.IsNotNull() && existingShift.Staff.IsNotNull())
                {
                    waiterIds.Add(existingShift.Staff.Id);
                }
            }
            //The shift being saved (new or not) may be assigned
            if (shift.Staff.IsNotNull())
            {
                waiterIds.Add(shift.Staff.Id);
            }

            var result = repository.Save(shift);

            if (!result.HasErrors())
            {
                publisher.Publish(MessageFactory.CreateAppMessage(shift.Id, MessageTopics.ShiftScheduleChanged, string.Empty));

                UpdateShiftAssignmentTracking(waiterIds, shift);
            }

            return result;
        }

        public NotificationCollection DeleteShift(int shiftId)
        {
            var existingShift = LoadShift(shiftId);

            existingShift.IsCancelled = true;
            var result = repository.Save(existingShift);

            if (!result.HasErrors())
            {
                publisher.Publish(MessageFactory.CreateAppMessage(shiftId, MessageTopics.ShiftScheduleChanged, string.Empty));

                UpdateShiftTrackingForDelete(existingShift);
            }

            return result;
        }

        public NotificationCollection DeleteShifts(IEnumerable<Shift> shifts)
        {
            foreach (var shift in shifts)
            {
                shift.IsCancelled = true;
            }

            var result = repository.Save(shifts);

            return result;
        }

        public NotificationCollection AssignShift(int staffId, int shiftId, string clientKey)
        {
            var notifications = NotificationCollection.CreateEmpty();

            var shift = repository.FindById<Shift>(shiftId);

            var waiterIds = new List<int>();
            waiterIds.Add(staffId);

            if (shift.IsNotNull() && shift.Staff.IsNotNull())
            {
                waiterIds.Add(shift.Staff.Id);
            }

            var toWaiter = repository.FindById<Staff>(staffId);

            if (shift.IsNull())
            {
                notifications.AddError("Shift does not exist.");
            }

            if (toWaiter.IsNull())
            {
                notifications.AddError("Waiter does not exist.");
            }

            if (!notifications.HasErrors())
            {
                shift.Staff = toWaiter;
                shift.IsAvailable = false;

                notifications += repository.Save(shift);

                //TODO: Remove static factory
                publisher.Publish(MessageFactory.CreateAppMessage(shift.Id, MessageTopics.ShiftScheduleChanged, clientKey));
                UpdateShiftAssignmentTracking(waiterIds, shift, clientKey);
            }

            return notifications;
        }

        public NotificationCollection UpdateShiftAvailablity(int shiftId, bool makeAvailable, string clientKey)
        {
            var notifications = NotificationCollection.CreateEmpty();

            var shift = repository.FindById<Shift>(shiftId);

            if (shift.IsNull())
            {
                notifications.AddError("Shift does not exist.");
            }

            if (!notifications.HasErrors())
            {
                shift.IsAvailable = makeAvailable;

                notifications += repository.Save(shift);

                //TODO: Remove static factory
                publisher.Publish(MessageFactory.CreateAppMessage(shift.Id, MessageTopics.ShiftScheduleChanged, clientKey));

            }

            return notifications;
        }

        public NotificationCollection SaveConfirmedShiftAssignments(IEnumerable<int> shiftAssignmentIds)
        {
            var result = NotificationCollection.CreateEmpty();

            var shiftAssignments = new List<TrackedShiftAssignment>();
            foreach (var shiftAssignmentId in shiftAssignmentIds)
            {
                var shiftAssignment = repository.FindById<TrackedShiftAssignment>(shiftAssignmentId);
                if (shiftAssignment.IsNotNull())
                {
                    shiftAssignment.DateRecieved = DateTime.Now;

                    shiftAssignments.Add(shiftAssignment);
                }
            }
            result = repository.Save(shiftAssignments);

            return result;
        }

        public NotificationCollection SaveConfirmedShiftDeleteNotification(IEnumerable<int> deleteNotificationIds)
        {
            var result = NotificationCollection.CreateEmpty();

            var deleteNotifications = new List<TrackedDeletedShift>();
            foreach (var notificationId in deleteNotificationIds)
            {
                var shiftAssignment = repository.FindById<TrackedDeletedShift>(notificationId);
                if (shiftAssignment.IsNotNull())
                {
                    shiftAssignment.DateStaffConfirmed = DateTime.Now;

                    deleteNotifications.Add(shiftAssignment);
                }
            }
            result = repository.Save(deleteNotifications);

            return result;
        }

        public NotificationCollection SaveShifts(IEnumerable<Shift> shifts)
        {
            var result = repository.Save(shifts);

            return result;
        }

        #endregion

        #region Private Methods

        private void UpdateShiftTrackingForDelete(Shift existingShift)
        {
            //Step 1: Set relevant shift assignment notifications to no longer valid
            var trackedAssignments = repository.FindBy<TrackedShiftAssignment>(s => s.ShiftId == existingShift.Id);
            var assignmentsToUpdate = new List<TrackedShiftAssignment>();

            foreach (var shiftAssignment in trackedAssignments)
            {
                shiftAssignment.IsStillValid = false;
                assignmentsToUpdate.Add(shiftAssignment);
            }

            repository.Save(assignmentsToUpdate);

            //Step 2: Save a record for the deleted shift tracking and publish the message
            var deletedShift = new TrackedDeletedShift
            {
                DateDeleted = DateTime.Now,
                RestaurantId = existingShift.Restaurant.Id,
                ShiftEndTime = existingShift.EndTime,
                ShiftStartTime = existingShift.StartTime,
                ShiftId = existingShift.Id,
                StaffId = existingShift.Staff.IsNotNull() ? existingShift.Staff.Id : (int?)null,
            };

            repository.Save(deletedShift);

            publisher.Publish(MessageFactory.CreateAppMessage(string.Empty, MessageTopics.ShiftDeleted, string.Empty));
        }

        private Expression<Func<Shift, bool>> BuildPredicate(int restaurantId, int? timeslotId, DateTime? fromDate, DateTime? toDate, bool? isAssigned, bool? isAvailable, IEnumerable<int> staffTypeIds, int? staffId)
        {
            var predicate = PredicateBuilder.True<Shift>();
            predicate = predicate.And(s => s.Restaurant.Id == restaurantId && s.IsCancelled == false);

            if (timeslotId.HasValue)
            {
                var timeSlot = repository.FindById<ShiftTimeslot>(timeslotId.Value);
                predicate = predicate.And(s => s.StartTime.TimeOfDay == timeSlot.StartTime && s.EndTime.TimeOfDay == timeSlot.EndTime);
            }

            if (fromDate.HasValue)
            {
                predicate = predicate.And(s => s.StartTime.Date >= fromDate);
            }

            if (toDate.HasValue)
            {
                predicate = predicate.And(s => s.StartTime.Date <= toDate);
            }

            if (isAssigned.HasValue)
            {
                predicate = isAssigned.Value ? predicate.And(s => s.Staff != null) : predicate.And(s => s.Staff == null);
            }

            if (isAvailable.HasValue)
            {
                predicate = predicate.And(s => s.IsAvailable == isAvailable.Value);
            }

            if (staffId.HasValue)
            {
                predicate = predicate.And(s => s.Staff.Id == staffId);
            }

            return predicate;
        }
        
        private NotificationCollection UpdateShiftAssignmentTracking(IEnumerable<int> waiterIds, Shift shift, string clientKey = "")
        {
            Guard.ArgumentNotNull(shift, "shift");

            var result = NotificationCollection.CreateEmpty();

            var shiftAssignments = new List<TrackedShiftAssignment>();

            if (waiterIds.Any() && shift.Restaurant.IsNotNull())
            {
                foreach (var waiterId in waiterIds)
                {
                    result += SetExistingAssignmentsAsNoLongerValid(shift.Id, waiterId);

                    var shiftAssignment = new TrackedShiftAssignment()
                    {
                        ShiftId = shift.Id,
                        RestaurantId = shift.Restaurant.Id,
                        StaffId = waiterId,
                        DateAssigned = DateTime.Now,
                        IsStillValid = true
                    };

                    shiftAssignments.Add(shiftAssignment);
                }

                result += repository.Save(shiftAssignments);

                if (!result.HasErrors())
                {
                    foreach (var shiftAssignment in shiftAssignments)
                    {
                        publisher.Publish(MessageFactory.CreateAppMessage(shiftAssignment.StaffId, MessageTopics.ShiftAssigned, clientKey));
                    }
                }
            }

            return result;
        }

        private NotificationCollection SetExistingAssignmentsAsNoLongerValid(int shiftId, int waiterId)
        {
            var assignments = new List<TrackedShiftAssignment>();
            var outdatedAssignments = repository.FindBy<TrackedShiftAssignment>(t => t.ShiftId == shiftId && t.StaffId == waiterId);
            foreach (var assignment in outdatedAssignments)
            {
                assignment.IsStillValid = false;
                assignments.Add(assignment);
            }

            return repository.Save(assignments);
        }

        #endregion
    }
}
