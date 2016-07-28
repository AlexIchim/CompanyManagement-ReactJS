using Domain.Models;
using System.Data.Entity;

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
            modelBuilder.Entity<Assignment>().HasKey(t => new { t.EmployeeId, t.ProjectId});
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
