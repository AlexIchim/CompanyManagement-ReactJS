using System.Globalization;
using System.IO;
using System.Reflection;
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

        private string MapPath(string seedFile)
        {
            var absolutePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
            var directoryName = Path.GetDirectoryName(absolutePath);
            var path = Path.Combine(directoryName, ".." + seedFile);

            return path;
        }

        private DateTime ParseDate(string str)
        {
            return DateTime.ParseExact(str, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }


        protected override void Seed(DataAccess.Context.DbContext context)
        {
            byte[] bytes;
            bytes = System.IO.File.ReadAllBytes(MapPath(@"\..\Migrations\SeedImages\cluj.jpg"));
            string officeImage1 = Convert.ToBase64String(bytes);
            bytes = System.IO.File.ReadAllBytes(MapPath(@"\..\Migrations\SeedImages\london.jpg"));
            string officeImage2 = Convert.ToBase64String(bytes);


            context.Offices.AddOrUpdate(
                p => p.Id,

                new Office { Id = 1, Name = "Cluj Office", Address = "Motilor xy, 100100 Cluj, RO", Phone = "0040 123 456", Image = officeImage1 },
                new Office { Id = 2, Name = "London Office", Address = "Highway xy, 200200 London, UK", Phone = "0000 123 456", Image = officeImage2 }
            );


            context.Departments.AddOrUpdate(
                p => p.Id,

                new Department { Id = 1, Name = ".Net", OfficeId = 1 },
                new Department { Id = 2, Name = "JavaScript", OfficeId = 1 },
                new Department { Id = 3, Name = "Java", OfficeId = 1 },
                new Department { Id = 4, Name = "Php", OfficeId = 1 },
                new Department { Id = 5, Name = "Games", OfficeId = 1 },
                new Department { Id = 6, Name = "Perl", OfficeId = 1 },
                new Department { Id = 7, Name = "Python", OfficeId = 1 },
                new Department { Id = 8, Name = "C++", OfficeId = 1 },
                new Department { Id = 9, Name = "React", OfficeId = 1 },
                new Department { Id = 10, Name = "Design", OfficeId = 1 },
                new Department { Id = 11, Name = "Lisp", OfficeId = 1 },
                new Department { Id = 12, Name = "Prolog", OfficeId = 1 },
                new Department { Id = 13, Name = "IT", OfficeId = 1 },
                new Department { Id = 14, Name = "F#", OfficeId = 1 },
                new Department { Id = 15, Name = "J#", OfficeId = 1 },

                new Department { Id = 16, Name = "JavaScript", OfficeId = 2 },
                new Department { Id = 17, Name = ".Net", OfficeId = 2 }
            );


            context.Projects.AddOrUpdate(
                p => p.Id,

                new Project() { Id = 1, Name = "Northern Safety", Status = "In progress", Duration = 0, DepartmentId = 1 },
                new Project() { Id = 2, Name = "Pet Shop", Status = "Not Started", Duration = 3, DepartmentId = 1 },
                new Project() { Id = 3, Name = "HR Application", Status = "In progress", Duration = 5, DepartmentId = 1 },
                new Project() { Id = 4, Name = "Wedding planner", Status = "On hold", Duration = 0, DepartmentId = 1 },
                new Project() { Id = 5, Name = ".NET Project 5", Status = "In progress", Duration = 2, DepartmentId = 1 },
                new Project() { Id = 6, Name = ".NET Project 6", Status = "On hold", Duration = 0, DepartmentId = 1 },
                new Project() { Id = 7, Name = ".NET Project 7", Status = "Not Started", Duration = 0, DepartmentId = 1 },
                new Project() { Id = 8, Name = ".NET Project 8", Status = "Done", Duration = 0, DepartmentId = 1 },
                new Project() { Id = 9, Name = ".NET Project 9", Status = "In progress", Duration = 0, DepartmentId = 1 },
                new Project() { Id = 10, Name = ".NET Project 10", Status = "Not Started", Duration = 0, DepartmentId = 1 },
                new Project() { Id = 11, Name = ".NET Project 11", Status = "In progress", Duration = 0, DepartmentId = 1 },
                new Project() { Id = 12, Name = ".NET Project 12", Status = "Done", Duration = 10, DepartmentId = 1 },
                new Project() { Id = 13, Name = ".NET Project 13", Status = "Done", Duration = 12, DepartmentId = 1 },
                new Project() { Id = 14, Name = ".NET Project 14", Status = "Done", Duration = 5, DepartmentId = 1 },
                new Project() { Id = 15, Name = ".NET Project 15", Status = "In progress", Duration = 5, DepartmentId = 1 },
                new Project() { Id = 16, Name = ".NET Project 16", Status = "Not Started", Duration = 0, DepartmentId = 1 },
                new Project() { Id = 17, Name = ".NET Project 17", Status = "In progress", Duration = 0, DepartmentId = 1 },
                new Project() { Id = 18, Name = ".NET Project 18", Status = "Done", Duration = 0, DepartmentId = 1 },
                new Project() { Id = 19, Name = ".NET Project 19", Status = "In progress", Duration = 0, DepartmentId = 1 },

                new Project() { Id = 20, Name = "Todo MVC", Status = "Done", Duration = 5, DepartmentId = 2 },
                new Project() { Id = 21, Name = "Todo without TypeScript", Status = "Done", Duration = 4, DepartmentId = 2 },
                new Project() { Id = 22, Name = "Frontend Sample 1", Status = "On Hold", Duration = 3, DepartmentId = 2 },
                new Project() { Id = 23, Name = "Frontend Sample 2", Status = "In Progress", Duration = 2, DepartmentId = 2 }
            );

            context.Positions.AddOrUpdate(
                p => p.Id,

                new Position() { Id = 1, Name = "Department Manager" },
                new Position() { Id = 2, Name = "Business Analyst" },
                new Position() { Id = 3, Name = "Developer" },
                new Position() { Id = 4, Name = "Project Manager" },
                new Position() { Id = 5, Name = "QA" }
            );

            context.Employees.AddOrUpdate(
                p => p.Id,

                new Employee() { Id = 1, Name = "Clau Manager", Address = "123, P-ta Marasti, 100100 C-N, RO", Email = "address@email.ro", EmploymentDate = ParseDate("13/02/2003"), ReleaseDate = null, EmploymentHours = 8, PositionId = 1, DepartmentId = 1 },
                new Employee() { Id = 2, Name = "John Manager", Address = "123, Manastur, 100100 C-N, RO", Email = "address@email.ro", EmploymentDate = ParseDate("13/02/2004"), ReleaseDate = null, EmploymentHours = 8, PositionId = 1, DepartmentId = 2 },
                new Employee() { Id = 3, Name = "Gabi Manager", Address = "123, Gheorgheni, 100100 C-N, RO", Email = "address@email.ro", EmploymentDate = ParseDate("13/02/2009"), ReleaseDate = null, EmploymentHours = 6, PositionId = 1, DepartmentId = 3 },

                new Employee() { Id = 4, Name = "Ion Dev", Address = "1234, Unirii, 100100 C-N, RO", Email = "address@email.ro", EmploymentDate = ParseDate("13/02/2002"), ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 5, Name = "Gabi Dev", Address = "1234, Unirii, 100100 C-N, RO", Email = "address@email.ro", EmploymentDate = ParseDate("13/02/2001"), ReleaseDate = null, EmploymentHours = 6, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 6, Name = "Ioana Dev", Address = "1234, Unirii, 100100 C-N, RO", Email = "address@email.ro", EmploymentDate = ParseDate("22/02/2000"), ReleaseDate = null, EmploymentHours = 4, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 7, Name = "Ana Dev", Address = "1234, Unirii, 100100 C-N, RO", Email = "address@email.ro", EmploymentDate = ParseDate("17/03/2007"), ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 8, Name = "Alin Dev", Address = "1234, Unirii, 100100 C-N, RO", Email = "address@email.ro", EmploymentDate = ParseDate("26/02/2003"), ReleaseDate = null, EmploymentHours = 6, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 9, Name = "Sergiu Dev", Address = "1234, Unirii, 100100 C-N, RO", Email = "address@email.ro", EmploymentDate = ParseDate("04/02/2004"), ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 10, Name = "Mitica Dev", Address = "1234, Unirii, 100100 C-N, RO", Email = "address@email.ro", EmploymentDate = ParseDate("13/02/2007"), ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 11, Name = "Ion QA", Address = "1234, Unirii, 100100 C-N, RO", Email = "address@email.ro", EmploymentDate = ParseDate("13/02/2007"), ReleaseDate = null, EmploymentHours = 6, PositionId = 5, DepartmentId = 1 },
                new Employee() { Id = 12, Name = "Ion BA", Address = "1234, Unirii, 100100 C-N, RO", Email = "address@email.ro", EmploymentDate = ParseDate("03/07/2007"), ReleaseDate = null, EmploymentHours = 4, PositionId = 2, DepartmentId = 1 },
                new Employee() { Id = 13, Name = "Mitica QA", Address = "1234, Unirii, 100100 C-N, RO", Email = "address@email.ro", EmploymentDate = ParseDate("13/11/2007"), ReleaseDate = null, EmploymentHours = 8, PositionId = 5, DepartmentId = 1 },
                new Employee() { Id = 14, Name = "Mitica BA", Address = "1234, Unirii, 100100 C-N, RO", Email = "address@email.ro", EmploymentDate = ParseDate("13/02/2007"), ReleaseDate = null, EmploymentHours = 6, PositionId = 2, DepartmentId = 1 },
                new Employee() { Id = 15, Name = "Vlad PM", Address = "1234, Unirii, 100100 C-N, RO", Email = "address@email.ro", EmploymentDate = ParseDate("13/02/2009"), ReleaseDate = null, EmploymentHours = 4, PositionId = 4, DepartmentId = 1 },
                new Employee() { Id = 16, Name = "Alex Dev", Address = "1234, Unirii, 100100 C-N, RO", Email = "address@email.ro", EmploymentDate = ParseDate("15/02/2007"), ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 17, Name = "Sergiu Dev", Address = "1234, Unirii, 100100 C-N, RO", Email = "address@email.ro", EmploymentDate = ParseDate("18/02/2007"), ReleaseDate = null, EmploymentHours = 6, PositionId = 3, DepartmentId = 1 },
                new Employee() { Id = 18, Name = "Traian PM", Address = "1234, Unirii, 100100 C-N, RO", Email = "address@email.ro", EmploymentDate = ParseDate("19/12/2012"), ReleaseDate = null, EmploymentHours = 4, PositionId = 4, DepartmentId = 1 },
                new Employee() { Id = 19, Name = "Emil QA", Address = "1234, Unirii, 100100 C-N, RO", Email = "address@email.ro", EmploymentDate = ParseDate("17/02/2007"), ReleaseDate = null, EmploymentHours = 8, PositionId = 5, DepartmentId = 1 },
                new Employee() { Id = 20, Name = "Claudia BA", Address = "1234, Unirii, 100100 C-N, RO", Email = "address@email.ro", EmploymentDate = ParseDate("13/03/2007"), ReleaseDate = null, EmploymentHours = 6, PositionId = 3, DepartmentId = 1 },

                new Employee() { Id = 21, Name = "Ion Dev", Address = "1234, Unirii, 100100 C-N, RO", Email = "address@email.ro", EmploymentDate = ParseDate("13/02/2001"), ReleaseDate = null, EmploymentHours = 8, PositionId = 3, DepartmentId = 2 },
                new Employee() { Id = 22, Name = "Gabi Dev", Address = "1234, Unirii, 100100 C-N, RO", Email = "address@email.ro", EmploymentDate = ParseDate("13/02/2002"), ReleaseDate = ParseDate("13/02/2009"), EmploymentHours = 6, PositionId = 3, DepartmentId = 2 },
                new Employee() { Id = 23, Name = "Ioana Dev", Address = "1234, Unirii, 100100 C-N, RO", Email = "address@email.ro", EmploymentDate = ParseDate("13/02/2000"), ReleaseDate = null, EmploymentHours = 4, PositionId = 3, DepartmentId = 2 }
            );

            context.ProjectAllocations.AddOrUpdate(
                p => p.Id,

                new ProjectAllocation() { Id = 1, AllocationPercentage = 30, ProjectId = 1, EmployeeId = 4 },
                new ProjectAllocation() { Id = 2, AllocationPercentage = 30, ProjectId = 1, EmployeeId = 5 },
                new ProjectAllocation() { Id = 3, AllocationPercentage = 50, ProjectId = 1, EmployeeId = 6 },
                new ProjectAllocation() { Id = 4, AllocationPercentage = 50, ProjectId = 1, EmployeeId = 7 },
                new ProjectAllocation() { Id = 5, AllocationPercentage = 100, ProjectId = 1, EmployeeId = 8 },
                new ProjectAllocation() { Id = 6, AllocationPercentage = 50, ProjectId = 1, EmployeeId = 9 },
                new ProjectAllocation() { Id = 7, AllocationPercentage = 100, ProjectId = 1, EmployeeId = 10 },
                new ProjectAllocation() { Id = 8, AllocationPercentage = 30, ProjectId = 2, EmployeeId = 4 },
                new ProjectAllocation() { Id = 9, AllocationPercentage = 30, ProjectId = 2, EmployeeId = 5 },
                new ProjectAllocation() { Id = 10, AllocationPercentage = 30, ProjectId = 2, EmployeeId = 6 },
                new ProjectAllocation() { Id = 11, AllocationPercentage = 30, ProjectId = 2, EmployeeId = 7 },
                new ProjectAllocation() { Id = 12, AllocationPercentage = 100, ProjectId = 2, EmployeeId = 11 },
                new ProjectAllocation() { Id = 13, AllocationPercentage = 50, ProjectId = 2, EmployeeId = 12 },
                new ProjectAllocation() { Id = 14, AllocationPercentage = 100, ProjectId = 2, EmployeeId = 13 },
                new ProjectAllocation() { Id = 15, AllocationPercentage = 40, ProjectId = 3, EmployeeId = 4 },
                new ProjectAllocation() { Id = 16, AllocationPercentage = 40, ProjectId = 3, EmployeeId = 5 }
            );

            context.SaveChanges();

            // Assign the department managers
            context.Departments.Single(t => t.Id == 1).DepartmentManager =
                context.Employees.Single(t => t.Id == 1);
            context.Departments.Single(t => t.Id == 2).DepartmentManager =
                context.Employees.Single(t => t.Id == 2);
            context.Departments.Single(t => t.Id == 3).DepartmentManager =
                context.Employees.Single(t => t.Id == 3);

            context.SaveChanges();
        }
    }
}
