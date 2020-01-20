using Framework.Domain;
using NUnit.Framework;
using Shifter.Managers.Domain.Models;
using System;
using System.Linq;
using Shifter.Persistence.Domain;

namespace Shifter.Managers.Domain.Tests.Services
{
    [TestFixture]
    public class DomainrepositoryTestFixture
    {
        [SetUp]
        public void Initialize()
        {
            var factory = new DatabaseFactory("Shifter.Managers.Domain");
            repository = new Repository(factory);
        }

        private IRepository repository;

        #region Load

        [Test]
        public void CanValidate()
        {
            var result = repository.Save(new Waiter());
            Assert.IsTrue(result.HasErrors());
        }

        [Test]
        public void CanLoadUserAccounts()
        {
            var result = repository.FindAll<UserAccount>();
            Assert.IsTrue(result.Any());
        }

        [Test]
        public void CanLoadWaiters()
        {
            var result = repository.FindAll<Waiter>();
            Assert.IsTrue(result.Any());
        }

        [Test]
        public void CanLoadManagers()
        {
            var result = repository.FindAll<Manager>();
            Assert.IsTrue(result.Any());
        }
        
        [Test]
        public void CanLoadRestaurants()
        {
            var result = repository.FindAll<Restaurant>();
            Assert.IsTrue(result.Any());
        } 
        
        [Test]
        public void CanLoadShifts()
        {
            var result = repository.FindAll<Shift>();
            Assert.IsTrue(result.Any());
        }
        
        [Test]
        public void CanLoadShiftTemplates()
        {
            var result = repository.FindAll<ShiftTemplate>();
            Assert.IsTrue(result.Any());
        }
        
        [Test]
        public void CanLoadShiftTimeslots()
        {
            var result = repository.FindAll<ShiftTimeslot>();
            Assert.IsTrue(result.Any());
        }

        [Test]
        public void CanLoadSettings()
        {
            var result = repository.FindAll<Settings>();
            Assert.IsTrue(result.Any());
        }

        #endregion

        #region Save

        [Test]
        public void CanSaveWaiter()
        {
            var waiter = Mocks.Waiter();
            waiter.FirstName = Guid.NewGuid().ToString();
            
            repository.Save(waiter);
            var saved = repository.FindBy<Waiter>(w => w.FirstName == waiter.FirstName).FirstOrDefault();
            Assert.IsNotNull(saved);

           var deleted = repository.Delete<Waiter>(saved.Id);
            Assert.IsFalse(deleted.HasErrors());
        }

        [Test]
        public void CanSaveTimeSlot()
        {
            var timeslot = Mocks.ShiftTimeSlot();
            timeslot.RestaurantId = 1;
            
            repository.Save(timeslot);
            var saved = repository.FindBy<ShiftTimeslot>(w => w.StartTime == timeslot.StartTime && w.EndTime == timeslot.EndTime).FirstOrDefault();
            Assert.IsNotNull(saved);

            var deleted = repository.Delete<ShiftTimeslot>(saved.Id);
            Assert.IsFalse(deleted.HasErrors());
        }

        [Test]
        public void CanSaveShift()
        {
            var shift = new Shift();
            shift.Restaurant = new Restaurant(){Id = 1};
            shift.Date = DateTime.Now.Date;
            
            repository.Save(shift);
            var saved = repository.FindBy<Shift>(w => w.Date == shift.Date.Date).FirstOrDefault();
            Assert.IsNotNull(saved);

            var deleted = repository.Delete<Shift>(saved.Id);
            Assert.IsFalse(deleted.HasErrors());
        }

        [Test]
        public void CanSaveShiftTemplate()
        {
            var restaurant = new Restaurant() {Id = 1};
            var shiftTemplate = Mocks.ShiftTemplate();
            shiftTemplate.RestaurantId = restaurant.Id;
            shiftTemplate.Id = 0;
            shiftTemplate.Timeslot = new ShiftTimeslot() {Id = 1};

            
            repository.Save(shiftTemplate);

            var saved = repository.FindBy<ShiftTemplate>(w => w.RestaurantId == restaurant.Id).FirstOrDefault();
            Assert.IsNotNull(saved);

            var deleted = repository.Delete<ShiftTemplate>(saved.Id);
            Assert.IsFalse(deleted.HasErrors());
        }

        [Test]
        public void CanSaveShiftTemplates()
        {
            var shiftTemplates = Mocks.ShiftTemplates();

            
            repository.Save(shiftTemplates);

            var saved = repository.FindBy<ShiftTemplate>(w => w.RestaurantId == shiftTemplates.First().RestaurantId);
            Assert.IsNotNull(saved);

            foreach (var shiftTemplate in saved)
            {
                var deleted = repository.Delete<ShiftTemplate>(shiftTemplate.Id);
                Assert.IsFalse(deleted.HasErrors());
            }
        }

        #endregion
    }
}
