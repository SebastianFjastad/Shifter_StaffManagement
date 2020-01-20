using FluentNHibernate.Mapping;
using Shifter.Managers.Domain.Models;

namespace Shifter.Managers.Domain.Mappings
{
    public class UserAccountMapping : ClassMap<UserAccount>
    {
        public UserAccountMapping()
        {
            base.Not.LazyLoad();

            Id(c => c.Id, "UserAccountId");
            
            Map(c => c.Username);
            Map(c => c.Password);
            Map(c => c.Salt);

            Table("UserAccount");
        }
    }
}