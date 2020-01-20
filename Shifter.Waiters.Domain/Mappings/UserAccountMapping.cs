using FluentNHibernate.Mapping;
using Shifter.Waiters.Domain.Models;

namespace Shifter.Waiters.Domain.Mappings
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