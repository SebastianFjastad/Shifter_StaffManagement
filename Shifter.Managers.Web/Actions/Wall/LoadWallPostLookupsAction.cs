using Framework;
using Shifter.Managers.Web.ViewModels.Wall;
using Shifter.Service.Api.Dtos;
using System;
using System.Collections.Generic;
using Shifter.Service.Api.Requests;

namespace Shifter.Managers.Web.Actions.Wall
{
    public class LoadWallPostLookupsAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public LoadWallPostLookupsAction(IServiceRegistry serviceRegistry)
        {
            Guard.ArgumentNotNull(serviceRegistry, "serviceRegistry");

            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<WallViewModel, T> OnComplete { get; set; }

        #endregion

        #region Methods

        public T Invoke()
        {
            Guard.InstanceNotNull(OnComplete, "OnComplete");

            var result = serviceRegistry.StaffService.LoadStaffTypes();

            var viewModel = new WallViewModel
            {
                StaffTypes = result.StaffTypes
            };

            return OnComplete(viewModel);
        }

        #endregion
    }
}