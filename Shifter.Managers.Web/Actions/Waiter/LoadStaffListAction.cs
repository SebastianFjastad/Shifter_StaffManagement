using System.Collections.Generic;
using System.Linq;
using Framework;
using Shifter.Managers.Web.ViewModels.Waiters;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;
using System;
using Shifter.Service.Api.Responses;

namespace Shifter.Managers.Web.Actions.Waiter
{
    public class LoadStaffListAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public LoadStaffListAction(IServiceRegistry serviceRegistry)
        {
            Guard.ArgumentNotNull(serviceRegistry, "serviceRegistry");

            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<StaffListViewModel, T> OnComplete { get; set; }

        #endregion

        #region Methods

        public T Invoke(int restaurantId)
        {
            Guard.InstanceNotNull(OnComplete, "OnComplete");

            var viewModel = new StaffListViewModel();

            StaffCollectionResponse collectionResponse = serviceRegistry.StaffService.LoadStaffList(new LoadStaffListRequest { RestaurantId = restaurantId });

            var types = (from s in collectionResponse.Staff
                group s by s.StaffType.Id
                into g
                select new {g.Key, count = g.Count()}).ToList();

            IEnumerable<StaffDto> stafflist = from s in collectionResponse.Staff
                from g in types
                where s.StaffType.Id == g.Key
                orderby g.count descending
                select s;

            viewModel.StaffList = stafflist;

            return OnComplete.Invoke(viewModel);
        }

        #endregion
    }
}