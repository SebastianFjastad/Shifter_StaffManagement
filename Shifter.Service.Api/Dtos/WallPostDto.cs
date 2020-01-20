using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Shifter.Service.Api.Dtos
{
    public class WallPostDto
    {
        public WallPostDto()
        {
            Comments = new List<CommentDto>();
            SeenByNames = new List<string>();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int RestaurantId { get; set; }

        [DataMember]
        public DateTime PostedDate { get; set; }

        [DataMember]
        public string Header { get; set; }

        [DataMember]
        public string Body { get; set; }

        [DataMember]
        public IEnumerable<CommentDto> Comments { get; set; }

        [DataMember]
        public IEnumerable<string> SeenByNames { get; set; }

        [DataMember]
        public int TotalStaff { get; set; }

        [DataMember]
        public PostedByType PostedByType { get; set; }

        /// <summary>
        /// Indicates for which type of staff the wall post is mean for (eg. only show to waiters).
        /// </summary>
        [DataMember]
        public int? StaffTypeId { get; set; }
    }

    public enum PostedByType
    {
        Staff,
        Manager
    }
}
