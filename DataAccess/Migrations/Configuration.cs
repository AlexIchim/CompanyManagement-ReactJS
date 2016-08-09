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
                new Department { Id = 3, Name = "Java", OfficeId = 1 },
                new Department { Id = 4, Name = "Php", OfficeId = 1 },
                new Department { Id = 5, Name = "SAP", OfficeId = 1 },
                new Department { Id = 6, Name = "Perl", OfficeId = 1 },
                new Department { Id = 7, Name = "Python", OfficeId = 1 },
                new Department { Id = 8, Name = "C++", OfficeId = 1 },
                new Department { Id = 9, Name = "React", OfficeId = 1 },
                new Department { Id = 10, Name = "Design", OfficeId = 1 },
                new Department { Id = 11, Name = "Games", OfficeId = 1 },
                new Department { Id = 12, Name = ".Net 2", OfficeId = 1 },
                new Department { Id = 13, Name = ".Net 3", OfficeId = 1 },
                new Department { Id = 14, Name = ".Net 4", OfficeId = 1 },
                new Department { Id = 15, Name = ".Net 5", OfficeId = 1 },
                new Department { Id = 16, Name = ".Net 6", OfficeId = 1 },
                new Department { Id = 17, Name = ".Net 7", OfficeId = 1 },
                new Department { Id = 18, Name = ".Net 8", OfficeId = 1 },
                new Department { Id = 19, Name = ".Net 9", OfficeId = 1 },

                new Department { Id = 20, Name = "Java", OfficeId = 2 },
                new Department { Id = 21, Name = "Php", OfficeId = 2 }
            );

            context.Projects.AddOrUpdate(
                p => p.Id,
                new Project() { Id = 1, Name = "Northern Safety", Status = "In progress", Duration = null, DepartmentId = 1 },
                new Project() { Id = 2, Name = "Pet Shop", Status = "Not Started", Duration = 3, DepartmentId = 1 },
                new Project() { Id = 3, Name = "HR Application", Status = "In progress", Duration = 5, DepartmentId = 1 },
                new Project() { Id = 4, Name = "Wedding planner", Status = "On hold", Duration = null, DepartmentId = 2 },
                new Project() { Id = 5, Name = "Todo MVC", Status = "On hold", Duration = 1, DepartmentId = 2 },
                new Project() { Id = 6, Name = "Todo without TypeScript", Status = "Done", Duration = 1, DepartmentId = 3 },

                new Project() { Id = 10, Name = ".NET Project 1", Status = "In progress", Duration = null, DepartmentId = 1 },
                new Project() { Id = 11, Name = ".NET Project 2", Status = "In progress", Duration = null, DepartmentId = 1 },
                new Project() { Id = 12, Name = ".NET Project 3", Status = "In progress", Duration = null, DepartmentId = 1 },
                new Project() { Id = 13, Name = ".NET Project 4", Status = "In progress", Duration = null, DepartmentId = 1 },
                new Project() { Id = 14, Name = ".NET Project 5", Status = "In progress", Duration = null, DepartmentId = 1 },
                new Project() { Id = 15, Name = ".NET Project 6", Status = "In progress", Duration = null, DepartmentId = 1 },
                new Project() { Id = 16, Name = ".NET Project 7", Status = "In progress", Duration = null, DepartmentId = 1 },
                new Project() { Id = 17, Name = ".NET Project 8", Status = "In progress", Duration = null, DepartmentId = 1 },
                new Project() { Id = 18, Name = ".NET Project 9", Status = "In progress", Duration = null, DepartmentId = 1 },
                new Project() { Id = 19, Name = ".NET Project 10", Status = "In progress", Duration = null, DepartmentId = 1 },
                new Project() { Id = 20, Name = ".NET Project 11", Status = "In progress", Duration = null, DepartmentId = 1 },
                new Project() { Id = 21, Name = ".NET Project 12", Status = "In progress", Duration = null, DepartmentId = 1 },
                new Project() { Id = 22, Name = ".NET Project 13", Status = "In progress", Duration = null, DepartmentId = 1 },
                new Project() { Id = 23, Name = ".NET Project 14", Status = "In progress", Duration = null, DepartmentId = 1 },
                new Project() { Id = 24, Name = ".NET Project 15", Status = "In progress", Duration = null, DepartmentId = 1 }
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
                new Employee() { Id = 1, Name = "Ion Popescu1", Address = null, Email = "ipop@ipop.ro", EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 2, Name = "Claudiu Cretu1", Address = null, Email = "cretu@asd.ro", EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 1, DepartmentId = 1 },
                new Employee() { Id = 3, Name = "Ion Popescu2", Address = null, Email = "ipop2@ipop.ro", EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 4, PositionId = 3, DepartmentId = 2 },
                new Employee() { Id = 4, Name = "Claudiu Cretu2", Address = null, Email = "cretu2@iasd.ro", EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 1, DepartmentId = 2 },
                new Employee() { Id = 5, Name = "Ion Popescu3", Address = null, EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 6, PositionId = 3, DepartmentId = 3 },
                new Employee() { Id = 6, Name = "Claudiu Cretu3", Address = null, EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 1, DepartmentId = 3 },

                new Employee() { Id = 10, Name = "Ion Mitica 1", Address = null, Email = "ipop@ipop.ro", EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 11, Name = "Ion Mitica 2", Address = null, Email = "ipop@ipop.ro", EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 12, Name = "Ion Mitica 3", Address = null, Email = "ipop@ipop.ro", EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 13, Name = "Ion Mitica 4", Address = null, Email = "ipop@ipop.ro", EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 14, Name = "Ion Mitica 5", Address = null, Email = "ipop@ipop.ro", EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 15, Name = "Ion Mitica 6", Address = null, Email = "ipop@ipop.ro", EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 16, Name = "Ion Mitica 7", Address = null, Email = "ipop@ipop.ro", EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 17, Name = "Ion Mitica 8", Address = null, Email = "ipop@ipop.ro", EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 18, Name = "Ion Mitica 9", Address = null, Email = "ipop@ipop.ro", EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 19, Name = "Ion Mitica 10", Address = null, Email = "ipop@ipop.ro", EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 20, Name = "Ion Mitica 11", Address = null, Email = "ipop@ipop.ro", EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 21, Name = "Ion Mitica 12", Address = null, Email = "ipop@ipop.ro", EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 22, Name = "Ion Mitica 13", Address = null, Email = "ipop@ipop.ro", EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 23, Name = "Ion Mitica 14", Address = null, Email = "ipop@ipop.ro", EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 24, Name = "Ion Mitica 15", Address = null, Email = "ipop@ipop.ro", EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 25, Name = "Ion Mitica 16", Address = null, Email = "ipop@ipop.ro", EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 26, Name = "Ion Mitica 17", Address = null, Email = "ipop@ipop.ro", EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 27, Name = "Ion Mitica 18", Address = null, Email = "ipop@ipop.ro", EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 28, Name = "Ion Mitica 19", Address = null, Email = "ipop@ipop.ro", EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 29, Name = "Ion Mitica 20", Address = null, Email = "ipop@ipop.ro", EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 30, Name = "Ion Mitica 21", Address = null, Email = "ipop@ipop.ro", EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 31, Name = "Ion Mitica 22", Address = null, Email = "ipop@ipop.ro", EmploymentDate = DateTime.Now, ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 }
            );

            context.ProjectAllocations.AddOrUpdate(
                p => p.Id,
                new ProjectAllocation() { Id = 1, AllocationPercentage = 50, ProjectId = 1, EmployeeId = 1 },
                new ProjectAllocation() { Id = 2, AllocationPercentage = 25, ProjectId = 3, EmployeeId = 1 },
                new ProjectAllocation() { Id = 3, AllocationPercentage = 100, ProjectId = 4, EmployeeId = 3 },
                new ProjectAllocation() { Id = 4, AllocationPercentage = 50, ProjectId = 6, EmployeeId = 5 }
            );

            context.SaveChanges();

            // Assign the department managers
            context.Departments.Single(t => t.Id == 1).DepartmentManager =
                context.Employees.Single(t => t.Id == 2);
            context.Departments.Single(t => t.Id == 2).DepartmentManager =
                context.Employees.Single(t => t.Id == 4);
            context.Departments.Single(t => t.Id == 3).DepartmentManager =
                context.Employees.Single(t => t.Id == 6);
            context.SaveChanges();
        }
    }
}
