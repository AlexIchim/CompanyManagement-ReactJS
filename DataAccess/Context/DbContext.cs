using Domain.Models;
using System.Data.Entity;
using DataAccess.Mappings;

namespace DataAccess.Context
{
    public class DbContext: System.Data.Entity.DbContext
    {
        public DbContext() : base("DbContext")
        {

        }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AssignmentMap());
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new EmployeeMap());
            modelBuilder.Configurations.Add(new OfficeMap());
            modelBuilder.Configurations.Add(new ProjectMap());


            /*modelBuilder.Entity<Project>()
                .HasRequired(t => t.Department)
                .WithMany()
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Department>()
                .HasRequired(t => t.Office)
                .WithMany()
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Department>()
                .HasRequired(t => t.DepartmentManager)
                .WithMany()
                .WillCascadeOnDelete(true);*/
            //.WillCascadeOnDelete(true);
            /*modelBuilder.Entity<Employee>()
                .HasRequired(s => s.Department);*/
            /*modelBuilder.Entity<Assignment>()
              .HasRequired(s => s.Project)
              .WithMany()
              .WillCascadeOnDelete(true);*/
            /*modelBuilder.Entity<Department>()
                .HasRequired(s => s.Projects);*/


            //base.OnModelCreating(modelBuilder);
        }
    }
}
