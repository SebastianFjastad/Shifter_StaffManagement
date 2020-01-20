using System.Collections.Generic;
using Shifter.Service.Api.Dtos;
using Shifter.Shared.WebClient.ViewModels;

namespace Shifter.Waiters.Web.ViewModels.Wall
{
    public class WallViewModel : ShifterModelBase
    {
        public WallViewModel()
        {
            WallPosts = new List<WallPostDto>();
        }

        #region Properties

        public IEnumerable<WallPostDto> WallPosts { get; set; }

        #endregion
    }
}