using System.Data.Entity.ModelConfiguration;
using Domain.Models;

namespace DataAccess.Mappings
{
    class PositionMap : EntityTypeConfiguration<Position>
    {
        public PositionMap()
        {
            //Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
