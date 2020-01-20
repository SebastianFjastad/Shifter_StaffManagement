using System.Collections.Generic;
using System.Web;
using Framework.Notifications;
using Shifter.Service.Api.Dtos;
using Shifter.Shared.WebClient.ViewModels;

namespace Shifter.Managers.Web.ViewModels
{
    public class WaiterHoursViewModel : ShifterModelBase
    {
        #region Constructors

        public WaiterHoursViewModel()
        {
            WaiterHours = new List<StaffHours>();
        }

        #endregion

        #region Properties

        public IEnumerable<StaffHours> WaiterHours { get; set; }
        public double HourlyRate { get; set; }

        #endregion
    }
}