using System;
using System.Collections.Generic;
using Framework.Notifications;
using Shifter.Domain.Model.Entities;

namespace Shifter.Domain.Services
{
    public interface IShiftDomainService
    {
        IEnumerable<Shift> LoadShifts(int restaurantId, int? timeslotId, DateTime? fromDate, DateTime? toDate, bool? isAssigned, bool? isAvailable, IEnumerable<int> staffTypeIds, int? staffId);

        IEnumerable<TrackedShiftAssignment> LoadShiftAssignments(int restaurantId, int waiterId);

        /// <summary>
        /// Loads the deleted shift notifications for a waiter and restaurant after the specified date
        /// </summary>
        IEnumerable<TrackedDeletedShift> LoadDeletedShiftNotifications(int restaurantId, int staffId, DateTime afterDate);

        Shift LoadShift(int shiftId);

        NotificationCollection SaveShift(Shift shift);

        NotificationCollection DeleteShift(int shiftId);

        NotificationCollection DeleteShifts(IEnumerable<Shift> shifts);

        NotificationCollection AssignShift(int staffId, int shiftId, string clientKey);

        NotificationCollection UpdateShiftAvailablity(int shiftId, bool makeAvailable, string clientKey);

        NotificationCollection SaveConfirmedShiftAssignments(IEnumerable<int> shiftAssignmentIds);

        NotificationCollection SaveConfirmedShiftDeleteNotification(IEnumerable<int> shiftAssignmentIds);

        NotificationCollection SaveShifts(IEnumerable<Shift> shifts);
    }
}
