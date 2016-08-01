using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Domain.Models;

namespace DataAccess.Mappings
{
    public class EmployeeMap : EntityTypeConfiguration<Employee>
    {
        public EmployeeMap()
        {
            // Relationships
            this.HasRequired(t => t.Department)
                .WithMany(t => t.Employees)
                .HasForeignKey(d => d.DepartmentId);

            this.HasOptional(t => t.ManagedDepartment)
                .WithOptionalPrincipal(t => t.DepartmentManager);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Email)
                .HasMaxLength(200);

            this.Property(t => t.Address)
                .HasMaxLength(300);

            this.Property(t => t.EmploymentHours)
                .IsRequired();

            this.Property(t => t.EmploymentDate)
                .IsRequired();


        }
    }
}
