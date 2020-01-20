using System;
using Framework;
using Framework.Notifications.Extensions;
using Shifter.Managers.Web.ViewModels;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;
using Shifter.Service.Api.Services;

namespace Shifter.Managers.Web.Actions
{
    public class LoadStaffMemberAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public LoadStaffMemberAction(IServiceRegistry serviceRegistry)
        {
            Guard.ArgumentNotNull(serviceRegistry, "serviceRegistry");

            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<StaffMemberViewModel, T> OnComplete { get; set; }

        #endregion

        #region Methods

        public T Invoke(int restaurantId, int? waiterId = null)
        {
            Guard.InstanceNotNull(OnComplete, "OnComplete");

            var viewModel = new StaffMemberViewModel();

            if (waiterId.IsNotNull())
            {
                var response = serviceRegistry.StaffService.LoadStaffMember(new LoadStaffMemberRequest
                {
                    StaffId = waiterId.Value,
                    RestaurantId = restaurantId
                });

                if (response.IsNotNull() && response.Staff.IsNotNull())
                {
                    viewModel.Staff = response.Staff;
                    viewModel.EditMode = true;
                    viewModel.StaffMemberHasNoEmailAddress = response.Staff.HasNoEmailAddress;
                }
                else
                {
                    viewModel.Notifications.AddError("Failed to load staff member.");
                }
            }
            else
            {
                viewModel.Staff = new StaffDto();
            }

            var collectionResponse = serviceRegistry.StaffService.LoadStaffTypes();

            viewModel.StaffTypes = collectionResponse.StaffTypes;

            return OnComplete.Invoke(viewModel);
        }

        #endregion
    }
}