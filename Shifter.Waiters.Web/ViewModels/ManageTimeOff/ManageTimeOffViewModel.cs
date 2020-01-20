using System.Collections.Generic;
using Shifter.Service.Api.Dtos;
using Shifter.Shared.WebClient.ViewModels;

namespace Shifter.Waiters.Web.ViewModels.ManageTimeOff
{
    public class ManageTimeOffViewModel : ShifterModelBase
    {
        public ManageTimeOffViewModel()
        {
            Unavailability = new List<StaffUnavailabilityRecordDto>();
        }

        #region Properties

        public IEnumerable<StaffUnavailabilityRecordDto> Unavailability { get; set; }

        #endregion
    }
}