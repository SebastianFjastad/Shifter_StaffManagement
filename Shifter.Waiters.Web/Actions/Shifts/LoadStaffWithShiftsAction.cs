using System.Collections.Generic;
using System.Linq;
using Shifter.Service.Api.Requests;
using Shifter.Waiters.Web.ViewModels;
using System;

namespace Shifter.Waiters.Web.Actions.Shifts
{
    public class LoadStaffWithShiftsAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public LoadStaffWithShiftsAction(IServiceRegistry serviceRegistry)
        {
            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<AllShiftsViewModel, T> OnComplete { get; set; }

        #endregion

        #region Methods

        public T Invoke(int waiterId, int restaurantId, DateTime date)
        {
            var model = new AllShiftsViewModel();

            var loadWaiterResponse = serviceRegistry.StaffService.LoadStaffMember(new LoadStaffMemberRequest { StaffId = waiterId, IncludeShiftsFrom = date, IncludeShiftsTo = date, RestaurantId = restaurantId});

            var request = new LoadStaffListRequest
            {
                RestaurantId = restaurantId,
                IncludeShiftsFrom = date,
                IncludeShiftsTo = date,
                StaffTypeIds = new List<int> { loadWaiterResponse.Staff.StaffType.Id }
            };

            var loadWaitersResponse = serviceRegistry.StaffService.LoadStaffList(request);

            model.Staff = loadWaiterResponse.Staff;
            model.Waiters = loadWaitersResponse.Staff;
            model.SelectedDay = date;

            return OnComplete.Invoke(model);
        }

        #endregion

    }
}