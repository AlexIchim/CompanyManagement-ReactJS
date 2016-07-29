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

            Employee employee = new Employee()
            {
                Name = "Name1",
                Address = "Address1",
                EmploymentDate = new DateTime(2016, 10, 10),
                //ReleasedDate = new DateTime(2016, 11, 11),
                JobType = JobTypes.fullTime,
                Position = Position.Developer
            };

            Office office = new Office()
            {
                Name = "Office1",
                Address = "Address1",
                Phone = "0725658985",
                Image = new byte[50]
            };

            Department department = new Department()
            {
                Name = "Dept1",
                DepartmentManager = employee,
                Office = office
            };

            Project project = new Project()
            {
                Name = "Project1",
                Duration = "variable",
                Status = Status.NotStartedYet,
                Department = department
            };

            Assignment assignment = new Assignment()
            {
                Employee = employee,
                Project = project
            };

            context.Employees.AddOrUpdate(
                p => p.Id,
                employee
                );

            context.Offices.AddOrUpdate(
                p => p.Id,
                office
                );

            context.Departments.AddOrUpdate(
                p => p.Id,
                department
                );

            context.Projects.AddOrUpdate(
                p => p.Id,
                project
                );

            context.Assignments.AddOrUpdate(
                p => new { p.EmployeeId, p.ProjectId },
                assignment
                );

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
