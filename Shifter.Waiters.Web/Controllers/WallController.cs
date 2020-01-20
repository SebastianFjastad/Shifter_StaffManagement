using System.Linq;
using Shifter.Service.Api.Requests;
using Shifter.Shared.WebClient.Extensions;
using Shifter.Waiters.Web.Actions.Wall;
using Shifter.Waiters.Web.Attributes;
using Shifter.Waiters.Web.ViewModels.Wall;
using System.Web.Mvc;

namespace Shifter.Waiters.Web.Controllers
{
    [AuthorizeUserData]
    public class WallController : ShifterBaseController
    {
        /// <summary>
        /// Constructor to set the base system
        /// </summary>
        public WallController(IServiceRegistry serviceRegistry) : base(serviceRegistry) { }

        [HttpGet]
        public ActionResult Index()
        {
            var result = ServiceRegistry.WallService.LoadWallPosts(new LoadWallPostsRequest() { RestaurantId = ResolveRestaurantId() });
            return View("Wall", new WallViewModel() { WallPosts = result.WallPosts.OrderByDescending(w => w.PostedDate) });
        }

        /// <summary>
        /// Saves a comment on a wall post
        /// </summary>
        [HttpPost]
        public ActionResult SaveComment(string comment, int wallPostId)
        {
            return new SaveCommentAction<ActionResult>(ServiceRegistry)
            {
                OnSuccess = () => RedirectToAction("Index"),
                OnFailure = (n) => View("Errors")
            }.Invoke(wallPostId, ResolveStaffId(), comment);
        }

        [HttpPost]
        public JsonResult UpdateSeenWallPosts()
        {
            ServiceRegistry.WallService.UpdateSeenBy(new UpdateSeeByRequest(){RestaurantId = ResolveRestaurantId(), StaffId = ResolveStaffId()});

            return new JsonResult();
        }
    }
}
