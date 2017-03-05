using System.Data.Entity.ModelConfiguration;
using EFEndToEnd.Data;

namespace EFEndToEnd.DataAccess.Config
{
    public class AddressConfig : ComplexTypeConfiguration<Address>
    {
        public AddressConfig()
        {
            Property(t => t.Street).HasColumnName("Street").HasMaxLength(50);
            Property(t => t.City).HasColumnName("City").HasMaxLength(50);
            Property(t => t.State).HasColumnName("State").HasMaxLength(2);
            Property(t => t.Zip).HasColumnName("Zip").HasMaxLength(10);
        }
    }
}
