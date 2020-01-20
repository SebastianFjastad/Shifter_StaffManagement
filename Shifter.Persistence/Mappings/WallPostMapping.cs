using FluentNHibernate.Mapping;
using Shifter.Domain.Model.Entities;

namespace Shifter.Persistence.Mappings
{
    public class WallPostMapping : ClassMap<WallPost>
    {
        public WallPostMapping()
        {
            Not.LazyLoad();

            Id(c => c.Id, "WallPostId");
            
            Map(c => c.PostedDate);
            References(c => c.PostedBy).Column("UserAccountId").Cascade.None();
            Map(c => c.Header);
            Map(c => c.Body);
            Map(c => c.RestaurantId);
            Map(c => c.PostedByType);
            Map(c => c.StaffTypeId);
            HasMany(c => c.Comments).KeyColumn("WallPostId").Not.LazyLoad(); ;

            HasManyToMany(c => c.SeenBy)
                .Table("UserWallPost")
                .ParentKeyColumn("WallPostId")
                .ChildKeyColumn("UserAccountId")
                .Not.LazyLoad();

            Table("WallPost");
        }
    }
}