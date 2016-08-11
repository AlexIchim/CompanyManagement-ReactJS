using Domain.Enums;
using Domain.Models;
using System;

namespace DataAccess.Migrations
{
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

            context.Offices.AddOrUpdate(
              o => o.Id,
              new Office { Id = 1, Name = "Cluj", Address = "Calea Motilor 62", PhoneNumber = "0364 101 203", Image = "http://www.acumenintegrat.ro/wp-content/uploads/2014/08/evozone-logo.gif" },
              new Office { Id = 2, Name = "Bucuresti", Address = "Calea Serban Voda Nr. 133", PhoneNumber = "021 301.33.00", Image = "http://www.acumenintegrat.ro/wp-content/uploads/2014/08/evozone-logo.gif" },
              new Office { Id = 3, Name = "London", Address = "Arundel House, 4 Palace Green,", PhoneNumber = "44 20 7937 9666", Image = "http://www.acumenintegrat.ro/wp-content/uploads/2014/08/evozone-logo.gif" }
            );

            Employee e1 = new Employee
            {
                Id = 1,
                Name = "Patricia Mazere",
                Address = "Str. Alexandru Vlahuta, Nr. 22 ",
                EmploymentDate = new DateTime(2016, 7, 4),
                ReleaseDate = new DateTime(2020, 6, 12),
                JobType = JobType.FullTime,
                PositionType = PositionType.TeamLead,
                DepartmentId = 1
            };

            Employee e2 = new Employee
            {
                Id = 2,
                Name = "Cristina Morarescu",
                Address = "Str. Izlazului, Nr. 6 ",
                EmploymentDate = new DateTime(2016, 7, 4),
                ReleaseDate = new DateTime(2018, 6, 10),
                JobType = JobType.PartTime4,
                PositionType = PositionType.Developer,
                DepartmentId = 1
            };

            Employee e3 = new Employee
            {
                Id = 3,
                Name = "Camelia Bodnar",
                Address = "Str. Horea, Nr. 2, Et. 2 ",
                EmploymentDate = new DateTime(2016, 7, 4),
                ReleaseDate = new DateTime(2019, 3, 4),
                JobType = JobType.FullTime,
                PositionType = PositionType.ProjectManager,
                DepartmentId = 2
            };

            Employee e4 = new Employee
            {
                Id = 4,
                Name = "Adrian Ardelean",
                Address = "Str. Eroilor, Nr. 122, Bl.C1",
                EmploymentDate = new DateTime(2009, 6, 10),
                ReleaseDate = new DateTime(2012, 6, 10),
                JobType = JobType.FullTime,
                PositionType = PositionType.Developer,
                DepartmentId = 2
            };

            Employee e5 = new Employee
            {
                Id = 5,
                Name = "Andrei Muresan",
                Address = "Cluj-Napoca",
                EmploymentDate = new DateTime(2007, 6, 10),
                ReleaseDate = new DateTime(2015, 6, 10),
                JobType = JobType.PartTime6,
                PositionType = PositionType.Developer,
                DepartmentId = 1
            };

            Employee e6 = new Employee
            {
                Id = 6,
                Name = "Aleona Popescu",
                Address = "Calea Victoriei, Nr.13",
                EmploymentDate = new DateTime(2012, 6, 10),
                ReleaseDate = new DateTime(2014, 6, 10),
                JobType = JobType.FullTime,
                PositionType = PositionType.DepartmentManager,
                DepartmentId = 1
            };
            Employee e7 = new Employee
            {
                Id = 7,
                Name = "Radu Crisan",
                Address = "str. Macelarului",
                EmploymentDate = new DateTime(2011, 6, 10),
                ReleaseDate = new DateTime(2013, 6, 10),
                JobType = JobType.FullTime,
                PositionType = PositionType.DepartmentManager,
                DepartmentId = 4
            };

