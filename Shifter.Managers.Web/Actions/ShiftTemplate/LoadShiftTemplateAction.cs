using System.Linq;
using Framework;
using System;
using Shifter.Managers.Web.ViewModels;
using Shifter.Service.Api.Requests;

namespace Shifter.Managers.Web.Actions
{
    public class LoadShiftTemplateAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public LoadShiftTemplateAction(IServiceRegistry serviceRegistry)
        {
            Guard.ArgumentNotNull(serviceRegistry, "serviceRegistry");

            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<ShiftTemplateViewModel, T> OnComplete { get; set; }

        #endregion

        #region Methods

        public T Invoke(int restaurantId)
        {
            Guard.InstanceNotNull(OnComplete, "OnComplete");

            var viewModel = new ShiftTemplateViewModel();

            var shiftTemplateResponse = serviceRegistry.ShiftTemplateService.LoadTemplates(
                new LoadShiftTemplateByRestaurantIdRequest {RestaurantId = restaurantId});

            var shiftTimeSlotResponse = serviceRegistry.ShiftTimeSlotService.LoadTimeSlots(
                new LoadTimeSlotCollectionRequest { RestaurantId = restaurantId });

            viewModel.ShiftTemplates = shiftTemplateResponse.ShiftTemplates.ToList();
            viewModel.TimeSlots = shiftTimeSlotResponse.ShiftTimeslots;
            viewModel.RestaurantId = restaurantId;

            return OnComplete.Invoke(viewModel);
        }

        #endregion
    }
}