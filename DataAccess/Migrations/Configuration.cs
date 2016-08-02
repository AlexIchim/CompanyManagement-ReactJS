using System.Collections.Generic;
using Domain.Models;

namespace DataAccess.Migrations {
    using System;
    using System.Data.Entity;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.Context.DbContext> {
        public Configuration() {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataAccess.Context.DbContext context) {

            Employee employee = new Employee() {
                Name = "Name1",
                Address = "Address1",
                EmploymentDate = new DateTime(2016, 10, 10),
                //ReleasedDate = new DateTime(2016, 11, 11),
                JobType = JobTypes.fullTime,
                Position = Position.Developer
            };

            Employee employee2 = new Employee() {
                Name = "Name2",
                Address = "Address2",
                EmploymentDate = new DateTime(2015, 10, 10),
                //ReleasedDate = new DateTime(2016, 11, 11),
                JobType = JobTypes.fullTime,
                Position = Position.Developer
            };

            Employee employee3 = new Employee() {
                Name = "Name3",
                Address = "Address3",
                EmploymentDate = new DateTime(2015, 10, 10),
                //ReleasedDate = new DateTime(2016, 11, 11),
                JobType = JobTypes.fullTime,
                Position = Position.Developer
            };

            ICollection<Employee> employeesList1 = new List<Employee>();
            employeesList1.Add(employee);
            employeesList1.Add(employee2);
            ICollection<Employee> employeesList2 = new List<Employee>();
            employeesList2.Add(employee3);

            Office office = new Office() {
                Name = "Office1",
                Address = "Address1",
                Phone = "0725658985",
                Image = new byte[50]
            };

            Office office2 = new Office() {
                Name = "Office2",
                Address = "Address2",
                Phone = "0725658985",
                Image = new byte[50],

            };

            Office office3 = new Office() {
                Name = "Office3",
                Address = "Address2",
                Phone = "0725658985",
                Image = new byte[50],

            };

            Department department = new Department() {
                Name = "Dept1",
                //Employees = employeesList1,
                DepartmentManager = employee,
                Office = office
            };

            Department department2 = new Department() {
                Name = "Dept2",
                //Employees = employeesList2,
                DepartmentManager = employee,
                Office = office
            };

            Department department3 = new Department() {
                Name = "Dept3",
                DepartmentManager = employee2,
                Office = office
            };

            Project project = new Project() {
                Name = "Project1",
                Duration = "variable",
                Status = Status.NotStartedYet,
                Department = department
            };

            Project project2 = new Project() {
                Name = "Project2",
                Duration = "3 months",
                Status = Status.NotStartedYet,
                Department = department
            };

            Project project3 = new Project() {
                Name = "Project3",
                Duration = "4 months",
                Status = Status.InProgress,
                Department = department3
            };

            Assignment assignment = new Assignment() {
                Employee = employee,
                Project = project,
                Allocation = 50
            };

            Assignment assignment2 = new Assignment() {
                Employee = employee2,
                Project = project2,
                Allocation = 30
            };

            Assignment assignment3 = new Assignment() {
                Employee = employee3,
                Project = project3,
                Allocation = 30
            };

            Assignment assignment4 = new Assignment()
            {
                Employee = employee2,
                Project = project,
                Allocation = 30
            };

            Employee employee4 = new Employee() {
                Name = "Name4",
                Address = "Address4",
                EmploymentDate = new DateTime(2018, 10, 10),
                //ReleasedDate = new DateTime(2016, 11, 11),
                Department = department2,
                JobType = JobTypes.fullTime,
                Position = Position.Developer
            };

            context.Employees.AddOrUpdate(
                p => p.Id,
                employee,
                employee2,
                employee3,
                employee4
                );

            context.Offices.AddOrUpdate(
                p => p.Id,
                office,
                office2,
                office3
                );
            context.Departments.AddOrUpdate(
                p => p.Id,
                department,
                department2,
                department3
                );

            context.Projects.AddOrUpdate(
                p => p.Id,
                project,
                project2,
                project3
                );

            context.Assignments.AddOrUpdate(
                p => new { p.EmployeeId, p.ProjectId },
                assignment,
                assignment2,
                assignment3,
                assignment4
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
