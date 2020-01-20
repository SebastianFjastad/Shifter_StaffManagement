using System;
using System.Collections.Generic;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;

namespace Shifter.Waiters.Web.Actions.Shifts
{
    public class LoadWaiterShiftsAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public LoadWaiterShiftsAction(IServiceRegistry serviceRegistry)
        {
            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<IEnumerable<ShiftDto>, T> OnComplete { get; set; } 

        #endregion

        #region Methods

        public T Invoke(int restaurantId, DateTime shiftDate, int waiterId)
        {
            var loadShiftCollectionResponse = serviceRegistry.ShiftService.LoadShifts(
                new LoadShiftsRequest
                {
                    RestaurantId = restaurantId,
                    FromDate = shiftDate,
                    ToDate = shiftDate,
                    StaffId = waiterId
                });

            return OnComplete.Invoke(loadShiftCollectionResponse.Shifts);
        }

        #endregion

    }
}