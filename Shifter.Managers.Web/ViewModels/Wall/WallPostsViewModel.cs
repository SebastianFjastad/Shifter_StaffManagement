using Shifter.Service.Api.Dtos;
using Shifter.Shared.WebClient.ViewModels;
using System.Collections.Generic;

namespace Shifter.Managers.Web.ViewModels.Wall
{
    public class WallPostsViewModel : ShifterModelBase
    {
        public WallPostsViewModel()
        {
            WallPosts = new List<WallPostDto>();
        }

        #region Properties

        public IEnumerable<WallPostDto> WallPosts { get; set; }

        #endregion
    }
}