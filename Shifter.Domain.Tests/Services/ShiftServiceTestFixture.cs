using System;
using FakeItEasy;
using Framework.Domain;
using Framework.PubSub;
using Framework.PubSub.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shifter.Domain.Model.Entities;
using Shifter.Domain.Services;
using System.Collections.Generic;

namespace Shifter.Domain.Tests.Services
{
    [TestClass]
    public class ShiftServiceTestFixture
    {
        private ShiftDomainService shiftService;
        private IPublisher publisher;
        private IRepository repository;

        [TestInitialize]
        public void Setup()
        {
            repository = A.Fake<IRepository>();
            publisher = A.Fake<IPublisher>();

            shiftService = new ShiftDomainService(repository, publisher);
        }

        [TestMethod]
        public void Ensure_SaveShift_DoesNotUpdateShiftAssignmentsIfShiftNotAssigendAndNew()
        {
            var result = shiftService.SaveShift(new Shift());

            A.CallTo(() => publisher.Publish(new ApplicationMessage())).WhenArgumentsMatch(s => s.Get<IMessage>(0).Header.Topic == MessageTopics.ShiftAssigned).MustNotHaveHappened();
            A.CallTo(() => repository.Save(new List<TrackedShiftAssignment>())).WithAnyArguments().MustNotHaveHappened();
        }

        [TestMethod]
        public void Ensure_SaveShift_UpdatesShiftAssignmentsIfShiftAssignedFromUnassignedToWaiter()
        {
            shiftService.SaveShift(new Shift() {Staff = new Staff() {Id = 1}, Restaurant = new Restaurant(){Id = 1}});
            A.CallTo(() => publisher.Publish(new ApplicationMessage())).WhenArgumentsMatch(s => s.Get<IMessage>(0).Header.Topic == MessageTopics.ShiftAssigned).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => repository.Save(null)).WithAnyArguments().MustHaveHappened(Repeated.Exactly.Twice);
        }

        [TestMethod]
        public void Ensure_SaveShift_UpdatesShiftAssignmentsIfShiftAssignedFromWaiterToWaiter()
        {
            A.CallTo(() => repository.FindById<Shift>(1)).Returns(new Shift() { Staff = new Staff() { Id = 2 } });

            shiftService.SaveShift(new Shift() { Id = 1, Staff = new Staff() { Id = 1 }, Restaurant = new Restaurant() { Id = 1 } });
            A.CallTo(() => publisher.Publish(new ApplicationMessage())).WhenArgumentsMatch(s => s.Get<IMessage>(0).Header.Topic == MessageTopics.ShiftAssigned).MustHaveHappened(Repeated.Exactly.Twice);
            A.CallTo(() => repository.Save(null)).WithAnyArguments().MustHaveHappened(Repeated.Exactly.Times(3));
        }

        [TestMethod]
        public void Ensure_SaveShift_UpdatesShiftAssignmentsIfShiftAssignedFromWaiterToUnassigned()
        {
            A.CallTo(() => repository.FindById<Shift>(1)).Returns(new Shift() { Staff = new Staff() { Id = 2 } });

            shiftService.SaveShift(new Shift() { Id = 1, Restaurant = new Restaurant() { Id = 1 } });
            A.CallTo(() => publisher.Publish(new ApplicationMessage())).WhenArgumentsMatch(s => s.Get<IMessage>(0).Header.Topic == MessageTopics.ShiftAssigned).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => repository.Save(null)).WithAnyArguments().MustHaveHappened(Repeated.Exactly.Twice);
        }

        [TestMethod]
        public void Ensure_AssignShift_UpdatesShiftAssignmentsIfShiftWasAssigned()
        {
            A.CallTo(() => repository.FindById<Shift>(1)).Returns(new Shift() { Staff = new Staff() { Id = 2 }, Restaurant = new Restaurant(){Id = 1}});

            shiftService.AssignShift(1, 1, "");

            A.CallTo(() => publisher.Publish(new ApplicationMessage())).WhenArgumentsMatch(s => s.Get<IMessage>(0).Header.Topic == MessageTopics.ShiftAssigned).MustHaveHappened(Repeated.Exactly.Twice);
            A.CallTo(() => repository.Save(null)).WithAnyArguments().MustHaveHappened(Repeated.Exactly.Times(3));
        }

        [TestMethod]
        public void Ensure_AssignShift_UpdatesShiftAssignments()
        {
            A.CallTo(() => repository.FindById<Shift>(1)).Returns(new Shift() { Restaurant = new Restaurant() { Id = 1 } });

            shiftService.AssignShift(1, 1, "");

            A.CallTo(() => publisher.Publish(new ApplicationMessage())).WhenArgumentsMatch(s => s.Get<IMessage>(0).Header.Topic == MessageTopics.ShiftAssigned).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => repository.Save(null)).WithAnyArguments().MustHaveHappened(Repeated.Exactly.Twice);
        }
    }
}
