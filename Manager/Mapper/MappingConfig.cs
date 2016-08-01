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
            CreateMap<Office, OfficeInfo>();

            CreateMap<AddOfficeInputInfo, Office>();

                

            CreateMap<AddDepartmentInputInfo, Department>();
            CreateMap<AddEmployeeInputInfo, Employee>();
            CreateMap<Project, ProjectInfo>().ForMember(projectInfo => projectInfo.NrMembers,
                                                        project => project.MapFrom(src => src.Assignments.Count));
            CreateMap<AddProjectInputInfo, Project>();

            CreateMap<Employee, EmployeeInfo>();

            CreateMap<Project, ProjectInfo>();
            CreateMap<Employee, EmployeeInfo>();
            CreateMap<AssignmentInfo, Assignment>();

        }
    }
}