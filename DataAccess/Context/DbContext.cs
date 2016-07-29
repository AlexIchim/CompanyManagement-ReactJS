using Domain.Models;
using System.Data.Entity;

namespace DataAccess.Context
{
    public class DbContext: System.Data.Entity.DbContext
    {
        public DbContext() : base("DbContext"){

        }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Office> Offices { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<EmployeeProject> EmployeeProjects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder){

            //Office restrictions
            modelBuilder.Entity<Office>().Property(o => o.Name).IsRequired();
            modelBuilder.Entity<Office>().Property(o => o.Address).IsRequired();
            modelBuilder.Entity<Office>().Property(o => o.PhoneNumber).IsRequired();

            //Department
            modelBuilder.Entity<Department>().Property(d => d.Name).IsRequired();

            // Employees
            modelBuilder.Entity<Employee>().HasRequired(t => t.Department)
                .WithMany(t => t.Employees)
                .HasForeignKey(d => d.DepartmentId);

            modelBuilder.Entity<Employee>().HasOptional(t => t.ManagedDepartment)
                .WithOptionalPrincipal(t => t.DepartmentManager);

            modelBuilder.Entity<Employee>().Property(e => e.Name).IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.Address).IsRequired();

            //Project
            modelBuilder.Entity<Project>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<Project>().Property(p => p.Name).IsRequired();

            //EmployeeProject

            modelBuilder.Entity<EmployeeProject>().HasKey(ep => new { ep.EmployeeId, ep.ProjectId });

        }
    }
}
