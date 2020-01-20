using Shifter.Service.Api.Dtos;

namespace Shifter.Shared.WebClient.Models
{
    public class UserData
    {
        public int RestaurtantId { get; set; }
        public int ProfileId { get; set; }
        public ProfileType? ProfileType { get; set; }
        public string AppUrl { get; set; }
        public int UserAccountId { get; set; }
    }
}