﻿using System.Linq;
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
            CreateMap<Department, DepartmentInfo>().ForMember(
                p => p.EmployeeCount,
                opts => opts.MapFrom(src => src.Employees.Count)
            ).ForMember(
                p => p.ProjectCount,
                opts => opts.MapFrom(src => src.Projects.Count)
            ).ForMember(
                p => p.DepartmentManagerName,
                opts => opts.MapFrom(src => src.DepartmentManager.Name)
            );


            CreateMap<Employee, EmployeeInfo>().ForMember(
                p => p.TotalAllocation,
                opts => opts.MapFrom(src => src.ProjectAllocations.Select(s => s.AllocationPercentage).Sum())
            ).ForMember(
                p => p.PositionName,
                opts => opts.MapFrom(src => src.Position.Name)
            );


            CreateMap<AddDepartmentInputInfo, Department>();

            CreateMap<Position, PositionInfo>();
            CreateMap<Project, ProjectInfo>().ForMember(
                p => p.MemberCount,
                opts => opts.MapFrom(src => src.Allocations.Count)
            );

            CreateMap<Office, OfficeInfo>();

            CreateMap<AddPositionInputInfo, Position>();
            CreateMap<AddAllocationInputInfo, ProjectAllocation>();
            CreateMap<AddEmployeeInputInfo, Employee>();
            CreateMap<AddOfficeInputInfo, Office>();
            CreateMap<AddProjectInputInfo, Project>();
        }
    }
}