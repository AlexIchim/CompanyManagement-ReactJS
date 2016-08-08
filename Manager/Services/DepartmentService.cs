using AutoMapper;
using Contracts;
using Domain.Enums;
using Domain.Models;
using Manager.InfoModels;
using Manager.InputInfoModels;
using System.Collections.Generic;

namespace Manager.Services
{
    public class DepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IDepartmentValidator _departmentValidator;

        public DepartmentService(IMapper mapper, IDepartmentRepository departmentRepository, IDepartmentValidator departmentValidator)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _departmentValidator = departmentValidator;
        }

        public IEnumerable<DepartmentInfo> GetAll()
        {
            var departments = _departmentRepository.GetAll();
            var departmentInfos = _mapper.Map<IEnumerable<DepartmentInfo>>(departments);

            return departmentInfos;
        }

        /*public OperationResult Update(UpdateDepartmentInputInfo inputInfo)
        {
            if (_departmentValidator.ValidateUpdateDepartmentInfo(inputInfo))
            {
                var department = _departmentRepository.GetDepartmentById(inputInfo.Id);

            if (department == null)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingDepartment);
            }

            department.Name = inputInfo.Name;
            _departmentRepository.Save();

            return new OperationResult(true, Messages.SuccessfullyUpdatedDepartment);
        }*/



        public OperationResult AddDepartment(AddDepartmentInputInfo inputInfo)
        {
            if (_departmentValidator.ValidateAddDepartmentInfo(inputInfo))
            {
                var newDepartment = _mapper.Map<Department>(inputInfo);

                var depManagerId = inputInfo.DepartmentManagerId;

                if (!_departmentRepository.DepartmentWithNameExists(inputInfo.Name))
                {
                    if (_departmentRepository.IsDepartmentManager((int) depManagerId))
                    {
                        _departmentRepository.AddDepartment(newDepartment, (int) inputInfo.DepartmentManagerId);
                        return new OperationResult(true, Messages.SuccessfullyAddedDepartment);
                    }
                }
                return new OperationResult(false, Messages.ErrorAddingDepartment);
            }
            return new OperationResult(false, Messages.ErrorAddingDepartment);
        }

        public IEnumerable<EmployeeInfo> GetAllDepartmentManagers()
        {
         
            var departmentManagers= _departmentRepository.GetAllDepartmentManagers();
            var departmentManagersInfo= _mapper.Map<IEnumerable<EmployeeInfo>>(departmentManagers);

            return departmentManagersInfo;
        }

        public OperationResult UpdateDepartment(UpdateDepartmentInputInfo inputInfo)
        {
            if (_departmentValidator.ValidateUpdateDepartmentInfo(inputInfo))
            {
                    var department = _departmentRepository.GetDepartmentById(inputInfo.Id);

                if (department == null)
                {
                    return new OperationResult(false, Messages.ErrorWhileUpdatingDepartment);
                }

                var dm = _departmentRepository.GetEmployeeById(inputInfo.DepartmentManagerId);

                if (dm != null && dm.PositionType == PositionType.DepartmentManager)
                 {
                    department.DepartmentManager = dm;
                   
                }
                department.Name = inputInfo.Name;
                _departmentRepository.Save();
                return new OperationResult(true, Messages.SuccessfullyUpdatedDepartment);
            }
            return new OperationResult(false, Messages.ErrorWhileUpdatingDepartment);
        }
    }
}
