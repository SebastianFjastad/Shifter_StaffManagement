using Framework;
using Shifter.Domain.Model.Entities;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace Shifter.Service.Api.Responses
{
    public static class WallPostExtensions
    {
        public static IEnumerable<WallPostDto> AsWallPostDtos(this IEnumerable<WallPost> wallPosts, int totalStaff)
        {
            Guard.InstanceNotNull(wallPosts, "wallPosts");

            var wallPostDtos = wallPosts.Select(w => w.AsWallPostDto(totalStaff)).ToList();

            return wallPostDtos;
        }

        public static WallPostDto AsWallPostDto(this WallPost wallPost, int totalStaff)
        {
            Guard.InstanceNotNull(wallPost, "wallPost");

            var wallPostDto = MappingUtility.Map<WallPost, WallPostDto>(wallPost);
            wallPostDto.SeenByNames = wallPost.SeenBy.Select(u => u.FullName).ToList();
            wallPostDto.Comments = wallPost.Comments.Select(c => c.AsCommentDto()).ToList();
            wallPostDto.TotalStaff = totalStaff;

            return wallPostDto;
        }

        public static WallPost AsDomainModel(this WallPostDto wallPostDto)
        {
            Guard.InstanceNotNull(wallPostDto, "wallPostDto");

            var wallPost = MappingUtility.Map<WallPostDto, WallPost>(wallPostDto);

            return wallPost;
        }

        public static CommentDto AsCommentDto(this Comment comment)
        {
            Guard.InstanceNotNull(comment, "comment");

            var commentDto = MappingUtility.Map<Comment, CommentDto>(comment);
            commentDto.CommenterName = comment.CommentBy.FullName;

            return commentDto;
        }
    }
}
