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
            CreateMap<AddDepartmentInputInfo, Department>();
        }
    }
}