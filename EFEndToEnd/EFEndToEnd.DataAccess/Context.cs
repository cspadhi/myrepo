using EFEndToEnd.Data;
using System.Data.Entity;
using EFEndToEnd.DataAccess.Config;

namespace EFEndToEnd.DataAccess
{
    public class Context : DbContext
    {
        public Context()
            :base("Name=ContactsDb")
        {
            
        }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<ContactType> ContactTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AddressConfig());
            modelBuilder.Configurations.Add(new ContactConfig());
            modelBuilder.Configurations.Add(new CompanyConfig());
            modelBuilder.Configurations.Add(new ContactTypeConfig());
        }
    }
}
