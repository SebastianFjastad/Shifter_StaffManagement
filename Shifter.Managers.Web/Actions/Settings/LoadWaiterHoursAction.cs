using Framework;
using Shifter.Managers.Web.ViewModels;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;
using System;
using System.Collections.Generic;

namespace Shifter.Managers.Web.Actions
{
    public class LoadWaiterHoursAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public LoadWaiterHoursAction(IServiceRegistry serviceRegistry)
        {
            Guard.ArgumentNotNull(serviceRegistry, "serviceRegistry");
            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<WaiterHoursViewModel, T> OnComplete { get; set; }

        #endregion

        #region Methods

        public T Invoke(int restaurantId, DateTime fromDate, DateTime toDate, double hourlyRate, IEnumerable<int> staffTypeIds)
        {
            Guard.InstanceNotNull(OnComplete, "OnComplete");

            var response = serviceRegistry.StaffService.LoadStaffHours(new LoadStaffHoursRequest()
            {
                RestaurantId = restaurantId,
                FromDate = fromDate,
                ToDate = toDate,
                StaffTypeIds = staffTypeIds
            });

            var viewModel = new WaiterHoursViewModel()
            {
                WaiterHours = response.StaffHoursCollection,
                HourlyRate = hourlyRate
            };

            return OnComplete.Invoke(viewModel);
        }

        #endregion
    }
}