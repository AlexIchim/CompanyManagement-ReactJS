using Domain.Models;

namespace DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.Context.DbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataAccess.Context.DbContext context)
        {
            context.Departments.AddOrUpdate(
              p => p.Name,
              new Department { Name = ".NET" },
              new Department { Name = "JAVA" },
              new Department { Name = "PHP" }
            );

        }
    }
}
