
using AutoMapper;
using Contracts;
using Domain.Models;
using Manager.InfoModels;
using Manager.InputInfoModels;
using System.Collections.Generic;

namespace Manager.Services
{
    public class OfficeService
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IMapper _mapper;
        private readonly IOfficeValidator _officeValidator;

        public OfficeService(IMapper mapper, IOfficeRepository officeRepository, IOfficeValidator officeValidator)
        {
            _officeRepository = officeRepository;
            _mapper = mapper;
            _officeValidator = officeValidator;
        }

        public IEnumerable<OfficeInfo> GetAll()
        {
            var offices = _officeRepository.GetAll();
            var officeInfos = _mapper.Map<IEnumerable<OfficeInfo>>(offices);

            return officeInfos;
        }

        public IEnumerable<DepartmentInfo> GetAllDepartmentsOfAnOffice(int officeId, int? pageSize, int? pageNr)
        {
            if (_officeValidator.ValidateId(officeId))
            {
                var departments = _officeRepository.GetAllDepartmentsOfAnOffice(officeId, pageSize, pageNr);
                var departmentInfos = _mapper.Map<IEnumerable<DepartmentInfo>>(departments);

                return departmentInfos;
            }
            return null;
        }

        public OperationResult Add(AddOfficeInputInfo inputInfo)
        {
            if (_officeValidator.ValidateAddOfficeInfo(inputInfo))
            {
                var newOffice = _mapper.Map<Office>(inputInfo);
                _officeRepository.AddOffice(newOffice);
                return new OperationResult(true, Messages.SuccessfullyAddedOffice);
            }
            return new OperationResult(false, Messages.ErrorAddingOffice);
        }

        public OperationResult UpdateOffice(UpdateOfficeInputInfo inputInfo)
        {
            if (_officeValidator.ValidateUpdateOfficeInfo(inputInfo))
            {
                var office = _officeRepository.GetOfficeById(inputInfo.Id);

                if (office != null)
                {                    
                    office.Name = inputInfo.Name;
                    office.Address = inputInfo.Address;
                    office.PhoneNumber = inputInfo.PhoneNumber;
                    office.Image = inputInfo.Image;
                    _officeRepository.Save();
                    return new OperationResult(true, Messages.SuccessfullyUpdatedOffice);
                }
            }
            return new OperationResult(false, Messages.ErrorWhileUpdatingOffice);
        }
    }
}
