using System.Data.Entity.ModelConfiguration;
using Shifter.Security.Domain.Models;

namespace Shifter.Security.Domain.Data.Mappings
{
    /// <summary>
    /// Mapping for an article
    /// </summary>
    public class SecurityAccountMap : EntityTypeConfiguration<SecurityAccount>
    {
        /// <summary>
        /// Mapping for a SecurityAccount
        /// </summary>
        public SecurityAccountMap()
        {
            ToTable("SecurityAccount");
            //Property(m => m.Id).HasColumnName("UserId");
            Property(m => m.Username).HasColumnName("UserName");
        }
    }
}
