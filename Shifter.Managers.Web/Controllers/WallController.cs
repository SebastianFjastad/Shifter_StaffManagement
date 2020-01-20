using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shifter.Managers.Web.Actions.Wall;
using Shifter.Managers.Web.Attributes;
using Shifter.Managers.Web.Utils;
using Shifter.Managers.Web.ViewModels.Wall;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;

namespace Shifter.Managers.Web.Controllers
{
    [AuthorizeUserData]
    public class WallController : ShifterBaseController
    {
        /// <summary>
        /// Constructor to set the base system
        /// </summary>
        public WallController(IServiceRegistry serviceRegistry)
            : base(serviceRegistry)
        {

        }

        public ActionResult ManageWall()
        {
            var action = new LoadWallPostLookupsAction<ViewResult>(ServiceRegistry)
            {
                OnComplete = (model) => View("ManageWall", model)
            };

            return action.Invoke();
        }

        public PartialViewResult LoadWallPosts(string staffTypeIds, bool includeGroupPosts = false)
        {
            var action = new LoadWallPostsAction<PartialViewResult>(ServiceRegistry)
            {
                OnComplete = (model) => PartialView("WallPosts", model)
            };

            return action.Invoke(ResolveRestaurantId(), StringUtils.ConvertToList(staffTypeIds), includeGroupPosts);
        }

        public PartialViewResult NewPost()
        {
            var result = ServiceRegistry.StaffService.LoadStaffTypes();
            return PartialView("WallPost", new WallPostEditorViewModel() { StaffTypes = result.StaffTypes });
        }

        public PartialViewResult LoadPost(int wallPostId)
        {
            var result = ServiceRegistry.WallService.LoadWallPost(new GenericEntityRequest(wallPostId));
            var staffTypeResult = ServiceRegistry.StaffService.LoadStaffTypes();

            var model = new WallPostEditorViewModel() { WallPost = result.WallPost, StaffTypes = staffTypeResult.StaffTypes };
            return PartialView("WallPost", model);
        }

        [HttpPost]
        public ActionResult DeleteWallPost(int wallPostId)
        {
            var result = ServiceRegistry.WallService.DeleteWallPost(new GenericEntityRequest(wallPostId));

            return RedirectToAction("ManageWall");
        }

        [HttpPost]
        public ActionResult SaveWallPost(WallPostViewModel model)
        {
            model.WallPost.Body = model.Content;
            model.WallPost.RestaurantId = ResolveRestaurantId();

            var result = ServiceRegistry.WallService.SaveWallPost(new SaveWallPostRequest() { WallPost = model.WallPost, PostedById = ResolveManagerId() });

            return RedirectToAction("ManageWall");
        }
    }

    public class WallPostViewModel
    {
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Content { get; set; }

        public WallPostDto WallPost { get; set; }
    }
}