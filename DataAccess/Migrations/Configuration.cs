using Domain.Models;

namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.Context.DbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataAccess.Context.DbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Offices.AddOrUpdate(
                p => p.Id,
                new Office { Id = 1, Name = "Cluj Office", Address = "Motilor xy, 100100 Cluj, RO", Phone = "0040 123 456" },
                new Office { Id = 2, Name = "London Office", Address = "Highway xy, 200200 London, UK", Phone = "0xyz 123 456" }
            );


            context.Departments.AddOrUpdate(
                p => p.Id,
                new Department { Id = 1, Name = ".Net", OfficeId = 1 },
                new Department { Id = 2, Name = "Javascript", OfficeId = 1 },
                new Department { Id = 3, Name = "Java", OfficeId = 2 },
                new Department { Id = 4, Name = "Php", OfficeId = 2 }
            );

            context.Projects.AddOrUpdate(
                p => p.Id,
                new Project() { Id = 1, Name = "Northern Safety", Status = "In progress", Duration = null, DepartmentId = 1 },
                new Project() { Id = 2, Name = "Pet Shop", Status = "Not Started", Duration = 3, DepartmentId = 1 },
                new Project() { Id = 3, Name = "HR Application", Status = "In progress", Duration = 5, DepartmentId = 1 },
                new Project() { Id = 4, Name = "Wedding planner", Status = "On hold", Duration = null, DepartmentId = 2 },
                new Project() { Id = 5, Name = "Todo MVC", Status = "On hold", Duration = 1, DepartmentId = 2 },
                new Project() { Id = 6, Name = "Todo without TypeScript", Status = "Done", Duration = 1, DepartmentId = 3 }
            );

            context.Positions.AddOrUpdate(
                p => p.Id,
                new Position() { Id = 1, Name = "Department Manager" },
                new Position() { Id = 2, Name = "Business Analyst" },
                new Position() { Id = 3, Name = "Developer" },
                new Position() { Id = 4, Name = "Project Manager" }
            );

            context.Employees.AddOrUpdate(
                p => p.Id,
                new Employee() { Id = 1, Name = "Ion Popescu1", Address = null, EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 2, Name = "Claudiu Cretu1", Address = null, EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 1, DepartmentId = 1 },
                new Employee() { Id = 3, Name = "Ion Popescu2", Address = null, EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 4, PositionId = 3, DepartmentId = 2 },
                new Employee() { Id = 4, Name = "Claudiu Cretu2", Address = null, EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 1, DepartmentId = 2 },
                new Employee() { Id = 5, Name = "Ion Popescu3", Address = null, EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 6, PositionId = 3, DepartmentId = 3 },
                new Employee() { Id = 6, Name = "Claudiu Cretu3", Address = null, EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 1, DepartmentId = 3 }
            );

            context.ProjectAllocations.AddOrUpdate(
                p => p.Id,
                new ProjectAllocation() { Id = 1, AllocationPercentage = 50, ProjectId = 1, EmployeeId = 1 },
                new ProjectAllocation() { Id = 2, AllocationPercentage = 25, ProjectId = 3, EmployeeId = 1 },
                new ProjectAllocation() { Id = 3, AllocationPercentage = 100, ProjectId = 4, EmployeeId = 3 },
                new ProjectAllocation() { Id = 4, AllocationPercentage = 50, ProjectId = 6, EmployeeId = 5 }
            );
        }
    }
}
