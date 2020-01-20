using System.Runtime.Serialization;
using Framework.Notifications;
using Framework.Services;

namespace Shifter.Service.Api.Responses
{
    [DataContract]
    public class GenericServiceResponse : IServiceResponse
    {
        public GenericServiceResponse()
        {
            NotificationCollection = NotificationCollection.CreateEmpty();
        }

        [DataMember]
        public NotificationCollection NotificationCollection { get; set; }
    }
}