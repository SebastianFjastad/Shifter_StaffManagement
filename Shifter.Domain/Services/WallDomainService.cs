using System.Linq;
using Framework;
using Framework.Domain;
using Framework.Notifications;
using LinqKit;
using Shifter.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Shifter.Domain.Services
{
    public class WallDomainService : IWallDomainService
    {
        public WallDomainService(IRepository repository)
        {
            Guard.ArgumentNotNull(repository, "repository");

            this.repository = repository;
        }

        private readonly IRepository repository;

        public IEnumerable<WallPost> LoadWallPosts(int restaurantId, DateTime? fromDate, DateTime? toDate, IEnumerable<int> staffTypeIds, bool includeGroupPosts)
        {
            var predicate = BuildPredicate(restaurantId, fromDate, toDate, staffTypeIds, includeGroupPosts);

            var result = repository.FindBy(predicate);

            //TODO this needs to be looked at. There are 4 combinations (any doesn't work in predicate builder)
            //1: only group posts
            //2: only posts for specific staff types 
            //3: posts for both group and specific staff types
            //4: posts for all staff types and for group
            if (staffTypeIds.IsNotNull() && staffTypeIds.Any())
            {
                if (includeGroupPosts)
                {
                    result = result.Where(s => s.StaffTypeId == null || staffTypeIds.Any(id => id == s.StaffTypeId));
                }
                else
                {
                    result = result.Where(s => staffTypeIds.Any(id => id == s.StaffTypeId));
                }
            }
            else if (includeGroupPosts)
            {
                result = result.Where(s => s.StaffTypeId == null);
            }

            return result.ToList();
        }

        public WallPost LoadWallPost(int wallPostId)
        {
            return repository.FindById<WallPost>(wallPostId);
        }

        public NotificationCollection SaveWallPost(WallPost wallPost)
        {
            var result = NotificationCollection.CreateEmpty();

            result += repository.Save(wallPost);

            return result;
        }

        public NotificationCollection SaveComment(Comment comment)
        {
            var result = NotificationCollection.CreateEmpty();

            result += repository.Save(comment);

            return result;
        }

        public NotificationCollection DeleteWallPost(int wallPostId)
        {
            var result = NotificationCollection.CreateEmpty();

            result += repository.Delete<WallPost>(wallPostId);

            return result;
        }

        private Expression<Func<WallPost, bool>> BuildPredicate(int restaurantId, DateTime? fromDate, DateTime? toDate, IEnumerable<int> staffTypeIds, bool includeGroupPosts)
        {
            var predicate = PredicateBuilder.True<WallPost>();
            predicate = predicate.And(s => s.RestaurantId == restaurantId);

            if (fromDate.HasValue)
            {
                predicate = predicate.And(s => s.PostedDate >= fromDate);
            }

            if (toDate.HasValue)
            {
                predicate = predicate.And(s => s.PostedDate <= toDate);
            }

            return predicate;
        }

    }
}
