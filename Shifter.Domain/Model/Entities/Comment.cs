using System;
using Framework.Domain;

namespace Shifter.Domain.Model.Entities
{
    public class Comment : Entity
    {
        public virtual DateTime CommentDate { get; set; }

        public virtual UserAccount CommentBy { get; set; }

        public int WallPostId { get; set; }

        public virtual string Value { get; set; }
    }
}