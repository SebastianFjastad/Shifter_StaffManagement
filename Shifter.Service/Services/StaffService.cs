using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Framework;
using Framework.Notifications;
using Framework.Notifications.Extensions;
using Shifter.Domain.Model.Entities;
using Shifter.Domain.Services;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;
using Shifter.Service.Api.Responses;
using Shifter.Service.Api.Services;
using Shifter.Service.Utilities;

namespace Shifter.Service.Services
{
    public class StaffService : ShifterServiceBase, IStaffService
    {
        private readonly IStaffDomainService staffDomainService;
        private readonly IShiftDomainService shiftDomainService;
        private readonly IRestaurantDomainService restaurantDomainService;

        public StaffService(IStaffDomainService staffDomainService, IShiftDomainService shiftDomainService, IRestaurantDomainService restaurantDomainService)
        {
            Guard.ArgumentNotNull(staffDomainService, "staffDomainService");
            Guard.ArgumentNotNull(shiftDomainService, "shiftDomainService");
            Guard.ArgumentNotNull(restaurantDomainService, "restaurantDomainService");

            this.staffDomainService = staffDomainService;
            this.shiftDomainService = shiftDomainService;
            this.restaurantDomainService = restaurantDomainService;
        }

        public StaffCollectionResponse LoadStaffList(LoadStaffListRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var result = new StaffCollectionResponse();

                var staff = staffDomainService.LoadStaff(request.RestaurantId, request.StaffTypeIds);
                result.Staff = staff.AsStaffDtos();

                var staffTypes = staffDomainService.LoadStaffTypes();
                result.StaffTypes = staffTypes.AsStaffTypeDtos();

                if (request.IncludeShiftsFrom.IsNotNull() || request.IncludeShiftsTo.IsNotNull())
                {
                    var staffWithShifts = new List<StaffDto>();

                    var shifts = shiftDomainService.LoadShifts(request.RestaurantId, null, request.IncludeShiftsFrom, request.IncludeShiftsTo, null, null, null, null).Select(s => s.AsShiftDto());

                    foreach (var staffDto in result.Staff)
                    {
                        staffDto.Shifts = shifts.Where(s => s.Staff != null && s.Staff.Id == staffDto.Id).ToList();
                        staffWithShifts.Add(staffDto);
                    }
                    result.Staff = staffWithShifts;
                }

                return result;
            });
        }

        public StaffTypeCollectionResponse LoadStaffTypes()
        {
            return TryExecute(() =>
            {
                var result = new StaffTypeCollectionResponse();

                var staffTypes = staffDomainService.LoadStaffTypes();
                result.StaffTypes = staffTypes.AsStaffTypeDtos();

                return result;
            });
        }

        public StaffHoursResponse LoadStaffHours(LoadStaffHoursRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");
                var staffHourses = new List<StaffHours>();

                var staffDtos = staffDomainService.LoadStaff(request.RestaurantId, request.StaffTypeIds).AsStaffDtos();
                var shifts = shiftDomainService.LoadShifts(request.RestaurantId, null, request.FromDate, request.ToDate, null, null, request.StaffTypeIds, null).ToList();

                foreach (var staffDto in staffDtos)
                {
                    var staffShifts = shifts.Where(s => s.Staff != null && s.Staff.Id == staffDto.Id).ToList();
                    var hoursWorked = new TimeSpan(0, 0, 0);

                    foreach (var staffShift in staffShifts)
                    {
                        var duration = staffShift.EndTime.Subtract(staffShift.StartTime);
                        hoursWorked += duration;
                    }

                    staffHourses.Add(new StaffHours() { FirstName = staffDto.FirstName, LastName = staffDto.LastName, Id = staffDto.Id, HoursWorked = hoursWorked, NoOfShifts = staffShifts.Count()});
                }

                return new StaffHoursResponse() { StaffHoursCollection = staffHourses };
            });
        }

        public StaffResponse LoadStaffMember(LoadStaffMemberRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var staff = staffDomainService.LoadStaffMember(request.StaffId);

                Guard.InstanceNotNull(staff, "waiter");

                var response = staff.AsStaffResponse();

                if (response.Staff.IsNotNull() && request.RestaurantId.IsNotNull() && (request.IncludeShiftsFrom.IsNotNull() || request.IncludeShiftsTo.IsNotNull()))
                {
                    var shifts = shiftDomainService.LoadShifts(request.RestaurantId.Value, null, request.IncludeShiftsFrom, request.IncludeShiftsTo, null, null, null, staff.Id).Select(s => s.AsShiftDto());

                    response.Staff.Shifts = shifts.ToList();
                }

                return response;
            });
        }

        public GenericServiceResponse SaveStaff(SaveWaiterRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var staff = request.Staff.AsDomainModel(true);
                var restaurant = restaurantDomainService.LoadRestaurant(request.RestaurantId);

                //TODO This needs to change if the system is to cater for staff working at multiple restaurants
                staff.Restaurants = new List<Restaurant>(){restaurant};

                if (!request.StaffMemberHasEmailAddress && staff.IsTransient())
                {
                    staff.UserAccount.Username = staffDomainService.GenerateUsername(staff.UserAccount.FirstName, staff.UserAccount.LastName, restaurant.Name);
                }

                var notificationCollection = staffDomainService.SaveStaff(staff, staff.UserAccount, request.StaffMemberHasEmailAddress);

                return new GenericServiceResponse
                {
                    NotificationCollection = notificationCollection
                };
            });
        }

        public GenericServiceResponse DeleteStaff(GenericStaffRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var notificationCollection = staffDomainService.DeleteStaff(request.StaffId);

                var result = new GenericServiceResponse
                {
                    NotificationCollection = notificationCollection
                };

                return result;
            });
        }

        public GenericServiceResponse ResetPassword(GenericStaffRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var notificationCollection = staffDomainService.ResetPassword(request.StaffId);

                return new GenericServiceResponse
                {
                    NotificationCollection = notificationCollection
                };
            });
        }

        public GenericServiceResponse LoadStaffNotifications(LoadByStaffAndRestaurantRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var result = NotificationCollection.CreateEmpty();

                result += GetAssignmentNotifications(request);
                result += GetDeleteNotifications(request);

                return new GenericServiceResponse() { NotificationCollection = result };
            });
        }

        public GenericServiceResponse ConfirmStaffReceivedNotifications(ConfirmNotificationReceivedRequest request)
        {
            return TryExecute(() =>
            {
                var result = NotificationCollection.CreateEmpty();

                var assignNotificationIds = request.NotificationIds.Where(s => s.Value == SharedConstants.AssignNotification).Select(s => s.Key).ToList();
                var deleteNotificationIds = request.NotificationIds.Where(s => s.Value == SharedConstants.DeleteNotification).Select(s => s.Key).ToList();

                result += shiftDomainService.SaveConfirmedShiftAssignments(assignNotificationIds);

                result += shiftDomainService.SaveConfirmedShiftDeleteNotification(deleteNotificationIds);

                return new GenericServiceResponse() { NotificationCollection = result };
            });
        }

        public GenericServiceResponse SaveUnavailability(SaveUnavailabilityRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var unavailabilityRecords = request.AsStaffUnavailabilityRecords().ToList();

                var result = staffDomainService.SaveStaffUnavailability(unavailabilityRecords);

                return new GenericServiceResponse() { NotificationCollection = result };
            });
        }

        public GenericServiceResponse DeleteUnavailability(DeleteUnavailabilityRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var result = staffDomainService.DeleteStaffUnavailability(request.UnavailabilityIds);

                return new GenericServiceResponse() { NotificationCollection = result };
            });
        }

        public GenericServiceResponse UpdateLastActiveDate(UpdateLastActiveDateRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var staff = staffDomainService.LoadStaffMember(request.StaffMemberId);
                staff.DateLastActive = request.Date;

                var notificationCollection = staffDomainService.SaveStaff(staff, staff.UserAccount);

                return new GenericServiceResponse
                {
                    NotificationCollection = notificationCollection
                };
            });
        }

        #region Private methods

        private NotificationCollection GetAssignmentNotifications(LoadByStaffAndRestaurantRequest request)
        {
            var result = NotificationCollection.CreateEmpty();

            var assignments = shiftDomainService.LoadShiftAssignments(request.RestaurantId, request.StaffId);

            if (assignments.Any())
            {
                foreach (var assignment in assignments)
                {
                    var shift = TryLoadShift(assignment);
                    if (shift.IsNotNull() && shift.EndTime > DateTime.Now)
                    {
                        if (shift.Staff.IsNull() || shift.Staff.Id != assignment.StaffId)
                        {
                            var msg = string.Format("The shift on {0} at {1} is no longer assigned to you.", shift.StartTime.ToString(SharedConstants.DateFormat), shift.StartTime.ToString(SharedConstants.DateTimeSpecificTimeFormat));
                            result.AddMessage(new Notification(msg, NotificationSeverity.Information, assignment.Id, SharedConstants.AssignNotification));
                        }
                        else if (shift.Staff.IsNotNull() && shift.Staff.Id == assignment.StaffId)
                        {
                            var msg = string.Format("The shift on {0} at {1} is now assigned to you.", shift.StartTime.ToString(SharedConstants.DateFormat), shift.StartTime.ToString(SharedConstants.DateTimeSpecificTimeFormat));
                            result.AddMessage(new Notification(msg, NotificationSeverity.Information, assignment.Id, SharedConstants.AssignNotification));
                        }
                    }
                }
            }

            return result;
        }

        private NotificationCollection GetDeleteNotifications(LoadByStaffAndRestaurantRequest request)
        {
            var result = NotificationCollection.CreateEmpty();

            var deletedShiftNotifications = shiftDomainService.LoadDeletedShiftNotifications(request.RestaurantId, request.StaffId, DateTime.Now);

            if (deletedShiftNotifications.Any())
            {
                foreach (var notification in deletedShiftNotifications)
                {
                    var msg = string.Format("The shift assigned to you for {0} at {1} has been deleted.", notification.ShiftStartTime.ToString(SharedConstants.DateFormat), notification.ShiftStartTime.ToString(SharedConstants.DateTimeSpecificTimeFormat));
                    result.AddMessage(new Notification(msg, NotificationSeverity.Information, notification.Id, SharedConstants.DeleteNotification));
                }
            }

            return result;
        }

        private Shift TryLoadShift(TrackedShiftAssignment assignment)
        {
            try
            {
                return shiftDomainService.LoadShift(assignment.ShiftId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion
    }
}