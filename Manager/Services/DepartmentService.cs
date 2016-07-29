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

        public IEnumerable<DepartmentInfo> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAllDepartments();
            var departmentInfos = _mapper.Map<IEnumerable<DepartmentInfo>>(departments);

            return departmentInfos;
        }

        public DepartmentInfo GetDepartmentById(int departmentId) {
            var department = _departmentRepository.GetDepartmentById(departmentId);
            var departmentInfo = _mapper.Map<DepartmentInfo>(department);

            return departmentInfo;
        }

        public OperationResult AddDepartment(AddDepartmentInputInfo inputInfo)
        {
            var newDepartment = _mapper.Map<Department>(inputInfo);
            _departmentRepository.AddDepartment(newDepartment);

            return new OperationResult(true, Messages.SuccessfullyAddedDepartment);
        }

        public OperationResult DeleteDepartment(int departmentId)
        {
            _departmentRepository.DeleteDepartment(departmentId);

            return new OperationResult(true, Messages.SuccessfullyDeletedDepartment);
        }

        public OperationResult UpdateDepartment(UpdateDepartmentInputInfo inputInfo)
        {
            var department = _departmentRepository.GetDepartmentById(inputInfo.Id);

            if (department == null)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingDepartment);
            }

            department.Name = inputInfo.Name;

            _departmentRepository.UpdateDepartment(department);

            //_departmentRepository.Save();

            return new OperationResult(true, Messages.SuccessfullyUpdatedDepartment);
        }
    }
}
