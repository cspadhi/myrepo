using System.Data.Entity.ModelConfiguration;
using EFEndToEnd.Data;

namespace EFEndToEnd.DataAccess.Config
{
    public class CompanyConfig : EntityTypeConfiguration<Company>
    {
        public CompanyConfig()
        {
            // Primary Key
            HasKey(t => t.CompanyId);

            // Properties
            Property(t => t.CompanyName).HasMaxLength(50).IsRequired();

            // Relationships
            HasMany(t => t.Contacts)
                .WithMany(t => t.Companies)
                .Map(m =>
                {
                    m.ToTable("ContactCompanies");
                    m.MapLeftKey("CompanyId");
                    m.MapRightKey("ContactId");
                });
        }
    }
}
