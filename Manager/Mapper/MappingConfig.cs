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
            CreateMap<Department, DepartmentInfo>();
            CreateMap<AddDepartmentInputInfo, Department>();

            CreateMap<Project, ProjectInfo>();
            CreateMap<AddProjectInputInfo, Project>();

            CreateMap<Employee, EmployeeInfo>();
        }
    }
}