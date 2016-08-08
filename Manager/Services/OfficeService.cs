using System;
using System.Collections.Generic;
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
            var officeInfo = _mapper.Map<IEnumerable<OfficeInfo>>(offices);

            return officeInfo;
        }

        public OfficeInfo GetById(int officeId)
        {
            var office = _officeRepository.GetById(officeId);
            var officeInfo = _mapper.Map<OfficeInfo>(office);

            return officeInfo;
        }

        public IEnumerable<DepartmentInfo> GetDepartmentsByOfficeId(int OfficeId, int? pageSize = null, int? pageNumber = null)
        {
            var departments = _officeRepository.GetDepartmentsByOfficeId(OfficeId, pageSize, pageNumber);
            var departmentInfos = _mapper.Map<IEnumerable<DepartmentInfo>>(departments);

            return departmentInfos;
        }

        public int GetDepartmentCountByOfficeId(int OfficeId)
        {
            var count = _officeRepository.GetDepartmentCountByOfficeId(OfficeId);
            return count;
        }

        public OperationResult Add(AddOfficeInputInfo inputInfo)
        {
            var result = Validators.OfficeValidator.Validate(inputInfo);

            if (result.Success == true)
            {
                _officeRepository.Add(_mapper.Map<Office>(inputInfo));
            }

            return result;
        }

        public OperationResult Update(UpdateOfficeInputInfo inputInfo)
        {
            var office = _officeRepository.GetById(inputInfo.Id);

            var result = Validators.OfficeValidator.Validate(inputInfo);

            if (office != null)
            {
                if (result.Success == true)
                {

                    office.Name = inputInfo.Name;
                    office.Phone = inputInfo.Phone;
                    office.Address = inputInfo.Address;
                    office.Image = inputInfo.Image;
                    _officeRepository.Save();
                    return new Manager.OperationResult(true, Messages.SuccessfullyUpdatedOffice);
                }
                else
                {
                    return result;
                }
            }
            else
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingOffice_InvalidId);
            }
        }
    }
}
