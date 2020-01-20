using System;
using System.Collections.Generic;
using Shifter.Waiters.Domain.Models;

namespace Shifter.Waiters.Domain
{
    public class Mocks
    {
        public static IEnumerable<Shift> Shifts()
        {
            var shifts = new List<Shift>();

            var shift1 = new Shift()
            {
                Id = 22,
                Waiter = new Waiter(),
                StartTime = new TimeSpan(12, 30, 00),
                EndTime = new TimeSpan(19, 30, 00),
                Date = DateTime.Now,
                IsAvailable = true
            };


            shifts.Add(shift1);

            var names = new List<string> { "Ironman", "Spiderman", "Ironman", "Hulk", "Bob", "Batman", "Ironman", "Jimmy", "Spiderman", "Hulk", "Bob", "Batman", "Ironman", "Batman", "Ironman", "Batman", "Ironman", "Jimmy", "Hulk", "Ironman" };

            for (int i = 0; i < 20; i++)
            {
                var shift = new Shift()
                {
                    Id = 7,
                    Waiter = new Waiter(){Id = i},
                    StartTime = new TimeSpan(08, 30, 00),
                    EndTime = new TimeSpan(15, 30, 00),
                    Date = DateTime.Now.AddDays(i),
                    IsAvailable = false
                };
                shifts.Add(shift);
            }

            var shift2 = new Shift()
            {
                Id = 4,
                Waiter = new Waiter(),
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

        public static Models.Waiter Waiter()
        {
            return new Models.Waiter()
            {
                FirstName = "Bob"
            };
        }
        public static IEnumerable<Models.Waiter> Waiters()
        {
            var waiters = new List<Models.Waiter>();

            var w1 = new Models.Waiter()
            {
                FirstName = "Bob",
                Id = 3
            };
            var w2 = new Models.Waiter()
            {
                FirstName = "Irnoman",
                Id = 44
            };
            var w3 = new Models.Waiter()
            {
                FirstName = "Batman",
                Id = 8
            };
            waiters.Add(w1);
            waiters.Add(w2);
            return waiters;
        }
    }
}
