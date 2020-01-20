using System.Collections.Generic;
using System.Linq;
using Framework;
using Framework.Notifications;
using Shifter.Managers.Web.ViewModels;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;
using System;

namespace Shifter.Managers.Web.Actions
{
    public class CopyScheduleAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public CopyScheduleAction(IServiceRegistry serviceRegistry)
        {
            Guard.ArgumentNotNull(serviceRegistry, "serviceRegistry");

            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<string, T> OnError { get; set; }

        public Func<T> OnSuccess { get; set; }

        public Func<T> OnWarning { get; set; }


        #endregion

        #region Methods

        public T Invoke(int restaurantId, DateTime copyFromStartDate, DateTime copyFromEndDate, DateTime copyToStartDate, IEnumerable<int> staffTypeIds, bool overwriteExistingShifts)
        {
            Guard.InstanceNotNull(OnSuccess, "OnSuccess");
            Guard.InstanceNotNull(OnError, "OnError");

            var result = serviceRegistry.ShiftService.CopyShifts(
              new CopyShiftsRequest
              {
                  RestaurantId = restaurantId,
                  CopyToStartDate = copyToStartDate,
                  CopyFromStartDate = copyFromStartDate,
                  CopyFromEndDate = copyFromEndDate,
                  OverwriteExistingShifts = overwriteExistingShifts,
                  StaffTypeIds = staffTypeIds
              });

            if (result.HasConflictWarning)
            {
                return OnWarning();
            }

            return result.NotificationCollection.HasErrors() ? OnError(result.NotificationCollection.First().Text) : OnSuccess();
        }

        #endregion
    }
}