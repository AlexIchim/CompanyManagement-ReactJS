
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
        private Employee dm;

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

        public OperationResult AddDepartment(AddDepartmentInputInfo inputInfo)
        {
            var newDepartment = _mapper.Map<Department>(inputInfo);
            _officeRepository.Add(newDepartment, inputInfo.DepartmentManagerId);
            return new OperationResult(true, Messages.SuccessfullyAddedDepartment);
        }
        public OperationResult Add(AddOfficeInputInfo inputInfo)
        {

            var newOffice = _mapper.Map<Office>(inputInfo);
            _officeRepository.AddOffice(newOffice);
            return new OperationResult(true, Messages.SuccessfullyAddedOffice);
        }

        public OperationResult UpdateDepartment(UpdateDepartmentInputInfo inputInfo)
        {
            var department = _officeRepository.GetById(inputInfo.Id);

            if (department == null)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingDepartment);
            }
            dm = _officeRepository.GetEmployeeById(inputInfo.DepartmentManagerId);

            department.Name = inputInfo.Name;
            department.DepartmentManager = dm;
            _officeRepository.Save();

            return new OperationResult(true, Messages.SuccessfullyUpdatedDepartment);
        }

        public OperationResult UpdateOffice(UpdateOfficeInputInfo inputInfo)
        {
            var office = _officeRepository.GetOfficeById(inputInfo.Id);

            if (office == null)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingOffice);
            }

            office.Name = inputInfo.Name;
            office.Address = inputInfo.Address;
            office.PhoneNumber = inputInfo.PhoneNumber;
            office.Image = inputInfo.Image;
            _officeRepository.Save();

            return new OperationResult(true, Messages.SuccessfullyUpdatedOffice);
        }
    }
}
