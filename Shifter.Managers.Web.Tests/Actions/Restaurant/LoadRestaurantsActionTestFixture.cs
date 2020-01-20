using System;
using System.Collections.Generic;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shifter.Domain;
using Shifter.Managers.Web.Actions;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;
using Shifter.Service.Api.Responses;

namespace Shifter.Managers.Web.Tests.Actions.Restaurant
{
    [TestClass]
    public class LoadRestaurantsActionTestFixture
    {
        [TestMethod]
        public void EnsureGuardAgainstNullServiceRegistry()
        {
            var error = "";
            try
            {
                new LoadRestaurantsAction<dynamic>(null);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsTrue(error.Contains("serviceRegistry"));
        }

        [TestMethod]
        public void EnsureGuardAgainstNullOnManyRestaurantsFound()
        {
            var error = "";
            try
            {
                var action = new LoadRestaurantsAction<dynamic>(A.Fake<IServiceRegistry>())
                {
                    NoRestarauntsFound = () => new { Value = true },
                    OnOneRestarauntFound = (x) => new { Value = false }
                };

                action.Invoke(1);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsTrue(error.Contains("OnManyRestaurantsFound"));
        }

        [TestMethod]
        public void EnsureGuardAgainstNullNoRestarauntsFound()
        {
            var error = "";
            try
            {
                var action = new LoadRestaurantsAction<dynamic>(A.Fake<IServiceRegistry>())
                {
                    OnManyRestaurantsFound = (m) => new { Value = true },
                    OnOneRestarauntFound = (x) => new { Value = false }
                };

                action.Invoke(1);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsTrue(error.Contains("NoRestarauntsFound"));
        }

        [TestMethod]
        public void EnsureGuardAgainstNullOnOneRestarauntFound()
        {
            var error = "";
            try
            {
                var action = new LoadRestaurantsAction<dynamic>(A.Fake<IServiceRegistry>())
                {
                    OnManyRestaurantsFound = (m) => new { Value = true },
                    NoRestarauntsFound = () => new { Value = false }
                };

                action.Invoke(1);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsTrue(error.Contains("OnOneRestarauntFound"));
        }


        [TestMethod]
        public void EnsureOnOneRestarauntFoundIfOneFound()
        {
            var system = A.Fake<IServiceRegistry>();
            A.CallTo(() => system.RestaurantService.LoadRestaurantByManager(null)).WithAnyArguments().Returns(new LoadRestaurantResponse {Restaurants = new List<RestaurantDto> {new RestaurantDto()}});

            var action = new LoadRestaurantsAction<dynamic>(system)
            {
                OnOneRestarauntFound = (x) => new { Value = true },
                OnManyRestaurantsFound = (m) => new { Value = false },
                NoRestarauntsFound = () => new { Value = false }
            };

            var result = action.Invoke(1).Value;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EnsureOnManyRestaurantsFoundIfManyFound()
        {
            var system = A.Fake<IServiceRegistry>();
            A.CallTo(() => system.RestaurantService.LoadRestaurantByManager(null)).WithAnyArguments().Returns(new LoadRestaurantResponse { Restaurants = new List<RestaurantDto> { new RestaurantDto(), new RestaurantDto() } });

            var action = new LoadRestaurantsAction<dynamic>(system)
            {
                OnOneRestarauntFound = (x) => new { Value = false },
                OnManyRestaurantsFound = (m) => new { Value = true },
                NoRestarauntsFound = () => new { Value = false }
            };

            var result = action.Invoke(1).Value;

            Assert.IsTrue(result);
        }
    }
}
