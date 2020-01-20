using System.Linq;
using Framework.Domain;
using NUnit.Framework;
using System;
using Shifter.Persistence.Domain;
using Shifter.Waiters.Domain.Models;

namespace Shifter.Waiters.Domain.Tests.Services
{
    [TestFixture]
    public class DomainrepositoryTestFixture
    {
        [SetUp]
        public void Initialize()
        {
            var factory = new DatabaseFactory("Shifter.Waiters.Domain");
            repository = new Repository(factory);
        }

        private IRepository repository;

        #region Load

        [Test]
        public void CanValidate()
        {
            var result = repository.Save(new Shift());
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
        
        #endregion

        #region Save

        [Test]
        public void CanSaveShift()
        {
            var shift = new Shift();
            shift.RestaurantId = 1;
            shift.Date = DateTime.Now.Date;
            
            repository.Save(shift);
            var saved = repository.FindBy<Shift>(w => w.Date == shift.Date.Date).FirstOrDefault();
            Assert.IsNotNull(saved);

            var deleted = repository.Delete<Shift>(saved.Id);
            Assert.IsFalse(deleted.HasErrors());
        }

        #endregion
    }
}
