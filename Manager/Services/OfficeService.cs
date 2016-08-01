using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Domain.Models;
using Manager.InfoModels;
using Manager.InputInfoModels;

namespace Manager.Services
{
    public class OfficeService
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IMapper _mapper;

        public OfficeService(IMapper mapper, IOfficeRepository officeRepository)
        {
            _officeRepository = officeRepository;
            _mapper = mapper;
        }

        public IEnumerable<OfficeInfo> GetAll()
        {
            var offices = _officeRepository.GetAll();
            var officeInfos = _mapper.Map<IEnumerable<OfficeInfo>>(offices);

            return officeInfos;
        }

        public IEnumerable<DepartmentInfo> GetAllDepartmentsOfAnOffice(int officeId)
        {
            var newDep = _mapper.Map<int>(officeId);
            var departments = _officeRepository.GetAllDepartmentsOfAnOffice(newDep);
            var departmentInfos = _mapper.Map<IEnumerable<DepartmentInfo>>(departments);

            return departmentInfos;

        }

        public OperationResult Add(AddOfficeInputInfo inputInfo)
        {
            var newOffice = _mapper.Map<Office>(inputInfo);
            _officeRepository.AddOffice(newOffice);

            return new OperationResult(true, Messages.SuccessfullyAddedOffice);
        }
    }
}
