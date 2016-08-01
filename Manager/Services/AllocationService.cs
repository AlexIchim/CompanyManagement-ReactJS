using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Domain.Models;
using Manager.InfoModels;
using Manager.InputInfoModels;

namespace Manager.Services
{
    public class AllocationService
    {
        private readonly IAllocationRepository _allocationRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public AllocationService(IMapper mapper, IAllocationRepository allocationRepository, IProjectRepository projectRepository, IEmployeeRepository employeeRepository)
        {
            _allocationRepository = allocationRepository;
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public OperationResult Add(AddAllocationInputInfo inputInfo)
        {
            var result = Validators.AllocationValidator.Validate(_projectRepository, _employeeRepository, inputInfo);

            if (result.Success == true && inputInfo.AllocationPercentage > 0)
            {
                _employeeRepository.GetById(inputInfo.EmployeeId).ProjectAllocations.Add(
                    _mapper.Map<ProjectAllocation>(inputInfo)
                );
                _employeeRepository.Save();
            }

            return result;
        }

        public OperationResult Update(UpdateAllocationInputInfo inputInfo)
        {
            var result = Validators.AllocationValidator.Validate(_allocationRepository, _employeeRepository, inputInfo);

            if (result.Success == true)
            {
                var allocation = _allocationRepository.GetById(inputInfo.Id);

                if (inputInfo.AllocationPercentage != 0)
                {
                    allocation.AllocationPercentage = inputInfo.AllocationPercentage;
                }
                else
                {
                    _allocationRepository.Delete(allocation);
                }

                _allocationRepository.Save();
            }

            return result;
        }

        public OperationResult Delete(int id)
        {
            var allocation = _allocationRepository.GetById(id);

            if (allocation == null)
            {
                return new OperationResult(false, Messages.ErrorWhileDeleteingAllocation + " " + Messages.AllocationIdInvalid);
            }

            _allocationRepository.Delete(allocation);
            _allocationRepository.Save();
            return new OperationResult(true, Messages.SuccessfullyDeletedAllocation);
        }
    }
}