using Domain.Models;
using System.Data.Entity;

namespace DataAccess.Context
{
    public class DbContext: System.Data.Entity.DbContext
    {
        public DbContext() : base("DbContext")
        {

        }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Office> Offices { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<EmployeeProject> EmployeeProjects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeProject>().HasKey(ep => new { ep.EmployeeId, ep.ProjectId });

        }
    }
}
