using System.Linq;
using Framework;
using System;
using Shifter.Managers.Web.ViewModels;
using Shifter.Service.Api.Requests;

namespace Shifter.Managers.Web.Actions
{
    public class SaveShiftTemplateAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public SaveShiftTemplateAction(IServiceRegistry serviceRegistry)
        {
            Guard.ArgumentNotNull(serviceRegistry, "serviceRegistry");
            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<ShiftTemplateViewModel, T> OnComplete { get; set; }

        #endregion

        #region Methods

        public T Invoke(ShiftTemplateViewModel viewModel, int restaurantId)
        {
            Guard.InstanceNotNull(OnComplete, "OnComplete");
            Guard.InstanceNotNull(viewModel, "viewModel");

            var saveTemplateResponse = serviceRegistry.ShiftTemplateService.SaveTemplates(
                new SaveShiftTemplatesRequest {ShiftTemplates = viewModel.ShiftTemplates});

            viewModel.Notifications = saveTemplateResponse.NotificationCollection;

            if (!viewModel.Notifications.HasErrors())
            {
                var loadTemplatesResponse = serviceRegistry.ShiftTemplateService.LoadTemplates(
                    new LoadShiftTemplateByRestaurantIdRequest {RestaurantId = restaurantId});

                viewModel.ShiftTemplates = loadTemplatesResponse.ShiftTemplates.ToList();
            }

            var loadTimeSlotsResponse = serviceRegistry.ShiftTimeSlotService.LoadTimeSlots(
                new LoadTimeSlotCollectionRequest {RestaurantId = restaurantId});

            viewModel.TimeSlots = loadTimeSlotsResponse.ShiftTimeslots;
            viewModel.RestaurantId = restaurantId;

            return OnComplete.Invoke(viewModel);
        }

        #endregion

    }
}