            Employee e8 = new Employee
            {
                Id = 8,
                Name = "Razvan Stetcu",
                Address = "Str I.C. Bratianu",
                EmploymentDate = new DateTime(2011, 6, 10),
                ReleaseDate = new DateTime(2014, 6, 10),
                JobType = JobType.PartTime4,
                PositionType = PositionType.QA,
                DepartmentId = 4
            };
            Employee e9 = new Employee
            {
                Id = 9,
                Name = "Anna Dragomir",
                Address = "Sttr. Luna",
                EmploymentDate = new DateTime(2011, 6, 10),
                ReleaseDate = new DateTime(2011, 6, 10),
                JobType = JobType.FullTime,
                PositionType = PositionType.TeamLead,
                DepartmentId = 5
            };
            Employee e10 = new Employee
            {
                Id = 10,
                Name = "Claudiu Cretu",
                Address = "Str. Breaza",
                EmploymentDate = new DateTime(2011, 6, 10),
                ReleaseDate = new DateTime(2011, 6, 10),
                JobType = JobType.PartTime6,
                PositionType = PositionType.BA,
                DepartmentId = 5
            };
            Employee e11 = new Employee
            {
                Id = 11,
                Name = "Lucian Braescu",
                Address = "str. Plevnei",
                EmploymentDate = new DateTime(2011, 6, 10),
                ReleaseDate = new DateTime(2030, 6, 10),
                JobType = JobType.PartTime6,
                PositionType = PositionType.DepartmentManager,
                DepartmentId = 6
            };
            Employee e12 = new Employee
            {
                Id = 12,
                Name = "Gheorghe Enachescu",
                Address = "	Piata Blaga Lucian nr.1",
                EmploymentDate = new DateTime(2011, 6, 10),
                ReleaseDate = new DateTime(2016, 6, 10),
                JobType = JobType.FullTime,
                PositionType = PositionType.Developer,
                DepartmentId = 6
            };
            Employee e13 = new Employee
            {
                Id = 13,
                Name = "Armin van Burren",
                Address = "Emil Racovita 27",
                EmploymentDate = new DateTime(2011, 6, 10),
                ReleaseDate = new DateTime(2011, 6, 10),
                JobType = JobType.FullTime,
                PositionType = PositionType.DepartmentManager,
                DepartmentId = 6
            };

            Employee e14 = new Employee
            {
                Id = 14,
                Name = "Mihai Popa",
                Address = "Strada Napoca nr.2",
                EmploymentDate = new DateTime(2011, 6, 10),
                ReleaseDate = new DateTime(2017, 6, 10),
                JobType = JobType.FullTime,
                PositionType = PositionType.DepartmentManager,
                DepartmentId = 4
            };

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
                e1,
                e2,
                e3,
                e4,
                e5,
                e6,
                e7,
                e8,
                e9,
                e10,
                e11,
                e12,
                e13,
                e14
            );

            context.Projects.AddOrUpdate(
                p => p.Id,
                new Project { Id = 1, Name = "Northern Safety", Status = ProjectStatus.InProgress, DepartmentId = 1 },
                new Project { Id = 2, Name = "Street Remote", Status = ProjectStatus.Done, DepartmentId = 1 },
                new Project { Id = 3, Name = "Liquid Alarm", Status = ProjectStatus.OnHold, DepartmentId = 2 },
                new Project { Id = 4, Name = "Supersonic Mars", Status = ProjectStatus.Done, DepartmentId = 1 },
                new Project { Id = 5, Name = "Massive Donut", Status = ProjectStatus.InProgress, DepartmentId = 1 },
                new Project { Id = 6, Name = "Bulldozer Grim", Status = ProjectStatus.OnHold, DepartmentId = 1 }

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

            context.SaveChanges();

            context.Departments.Single(t => t.Id == 1).DepartmentManager =
                context.Employees.Single(t => t.Id == 11);
            context.Departments.Single(t => t.Id == 2).DepartmentManager =
                context.Employees.Single(t => t.Id == 7);
            context.Departments.Single(t => t.Id == 3).DepartmentManager =
                context.Employees.Single(t => t.Id == 6);
            context.Departments.Single(t => t.Id == 4).DepartmentManager =
                context.Employees.Single(t => t.Id == 13);

            context.SaveChanges();
        }
    }
}
