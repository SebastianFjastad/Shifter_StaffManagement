using System.Data.Entity;
using Shifter.Security.Domain.Data.Mappings;
using Shifter.Security.Domain.Models;

namespace Shifter.Security.Domain.Data
{
    public class Context : DbContext
    {
        public Context()
            : base("Shifter")
        {

        }

        public DbSet<SecurityAccount> SecurityAccounts { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SecurityAccountMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
