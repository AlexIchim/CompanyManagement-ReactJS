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
            CreateMap<Office, OfficeInfo>();
            CreateMap<AddOfficeInputInfo, Office>();

            CreateMap<Department, DepartmentInfo>();
            CreateMap<Employee, EmployeeInfo>();
            CreateMap<AddDepartmentInputInfo, Department>();
            CreateMap<AddEmployeeInputInfo, Employee>();
            CreateMap<Project, ProjectInfo>().ForMember(projectInfo => projectInfo.NrMembers,
                                                        project => project.MapFrom(src => src.Assignments.Count));
            CreateMap<AddProjectInputInfo, Project>();
            CreateMap<AssignmentInfo, Assignment>();

        }
    }
}