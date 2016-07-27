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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
