using Framework;
using Shifter.Managers.Web.ViewModels;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;
using System;

namespace Shifter.Managers.Web.Actions
{
    public class SaveShiftAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public SaveShiftAction(IServiceRegistry serviceRegistry)
        {
            Guard.ArgumentNotNull(serviceRegistry, "serviceRegistry");

            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<ShiftsResultViewModel, T> OnComplete { get; set; }


        #endregion

        #region Methods

        public T Invoke(TimeSpan startTime, TimeSpan endTime, DateTime shiftDate, int waiterId, int restaurantId)
        {
            Guard.InstanceNotNull(OnComplete, "OnComplete");

            var shift = new ShiftDto();

            shift.StartTime = shiftDate.Add(startTime);
            //if end time is smaller than start then it spans 2 days and therefor and extra day is added to the date
            if (endTime < startTime)
            {
                shiftDate = shiftDate.AddDays(1);
            }
            shift.EndTime = shiftDate.Add(endTime);

            shift.Restaurant = new RestaurantDto { Id = restaurantId };
            shift.Staff = new StaffDto(){Id = waiterId};

            var saveShiftResponse = serviceRegistry.ShiftService.SaveShift(new SaveShiftRequest { Shift = shift });
            var shiftsResponse = serviceRegistry.ShiftService.LoadShifts(new LoadShiftsRequest() { FromDate = shift.StartTime, ToDate = shift.EndTime, RestaurantId = restaurantId, StaffId = waiterId });
            var notifications = saveShiftResponse.NotificationCollection;

            return OnComplete(new ShiftsResultViewModel(shiftsResponse.Shifts) { Notifications = notifications });
        }

        #endregion
    }
}