using System;
using System.Collections.Generic;
using System.Linq;
using Shifter.Managers.Domain.Models;

namespace Shifter.Managers.Domain
{
    public static class Mocks
    {
        public static IEnumerable<Shift> Shifts()
        {
            var shifts = new List<Shift>();

            var shift1 = new Shift()
            {
                Waiter = new Waiter(){Id = 1},
                Id = 22,
                StartTime = new TimeSpan(12, 30, 00),
                EndTime = new TimeSpan(19, 30, 00),
                Date = DateTime.Now,
                IsAvailable = true
            };
            

            shifts.Add(shift1);

            var names = new List<string> { "Ironman", "Spiderman", "Ironman", "Hulk", "Bob", "Batman", "Ironman", "Jimmy", "Spiderman", "Hulk", "Bob", "Batman", "Ironman", "Batman", "Ironman", "Batman", "Ironman", "Jimmy", "Hulk", "Ironman" }; 

            for (int i = 0; i < 20; i++)
            {
                var waiter = new Waiter();
                waiter.FirstName = names[i];

                var shift = new Shift()
                {
                    Waiter = waiter,
                    Id = 1,
                    StartTime = new TimeSpan(08, 30, 00),
                    EndTime = new TimeSpan(15, 30, 00),
                    Date = DateTime.Now.AddDays(i),
                    IsAvailable = false
                };
                shifts.Add(shift);
            }

            var waiter1 = new Waiter();
            waiter1.FirstName = "Spiderman";

            var shift2 = new Shift()
            {

                Waiter = waiter1,
                Id = 44,
                StartTime = new TimeSpan(07, 30, 00),
                EndTime = new TimeSpan(15, 00, 00),
                Date = DateTime.Now,
                IsAvailable = true
            };

            shifts.Add(shift2);

            return shifts;
        }
        public static Shift Shift()
        {
            return new Shift()
            {
                Id = 1,
            };
        }

        public static Waiter Waiter()
        {
            return new Waiter()
            {
                FirstName = "Bob",
                UserAccount = new UserAccount(){Id = 1, Username = "Name", Password = "a"},
                LastName = "Miller",
                EmailAddress = "fake@fake.com",
                CanSwapShifts = true,
                CanWorkDoubles = false,
                ContactNumber = "0712840090",
                MaxNumberOfShiftsPerWeek = 4,
                Restaurants = new List<Restaurant>(){new Restaurant(){Id = 1}},
                //Shifts = Shifts().ToList(),
            };
        }
        public static IEnumerable<Waiter> Waiters()
        {
            var waiters = new List<Waiter>();

            var w1 = new Waiter()
            {
                FirstName = "Bob",
                LastName = "Miller",
                EmailAddress = "fake@fake.com",
                CanSwapShifts = true,
                CanWorkDoubles = false,
                ContactNumber = "0712840090",
                Id = 1,
                MaxNumberOfShiftsPerWeek = 4,
                Restaurants = new List<Restaurant>(),
                Shifts = Shifts().ToList(),
            };
            var w2 = new Waiter()
            {
                FirstName = "Irnoman",
                EmailAddress = "fake@fake.com",
                CanSwapShifts = true,
                CanWorkDoubles = false,
                ContactNumber = "0712840090",
                Id = 2,
                MaxNumberOfShiftsPerWeek = 4,
                Restaurants = new List<Restaurant>(),
                Shifts = Shifts().ToList(),
            };
            waiters.Add(w1);
            waiters.Add(w2);
            return waiters;
        }

        public static IEnumerable<ShiftTemplate> ShiftTemplates()
        {
            var shiftTemplates = new List<ShiftTemplate>();

            var st1 = new ShiftTemplate()
            {
                Id = 0,
                DayOfWeek = DayOfWeek.Monday,
                RestaurantId = 2,
                Timeslot = new ShiftTimeslot() { Id = 1 }
            };
            var st2 = new ShiftTemplate()
            {
                Id = 0,
                DayOfWeek = DayOfWeek.Monday,
                RestaurantId = 2,
                Timeslot = new ShiftTimeslot() { Id = 1 }
            };

            shiftTemplates.Add(st1);
            shiftTemplates.Add(st2);

            return shiftTemplates;
        }
        public static ShiftTemplate ShiftTemplate()
        {
            return new ShiftTemplate()
            {
                Id = 1,
                DayOfWeek = DayOfWeek.Monday,
                NumberOfWaitersNeeded = 1
            };
        }

        public static Restaurant Restaurant()
        {
            return new Restaurant()
            {
                Id = 1,
                Name = "Name",
                Waiters = Waiters().ToList(),
                Address = "11, abc",
                ShiftTemplates = ShiftTemplates()
            };
        }

        public static IEnumerable<Restaurant> Restaurants()
        {
            var r1 = Restaurant();

            var r2 = new Restaurant()
            {
                Id = 2,
            };

            return new List<Restaurant>(){r1,r2};
        }

        public static IEnumerable<ShiftTimeslot> ShiftTimeSlots()
        {
            var shiftTimeSlots = new List<ShiftTimeslot>();

            var st1 = new ShiftTimeslot()
            {
                Id = 1,
                StartTime = new TimeSpan(9, 30, 0),
                EndTime = new TimeSpan(12, 30, 0)
            };
            var st2 = new ShiftTimeslot()
            {
                Id = 2,
                StartTime = new TimeSpan(6, 30, 0),
                EndTime = new TimeSpan(10, 30, 0)
            };

            shiftTimeSlots.Add(st1);
            shiftTimeSlots.Add(st2);

            return shiftTimeSlots;
        }
        public static ShiftTimeslot ShiftTimeSlot()
        {
            return new ShiftTimeslot()
            {
                StartTime = new TimeSpan(8,31,0),
                EndTime = new TimeSpan(12,30,0)
            };
        }

        public static Manager Manager()
        {
            return new Manager()
                       {
                           Id = 1,
                           ContactNumber = "011",
                           EmailAddress = "abc@fake.com",
                           FirstName = "name",
                           LastName = "lastname",
                           Restaurants = Restaurants().ToList(),
                           UserAccount = new UserAccount(){Password = "a", Username = "name"}
                       };
        }
    }
}
