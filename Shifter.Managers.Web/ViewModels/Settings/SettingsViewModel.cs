using System.Collections.Generic;
using Framework.Notifications;
using Shifter.Service.Api.Dtos;
using Shifter.Shared.WebClient.ViewModels;

namespace Shifter.Managers.Web.ViewModels
{
    public class SettingsViewModel : ShifterModelBase
    {
        #region Constructors

        public SettingsViewModel()
        {
            Settings = new SettingsDto();
        }

        #endregion

        #region Properties

        public SettingsDto Settings { get; set; }

        public NotificationCollection Messages { get; set; }

        public IEnumerable<StaffTypeDto> StaffTypes { get; set; }


        #endregion
    }
}