using FluentNHibernate.Mapping;
using Shifter.Domain.Model.Entities;

namespace Shifter.Persistence.Mappings
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

            Map(c => c.FirstName);
            Map(c => c.LastName);

            Map(c => c.ContactNumber);
            Map(c => c.EmailAddress);

            Table("UserAccount");
        }
    }
}