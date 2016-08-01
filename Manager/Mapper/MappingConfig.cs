using AutoMapper;
using Domain.Models;
using Manager.InfoModels;
using Manager.InputInfoModels;

namespace Manager.Mapper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<AddDepartmentInputInfo, Department>();
            CreateMap<Office, OfficeInfo>();

            CreateMap<Employee, EmployeeInfo>();
            CreateMap<Employee, EmployeeAllocationInfo>();
            CreateMap<Project, ProjectInfo>().ForMember(
                pi => pi.EmployeesNumber,
                proj => proj.MapFrom(src => src.EmployeeProjects.Count)
            );
            CreateMap<Employee, MemberInfo>();


            CreateMap<Department, DepartmentInfo>().ForMember(
                 di => di.NbrOfEmployees,
                 d => d.MapFrom(src => src.Employees.Count)
             ).ForMember(
                 di => di.NbrOfProjects,
                 d => d.MapFrom(src => src.Projects.Count)
             ).ForMember(
                 di => di.DepartmentManager,
                 d => d.MapFrom(src => src.DepartmentManager.Name)
             );

            CreateMap<AddEmployeeToProjectInputInfo, EmployeeProject>();

            CreateMap<AddEmployeeToDepartmentInputInfo, Employee>();


        }
    }
}