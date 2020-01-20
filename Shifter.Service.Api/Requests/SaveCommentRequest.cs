using System.Runtime.Serialization;
using Shifter.Service.Api.Dtos;

namespace Shifter.Service.Api.Requests
{
    [DataContract]
    public class SaveCommentRequest
    {
        public SaveCommentRequest(int wallPostId, int commentedById, string comment)
        {
            WallPostId = wallPostId;
            CommentedById = commentedById;
            Comment = comment;
        }

        [DataMember]
        public int WallPostId { get; set; }

        [DataMember]
        public int CommentedById { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public PostedByType PostedByType { get; set; }
    }
}