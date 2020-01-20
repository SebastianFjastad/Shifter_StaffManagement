using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class ConfirmNotificationReceivedRequest 
    {
        [DataMember(IsRequired = true)]
        public IDictionary<int, string> NotificationIds { get; set; }

        public ConfirmNotificationReceivedRequest(IDictionary<int, string> notificationIds)
        {
            NotificationIds = notificationIds;
        }
    }
}