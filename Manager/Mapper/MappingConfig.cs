using AutoMapper;
using Domain.Models;
using Manager.InfoModels;
using Manager.InputInfoModels;

namespace Manager.Mapper
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<Department, DepartmentInfo>()
            .ForMember(
                departmentInfo => departmentInfo.NumberOfEmployees, 
                department => department.MapFrom(src => src.Employees.Count))
            .ForMember(
                 departmentInfo => departmentInfo.NumberOfProjects,
                 department => department.MapFrom(src => src.Projects.Count))
            .ForMember(
                 departmentInfo => departmentInfo.DepartmentManager,
                 department => department.MapFrom(src => src.DepartmentManager.Name)
            );
            CreateMap<AddDepartmentInputInfo, Department>();

            CreateMap<Office, OfficeInfo>();
            CreateMap<AddOfficeInputInfo, Office>();

            CreateMap<Employee, EmployeeInfo>()
                .ForMember(
                    employeeInfo => employeeInfo.JobType,
                    employee => employee.MapFrom(src => src.JobType.ToString()))
                .ForMember(
                    employeeInfo => employeeInfo.Position,
                    employee => employee.MapFrom(src => src.Position.ToString())); ;
            CreateMap<AddEmployeeInputInfo, Employee>();

            CreateMap<Project, ProjectInfo>()
            .ForMember(
                projectInfo => projectInfo.NrMembers,
                project => project.MapFrom(src => src.Assignments.Count)
            );
            CreateMap<AddProjectInputInfo, Project>();
            CreateMap<Assignment, ProjectMemberInfo>()
                .ForMember(
                    assignmentInfo => assignmentInfo.Name,
                    assignment => assignment.MapFrom(src => src.Employee.Name))
                .ForMember(
                    assignmentInfo => assignmentInfo.Position,
                    assignment => assignment.MapFrom(src => src.Employee.Position));
            CreateMap<AddAssignmentInputInfo, Assignment>();

        }
    }
}