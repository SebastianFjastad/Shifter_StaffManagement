using System;

namespace Shifter.Service.Api.Dtos
{
    public class CommentDto
    {
        public virtual DateTime CommentDate { get; set; }

        public virtual string CommenterName { get; set; }

        public virtual string Value { get; set; }
    }
}