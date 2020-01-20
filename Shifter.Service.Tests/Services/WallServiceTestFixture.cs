using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using Framework.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shifter.Domain.Model.Entities;
using Shifter.Domain.Services;

namespace Shifter.Service.Tests.Services
{
    [TestClass]
    public class WallServiceTestFixture
    {
        //4: posts for all staff types and for group

        [TestMethod]
        public void EnsureWallPostsCanBeFilteredForBothGroupAndSpecificStaffTypes()
        {
            var repo = A.Fake<IRepository>();

            A.CallTo(() => repo.FindBy<WallPost>(x => x == null)).WithAnyArguments().Returns(new List<WallPost>(){new WallPost(){StaffTypeId = null, Header = "all"}, new WallPost(){StaffTypeId = 1, Header = "one"}, new WallPost(){StaffTypeId = 2, Header = "two"}});

            var service = new WallDomainService(repo);

            var result = service.LoadWallPosts(0, null, null, new List<int>() {1}, true);

            Assert.IsTrue(result.Count() == 2);
            Assert.IsFalse(result.Any(r =>r.Header == "two"));
        }

        [TestMethod]
        public void EnsureWallPostsCanBeFilteredForOnlyGroupPosts()
        {
            var repo = A.Fake<IRepository>();

            A.CallTo(() => repo.FindBy<WallPost>(x => x == null)).WithAnyArguments().Returns(new List<WallPost>() { new WallPost() { StaffTypeId = null, Header = "all" }, new WallPost() { StaffTypeId = 1, Header = "one" }, new WallPost() { StaffTypeId = 2, Header = "two" } });

            var service = new WallDomainService(repo);

            var result = service.LoadWallPosts(0, null, null, new List<int>() { }, true);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsFalse(result.Any(r => r.Header == "one"));
            Assert.IsFalse(result.Any(r => r.Header == "two"));
        }

        [TestMethod]
        public void EnsureWallPostsCanBeFilteredForOnlySpecificStaffTypes()
        {
            var repo = A.Fake<IRepository>();

            A.CallTo(() => repo.FindBy<WallPost>(x => x == null)).WithAnyArguments().Returns(new List<WallPost>() { new WallPost() { StaffTypeId = null, Header = "all" }, new WallPost() { StaffTypeId = 1, Header = "one" }, new WallPost() { StaffTypeId = 2, Header = "two" } });

            var service = new WallDomainService(repo);

            var result = service.LoadWallPosts(0, null, null, new List<int>() { 1, 2 }, false);

            Assert.IsTrue(result.Count() == 2);
            Assert.IsFalse(result.Any(r => r.Header == "all"));
        }

        [TestMethod]
        public void EnsureWallPostsCanBeFilteredForAllStaffTypesAndForGroup()
        {
            var repo = A.Fake<IRepository>();

            A.CallTo(() => repo.FindBy<WallPost>(x => x == null)).WithAnyArguments().Returns(new List<WallPost>() { new WallPost() { StaffTypeId = null, Header = "all" }, new WallPost() { StaffTypeId = 1, Header = "one" }, new WallPost() { StaffTypeId = 2, Header = "two" } });

            var service = new WallDomainService(repo);

            var result = service.LoadWallPosts(0, null, null, new List<int>() { }, false);

            Assert.IsTrue(result.Count() == 3);
        }
    }
}
