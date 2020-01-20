using System;
using Framework.Domain;
using Framework.Notifications;
using Framework.Notifications.Extensions;
using Framework.Rules;
using Shifter.Waiters.Domain.Models;
using System.Linq;

namespace Shifter.Waiters.Domain.Validation.Rules
{
    /// <summary>
    /// Waiter Rule: Ensure a waiter is not working more than one shift at the same time
    /// </summary>
    [Rule(typeof(Shift))]
    [RuleContext(ValidationContextKeys.Save)]
    public class SR001 : IRule
    {
        public NotificationCollection Validate(object entity, IRepository repository)
        {
            var notifications = NotificationCollection.CreateEmpty();

            if (entity.IsNotNull())
            {
                var shift = entity as Shift;

                if (shift.Waiter.IsNotNull())
                {
                    var existingShift = GetShiftsByWaiterRestaurantDateAndTime(repository, shift);

                    //are there any other shifts assigned to the same waiter, at the same restaurant, on the same date, at the same time
                    if (existingShift.IsNotNull() && existingShift.Id != shift.Id)
                    {
                        notifications.AddError("You are already working a shift at this time.");
                    }

                    //how many other shifts does the waiter have in the same week as the current shift
                    int numShifts =
                        shift.Waiter.Shifts.Count(
                            s =>
                                s.Id != shift.Id  //not this shift in case we're saving an edit of an existing shift
                                && s.Date.CompareTo(shift.Date.StartOfWeek()) >= 0 //on or after start of week
                                && s.Date.CompareTo(shift.Date.StartOfWeek().AddDays(6)) < 0); //before end of week
                    if (numShifts >= shift.Waiter.MaxNumberOfShiftsPerWeek)
                    {
                        notifications.AddError("You cannot work more than " + shift.Waiter.MaxNumberOfShiftsPerWeek +
                                               " shifts per week.");
                    }
                }
            }

            return notifications;
        }

        private Shift GetShiftsByWaiterRestaurantDateAndTime(IRepository repository, Shift shift)
        {
            return repository.FindBy<Shift>(s => s.RestaurantId == shift.RestaurantId && s.Waiter.Id == shift.Waiter.Id && s.Date.Date == shift.Date.Date && s.StartTime == shift.StartTime).FirstOrDefault();
        }
    }

    /// <summary>
    /// Shift Rule: Ensure a shift has a start time and end time
    /// </summary>
    [Rule(typeof(Shift))]
    [RuleContext(ValidationContextKeys.Save)]
    public class SR002 : IRule
    {
        public NotificationCollection Validate(object entity, IRepository repository)
        {
            var notifications = NotificationCollection.CreateEmpty();

            if (entity.IsNotNull())
            {
                var shift = entity as Shift;

                if (shift.StartTime.IsNull() || shift.EndTime.IsNull())
                {
                    notifications.AddError("A shift must have a start and end time.");
                }
            }

            return notifications;
        }
    }
}