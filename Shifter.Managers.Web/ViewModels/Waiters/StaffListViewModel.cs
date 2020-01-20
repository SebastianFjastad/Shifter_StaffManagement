using Shifter.Service.Api.Dtos;
using Shifter.Shared.WebClient.ViewModels;
using System.Collections.Generic;

namespace Shifter.Managers.Web.ViewModels.Waiters
{
    public class StaffListViewModel : ShifterModelBase
    {
        #region constructor

        public StaffListViewModel()
        {
            StaffList = new List<StaffDto>();
        }

        #endregion

        #region Properties

        public IEnumerable<StaffDto> StaffList { get; set; }

        #endregion

    }
}
