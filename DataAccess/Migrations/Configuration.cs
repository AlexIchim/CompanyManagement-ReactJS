using Domain.Enums;
using Domain.Models;
using System;

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

            context.Offices.AddOrUpdate(
              o => o.Id,
              new Office { Id = 1, Name = "Cluj", Address = "Calea Motilor 62", PhoneNumber = "0364 101 203" },
              new Office { Id = 2, Name = "Bucuresti", Address = "Calea Serban Voda Nr. 133", PhoneNumber = "021 301.33.00" },
              new Office { Id = 3, Name = "London", Address = "Arundel House, 4 Palace Green,", PhoneNumber = "44 20 7937 9666" }
            );

            context.Departments.AddOrUpdate(
                d => d.Id,
                new Department { Id = 1, Name = ".Net", OfficeId = 1 },
                new Department { Id = 2, Name = "Java", OfficeId = 1 },
                new Department { Id = 3, Name = "PHP", OfficeId = 1 },
                new Department { Id = 4, Name = "JavaScript", OfficeId = 1 },
                new Department { Id = 5, Name = ".Net", OfficeId = 2 },
                new Department { Id = 6, Name = "Java", OfficeId = 2 }
            );

            context.Employees.AddOrUpdate(
                e => e.Id,
                new Employee
                {
                    Id = 1,
                    Name = "Patricia",
                    Address = "Luna",
                    EmploymentDate = new DateTime(2011, 6, 10),
                    ReleaseDate = new DateTime(2011, 6, 12),
                    TotalAllocation = 20,
                    JobType = JobType.FullTime,
                    PositionType = PositionType.Developer,
                    DepartmentId = 1
                },
                new Employee
                {
                    Id = 2,
                    Name = "Cristina",
                    Address = "Fagaras",
                    EmploymentDate = new DateTime(2010, 6, 21),
                    ReleaseDate = new DateTime(2011, 6, 10),
                    TotalAllocation = 40,
                    JobType = JobType.PartTime4,
                    PositionType = PositionType.Developer,
                    DepartmentId = 1
                }, new Employee
                {
                    Id = 3,
                    Name = "Camelia",
                    Address = "Gheorgheni",
                    EmploymentDate = new DateTime(2011, 6, 10),
                    ReleaseDate = new DateTime(2011, 6, 11),
                    TotalAllocation = 30,
                    JobType = JobType.FullTime,
                    PositionType = PositionType.Developer,
                    DepartmentId = 3
                },
                new Employee
                {
                    Id = 4,
                    Name = "Adi",
                    Address = "Manastur",
                    EmploymentDate = new DateTime(2009, 6, 10),
                    ReleaseDate = new DateTime(2012, 6, 10),
                    TotalAllocation = 50,
                    JobType = JobType.FullTime,
                    PositionType = PositionType.Developer,
                    DepartmentId = 2
                }, new Employee
                {
                    Id = 5,
                    Name = "Andrei",
                    Address = "Cluj-Napoca",
                    EmploymentDate = new DateTime(2007, 6, 10),
                    ReleaseDate = new DateTime(2015, 6, 10),
                    TotalAllocation = 20,
                    JobType = JobType.PartTime6,
                    PositionType = PositionType.Developer,
                    DepartmentId = 3
                }, new Employee
                {
                    Id = 6,
                    Name = "Aleona",
                    Address = "Orastie",
                    EmploymentDate = new DateTime(2012, 6, 10),
                    ReleaseDate = new DateTime(2014, 6, 10),
                    TotalAllocation = 100,
                    JobType = JobType.FullTime,
                    PositionType = PositionType.BA,
                    DepartmentId = 2
                },
                new Employee
                {
                    Id = 7,
                    Name = "Radu",
                    Address = "str. Macelarului",
                    EmploymentDate = new DateTime(2011, 6, 10),
                    ReleaseDate = new DateTime(2011, 6, 10),
                    TotalAllocation = 69,
                    JobType = JobType.FullTime,
                    PositionType = PositionType.ProjectManager,
                    DepartmentId = 4
                }, new Employee
                {
                    Id = 8,
                    Name = "Razvan",
                    Address = "Alba-Iulia",
                    EmploymentDate = new DateTime(2011, 6, 10),
                    ReleaseDate = new DateTime(2011, 6, 10),
                    TotalAllocation = 45,
                    JobType = JobType.PartTime4,
                    PositionType = PositionType.QA,
                    DepartmentId = 4
                }, new Employee
                {
                    Id = 9,
                    Name = "Anna",
                    Address = "Lunacul",
                    EmploymentDate = new DateTime(2011, 6, 10),
                    ReleaseDate = new DateTime(2011, 6, 10),
                    TotalAllocation = 90,
                    JobType = JobType.FullTime,
                    PositionType = PositionType.TeamLead,
                    DepartmentId = 5
                }, new Employee
                {
                    Id = 10,
                    Name = "Sorinel",
                    Address = "Breaza",
                    EmploymentDate = new DateTime(2011, 6, 10),
                    ReleaseDate = new DateTime(2011, 6, 10),
                    TotalAllocation = 80,
                    JobType = JobType.PartTime6,
                    PositionType = PositionType.BA,
                    DepartmentId = 5
                }, new Employee
                {
                    Id = 11,
                    Name = "Lucian",
                    Address = "str. Devei",
                    EmploymentDate = new DateTime(2011, 6, 10),
                    ReleaseDate = new DateTime(2011, 6, 10),
                    TotalAllocation = 60,
                    JobType = JobType.PartTime6,
                    PositionType = PositionType.Developer,
                    DepartmentId = 6
                }, new Employee
                {
                    Id = 12,
                    Name = "Gheorghe",
                    Address = "Luncani",
                    EmploymentDate = new DateTime(2011, 6, 10),
                    ReleaseDate = new DateTime(2011, 6, 10),
                    TotalAllocation = 40,
                    JobType = JobType.FullTime,
                    PositionType = PositionType.Developer,
                    DepartmentId = 6
                }

            );

            context.Projects.AddOrUpdate(
                p => p.Id,
                new Project { Id = 1, Name = "A", Status = "Done", DepartmentId = 1 },
                new Project { Id = 2, Name = "B", Status = "Done", DepartmentId = 1 },
                new Project { Id = 3, Name = "C", Status = "Done", DepartmentId = 2 },
                new Project { Id = 4, Name = "D", Status = "Done", DepartmentId = 1 }
            );

            context.EmployeeProjects.AddOrUpdate(

                p => p.ProjectId,
                new EmployeeProject { EmployeeId = 1, ProjectId = 2, Allocation = 10 },
                new EmployeeProject { EmployeeId = 1, ProjectId = 1, Allocation = 10 },
                new EmployeeProject { EmployeeId = 2, ProjectId = 2, Allocation = 40 },
                new EmployeeProject { EmployeeId = 3, ProjectId = 2, Allocation = 20 },
                new EmployeeProject { EmployeeId = 3, ProjectId = 3, Allocation = 10 },
                new EmployeeProject { EmployeeId = 4, ProjectId = 4, Allocation = 50 },
                new EmployeeProject { EmployeeId = 5, ProjectId = 1, Allocation = 10 },
                new EmployeeProject { EmployeeId = 5, ProjectId = 2, Allocation = 7 },
                new EmployeeProject { EmployeeId = 5, ProjectId = 3, Allocation = 3 },
                new EmployeeProject { EmployeeId = 6, ProjectId = 4, Allocation = 100 },
                new EmployeeProject { EmployeeId = 7, ProjectId = 2, Allocation = 60 },
                new EmployeeProject { EmployeeId = 7, ProjectId = 3, Allocation = 9 },
                new EmployeeProject { EmployeeId = 8, ProjectId = 4, Allocation = 45 },
                new EmployeeProject { EmployeeId = 9, ProjectId = 2, Allocation = 20 },
                new EmployeeProject { EmployeeId = 9, ProjectId = 1, Allocation = 70 },
                new EmployeeProject { EmployeeId = 10, ProjectId = 3, Allocation = 80 },
                new EmployeeProject { EmployeeId = 11, ProjectId = 1, Allocation = 40 },
                new EmployeeProject { EmployeeId = 11, ProjectId = 3, Allocation = 20 },
                new EmployeeProject { EmployeeId = 12, ProjectId = 1, Allocation = 40 }
                );
        }
    }
}
