using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Domain.Models;
using Manager.InfoModels;
using Manager.InputInfoModels;

namespace Manager.Services
{
    public class DepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IMapper mapper, IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public IEnumerable<DepartmentInfo> GetAll()
        {
            var departments = _departmentRepository.GetAll();
            var departmentInfos = _mapper.Map<IEnumerable<DepartmentInfo>>(departments);

            return departmentInfos;
        }

        public IEnumerable<DepartmentInfo> GetByOfficeId(int OfficeId)
        {
            var departments = _departmentRepository.GetByOfficeId(OfficeId);
            var departmentInfos = _mapper.Map<IEnumerable<DepartmentInfo>>(departments);

            return departmentInfos;
        }

        public OperationResult Add(AddDepartmentInputInfo inputInfo)
        {
            var newDepartment = _mapper.Map<Department>(inputInfo);
            _departmentRepository.Add(newDepartment);

            return new OperationResult(true, Messages.SuccessfullyAddedDepartment);
        }

        public OperationResult Update(UpdateDepartmentInputInfo inputInfo)
        {
            var department = _departmentRepository.GetById(inputInfo.Id);

            if (department == null)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingDepartment);
            }

            department.Name = inputInfo.Name;
            _departmentRepository.Save();

            return new OperationResult(true, Messages.SuccessfullyUpdatedDepartment);
        }
    }
}
