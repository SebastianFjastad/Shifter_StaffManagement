using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shifter.Service.Api.Dtos;
using Shifter.Shared.WebClient.ViewModels;

namespace Shifter.Managers.Web.ViewModels.Wall
{
    public class WallViewModel : ShifterModelBase
    {
        public WallViewModel()
        {
            StaffTypes = new List<StaffTypeDto>();
        }

        #region Properties

        public IEnumerable<StaffTypeDto> StaffTypes { get; set; }

        #endregion
    }
}