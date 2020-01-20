using System.Linq;
using Framework;
using Shifter.Managers.Web.ViewModels.Wall;
using Shifter.Service.Api.Dtos;
using System;
using System.Collections.Generic;
using Shifter.Service.Api.Requests;

namespace Shifter.Managers.Web.Actions.Wall
{
    public class LoadWallPostsAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public LoadWallPostsAction(IServiceRegistry serviceRegistry)
        {
            Guard.ArgumentNotNull(serviceRegistry, "serviceRegistry");

            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<WallPostsViewModel, T> OnComplete { get; set; }

        #endregion

        #region Methods

        public T Invoke(int restaurantId, IEnumerable<int> staffTypeIds, bool includeGroupPosts)
        {
            Guard.InstanceNotNull(OnComplete, "OnComplete");

            var result = serviceRegistry.WallService.LoadWallPosts(new LoadWallPostsRequest() { RestaurantId = restaurantId, StaffTypeIds = staffTypeIds, IncludeGroupPosts = includeGroupPosts});

            var viewModel = new WallPostsViewModel
            {
                WallPosts = result.WallPosts.OrderByDescending(w => w.PostedDate)
            };

            return OnComplete(viewModel);
        }

        #endregion
    }
}