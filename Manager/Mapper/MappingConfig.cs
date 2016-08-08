using System;
using System.Linq;
using System.Runtime.Remoting.Channels;
using AutoMapper;
using Domain.Models;
using Manager.Descriptors;
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
            ).ForMember(
                dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.GetDescription()));
            //CreateMap<Employee, MemberInfo>()
            //    .ForMember(
            //    dest=>dest.JobType, opt=>opt.MapFrom(src=>new JobTypeInfo
            //    {
            //        //Id = (int)src.JobType,
            //        Description = src.JobType.GetDescription()
            //    }));

            CreateMap<Employee, MemberInfo>().ForMember(
                dest => dest.JobType,
                opt => opt.MapFrom(src=> src.JobType.GetDescription()))
                .ForMember(
                e => e.PositionType,
                o => o.MapFrom(s => s.PositionType.GetDescription()));

            //CreateMap<Employee, MemberInfo>().AfterMap((e, mi) =>
            //    mi.JobType = new JobTypeInfo()
            //    {
            //        Id = 1,
            //        Description = "mnhbk"
            //    });


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

            CreateMap<AddOfficeInputInfo, Office>();

            CreateMap<EmployeeProject, ProjectsOfAnEmployeeInfo>();
            CreateMap<AddProjectInputInfo,Project>();
            CreateMap<AddEmployeeToProjectInputInfo, EmployeeProject>();
            CreateMap<UpdateProjectInputInfo, Project>();

            CreateMap<AddEmployeeToDepartmentInputInfo, Employee>()
                .ForMember(
                    dest => dest.JobType, opt => opt.MapFrom(src => EnumDescriptor.GetJobTypeEnum(src.JobType)))
                .ForMember(
                    dest2 => dest2.PositionType, opt2 => opt2.MapFrom(src2 => EnumDescriptor.GetPositionTypeEnum(src2.PositionType)));
                   
            CreateMap<Department, EmployeeInfo>();

            CreateMap<Employee, NotFullyAllocatedEmployeesInfo>();

        }
    }
}