using FluentNHibernate.Mapping;
using Shifter.Domain.Model.Entities;

namespace Shifter.Persistence.Mappings
{
    public class CommentMapping : ClassMap<Comment>
    {
        public CommentMapping()
        {
            Not.LazyLoad();

            Id(c => c.Id, "CommentId");
            
            Map(c => c.WallPostId);

            Map(c => c.Value);
            Map(c => c.CommentDate);
            References(c => c.CommentBy, "UserAccountId").Cascade.None();
            
            Table("Comment");
        }
    }
}