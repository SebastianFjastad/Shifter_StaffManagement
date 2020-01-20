using System;
using System.Collections.Generic;
using System.Linq;
using Framework;
using Shifter.Domain.Model.Entities;
using Shifter.Domain.Services;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;
using Shifter.Service.Api.Responses;
using Shifter.Service.Api.Services;

namespace Shifter.Service.Services
{
    public class WallService : ShifterServiceBase, IWallService
    {
        private readonly IWallDomainService wallDomainService;
        private readonly IManagerDomainService managerService;
        private readonly IStaffDomainService staffService;

        public WallService(IWallDomainService wallDomainService, IStaffDomainService staffDomainService, IManagerDomainService managerDomainService)
        {
            Guard.ArgumentNotNull(wallDomainService, "wallDomainService");
            Guard.ArgumentNotNull(staffDomainService, "staffDomainService");
            Guard.ArgumentNotNull(managerDomainService, "managerDomainService");

            this.wallDomainService = wallDomainService;
            this.staffService = staffDomainService;
            this.managerService = managerDomainService;
        }

        public WallPostCollectionResponse LoadWallPosts(LoadWallPostsRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var result = new WallPostCollectionResponse();

                var wallPosts = wallDomainService.LoadWallPosts(request.RestaurantId, request.IncludePostsFrom, request.IncludePostsTo, request.StaffTypeIds, request.IncludeGroupPosts).ToList();
                var total = staffService.LoadTotalStaff(request.RestaurantId);

                result.WallPosts = wallPosts.AsWallPostDtos(total);

                return result;
            });
        }

        public LoadWallPostResponse LoadWallPost(GenericEntityRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var result = new LoadWallPostResponse();

                var wallPost = wallDomainService.LoadWallPost(request.EntityId);
                var total = staffService.LoadTotalStaff(wallPost.RestaurantId);
                result.WallPost = wallPost.AsWallPostDto(total);

                return result;
            });
        }

        public GenericServiceResponse SaveWallPost(SaveWallPostRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var wallPost = request.WallPost.AsDomainModel();

                var userAccount = GetUserAccount(request.WallPost.PostedByType, request.PostedById);

                wallPost.PostedBy = userAccount;

                if (request.WallPost.Id.IsNotNull() && request.WallPost.Id != 0)
                {
                    var existing = wallDomainService.LoadWallPost(request.WallPost.Id);
                    wallPost.SeenBy = existing.SeenBy;
                    wallPost.Comments = existing.Comments;
                }

                var notificationCollection = wallDomainService.SaveWallPost(wallPost);

                return new GenericServiceResponse
                {
                    NotificationCollection = notificationCollection
                };
            });
        }

        private UserAccount GetUserAccount(PostedByType postedByType, int profileId)
        {
            return postedByType == PostedByType.Manager ? managerService.LoadManager(profileId).UserAccount : staffService.LoadStaffMember(profileId).UserAccount;
        }

        public GenericServiceResponse DeleteWallPost(GenericEntityRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");

                var notificationCollection = wallDomainService.DeleteWallPost(request.EntityId);

                return new GenericServiceResponse
                {
                    NotificationCollection = notificationCollection
                };
            });
        }

        public GenericServiceResponse SaveComment(SaveCommentRequest request)
        {
            return TryExecute(() =>
            {
                Guard.ArgumentNotNull(request, "request");
                var result = new GenericServiceResponse();

                if (request.Comment.IsNotNullOrEmpty())
                {
                    //var post = wallDomainService.LoadPost(request.WallPostId);
                    var user = GetUserAccount(request.PostedByType, request.CommentedById);

                    var comment = new Comment()
                    {
                        WallPostId = request.WallPostId,
                        CommentDate = DateTime.Now,
                        Value = request.Comment,
                        CommentBy = user
                    };

                    //var comments = post.Comments.ToList();
                    //comments.Add(comment);

                    result.NotificationCollection += wallDomainService.SaveComment(comment);
                }

                return result;
            });
        }

        public GenericServiceResponse UpdateSeenBy(UpdateSeeByRequest request)
        {
            return TryExecute(() =>
            {
                var result = new GenericServiceResponse();
                var staff = staffService.LoadStaffMember(request.StaffId);

                var posts = wallDomainService.LoadWallPosts(request.RestaurantId, null, null, new List<int>() { staff.StaffType.Id }, true);

                foreach (var wallPost in posts)
                {
                    var seenBy = wallPost.SeenBy.ToList();
                    if (seenBy.All(s => s.Id != staff.UserAccount.Id))
                    {
                        seenBy.Add(staff.UserAccount);
                    }

                    wallPost.SeenBy = seenBy;

                    wallDomainService.SaveWallPost(wallPost);
                }

                return result;
            });

        }
    }
}
