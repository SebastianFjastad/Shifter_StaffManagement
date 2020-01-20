using Shifter.Service.Api.Dtos;
using System.Collections.Generic;

namespace Shifter.Managers.Web.ViewModels.Wall
{
    public class WallPostEditorViewModel
    {
        public WallPostEditorViewModel()
        {
            StaffTypes = new List<StaffTypeDto>();
            WallPost = new WallPostDto();
        }

        #region Properties

        public WallPostDto WallPost { get; set; }

        public IEnumerable<StaffTypeDto> StaffTypes { get; set; }

        #endregion
    }
}