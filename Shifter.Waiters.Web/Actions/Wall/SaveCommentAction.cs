using Framework;
using Framework.Notifications;
using Shifter.Service.Api.Dtos;
using Shifter.Service.Api.Requests;
using System;

namespace Shifter.Waiters.Web.Actions.Wall
{
    public class SaveCommentAction<T> where T : class
    {
        #region Locals

        private readonly IServiceRegistry serviceRegistry;

        #endregion

        #region Constructors

        public SaveCommentAction(IServiceRegistry serviceRegistry)
        {
            Guard.ArgumentNotNull(serviceRegistry, "serviceRegistry");

            this.serviceRegistry = serviceRegistry;
        }

        #endregion

        #region Properties

        public Func<T> OnSuccess { get; set; }

        public Func<NotificationCollection,T> OnFailure { get; set; }

        #endregion

        #region Methods

        public T Invoke(int wallPostId, int waterId, string comment)
        {
            Guard.InstanceNotNull(OnSuccess, "OnSuccess");
            Guard.InstanceNotNull(OnFailure, "OnFailure");

            var result = serviceRegistry.WallService.SaveComment(new SaveCommentRequest(wallPostId, waterId, comment) { PostedByType = PostedByType.Staff });

            return result.NotificationCollection.HasErrors() ? OnFailure(result.NotificationCollection) : OnSuccess();
        }

        #endregion
    }
}