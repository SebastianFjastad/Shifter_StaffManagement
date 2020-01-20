using Framework.Domain;
using System;
using System.Collections.Generic;

namespace Shifter.Domain.Model.Entities
{
    public class WallPost : Entity
    {
        public virtual DateTime PostedDate { get; set; }

        public virtual UserAccount PostedBy { get; set; }

        public virtual string Header { get; set; }

        public virtual string Body { get; set; }

        public virtual int RestaurantId { get; set; }

        public virtual IEnumerable<Comment> Comments { get; set; }

        public virtual IEnumerable<UserAccount> SeenBy { get; set; }

        public virtual string PostedByType { get; set; }


        /// <summary>
        /// The staff type that the post is intended for
        /// </summary>
        public virtual int? StaffTypeId { get; set; }
    }
}
