using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Notifications;
using Shifter.Domain.Model.Entities;

namespace Shifter.Domain.Services
{
    public interface IWallDomainService
    {
        IEnumerable<WallPost> LoadWallPosts(int restaurantId, DateTime? dateFrom, DateTime? dateTo, IEnumerable<int> staffTypeIds, bool includeGroupPosts);

        WallPost LoadWallPost(int wallPostId);

        NotificationCollection SaveWallPost(WallPost wallPost);

        NotificationCollection SaveComment(Comment comment);

        NotificationCollection DeleteWallPost(int wallPostId);
    }
}
