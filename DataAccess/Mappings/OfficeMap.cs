using System.Data.Entity.ModelConfiguration;
using Domain.Models;

namespace DataAccess.Mappings
{
    class OfficeMap : EntityTypeConfiguration<Office>
    {
        public OfficeMap()
        { 
            //Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);
            this.Property(t => t.Address)
                .IsRequired()
                .HasMaxLength(300);
            this.Property(t => t.Phone)
                .IsRequired()
                .HasMaxLength(20);
            this.Property(t => t.Image)
                .HasMaxLength(2 * (1 << 20)); // 2 MB

            //Relationships
            
        }
    }
}
