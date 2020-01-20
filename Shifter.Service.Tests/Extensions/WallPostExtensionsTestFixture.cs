using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shifter.Domain.Model.Entities;
using Shifter.Service.Api.Responses;

namespace Shifter.Service.Tests.Extensions
{
    [TestClass]
    public class WallPostExtensionsTestFixture
    {
        [TestMethod]
        public void EnsureWallPostsAreMappedCorrectly()
        {
            var wallPost = new WallPost()
                           {
                               PostedByType = "Manager",
                               SeenBy = new List<UserAccount>{new UserAccount(){FirstName = "Batman", LastName = "a"}},
                               Comments = new List<Comment> { new Comment() { CommentBy = new UserAccount() { FirstName = "Ironman", LastName = "b"} } }
                           };

            var wallPosts = new List<WallPost> {wallPost};

            var dtos = wallPosts.AsWallPostDtos(0);

            foreach (var dto in dtos)
            {
                Assert.IsTrue(dto.SeenByNames.Any(s => s == "Batman a"));
                Assert.IsTrue(dto.Comments.Any(c => c.CommenterName == "Ironman b"));
            }
        }
    }
}
