using System.Runtime.Serialization;
using Framework.Extensions;

namespace Shifter.Service.Api.Dtos
{
    //public class ProfileSummary
    //{
    //    public ProfileSummary(int profileId, ProfileType profileType)
    //    {
    //        ProfileId = profileId;
    //        ProfileType = profileType;
    //    }

    //    public int ProfileId { get; set; }

    //    public ProfileType ProfileType { get; set; }

    //    public string GetAppUrl()
    //    {
    //        return ProfileType.Description();
    //    }
    //}
    [DataContract]
    public class StaffProfileSummary : ProfileSummary
    {
        public StaffProfileSummary(int profileId) : base(profileId)
        {
            ProfileType = ProfileType.Waiter;
            AppUrl = Config.WaiterAppUrl;
        }
    }


    [DataContract]
    public class ManagerProfileSummary : ProfileSummary
    {
        public ManagerProfileSummary(int profileId) : base(profileId)
        {
            ProfileType = ProfileType.Manager;
            AppUrl = Config.ManagerAppUrl;
        }
    }

    [DataContract]
    [KnownType(typeof(ManagerProfileSummary))]
    [KnownType(typeof(StaffProfileSummary))]
    public class ProfileSummary
    {
        public ProfileSummary(int profileId)
        {
            ProfileId = profileId;
        }

        [DataMember]
        public int ProfileId { get; set; }

        [DataMember]
        public ProfileType ProfileType { get; set; }

        [DataMember]
        public string AppUrl { get; set; }
    }
}