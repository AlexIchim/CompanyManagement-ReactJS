using System.Linq;
using Contracts;
using Manager.InputInfoModels;

namespace Manager.Validators
{
    static class AllocationValidator
    {
        public static OperationResult Validate(IProjectRepository projectRepository,
            IEmployeeRepository employeeRepository,
            AddAllocationInputInfo info)
        {
            var employee = employeeRepository.GetById(info.EmployeeId);
            var project = projectRepository.GetById(info.ProjectId);

            if (project == null)
            {
                return new OperationResult(false, Messages.ErrorWhileAddingAllocation + " " + Messages.ProjectInvalidId);
            }

            if (employee == null)
            {
                return new OperationResult(false, Messages.ErrorWhileAddingAllocation + " " + Messages.EmployeeIdInvalid);
            }

            if (employee.ProjectAllocations.Count(pa => pa.ProjectId == info.ProjectId) != 0)
            {
                return new OperationResult(false, Messages.ErrorWhileAddingAllocation + " " + Messages.EmployeeAlreadyOnProject);
            }

            if (employee.ProjectAllocations.Select(pa => pa.AllocationPercentage).Sum() >
                100 - info.AllocationPercentage)
            {
                return new OperationResult(false, Messages.ErrorWhileAddingAllocation + " " + Messages.EmployeeFreeTimeNotEnough);
            }

            return new OperationResult(true, Messages.SuccessfullyAddedAllocation);
        }



        public static OperationResult Validate(IAllocationRepository allocationRepository,
                                               IEmployeeRepository employeeRepository,
                                               UpdateAllocationInputInfo info)
        {
            var allocation = allocationRepository.GetById(info.Id);
            if (allocation == null)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingAllocation + " " + Messages.AllocationIdInvalid);
            }

            var employee = employeeRepository.GetById(allocation.EmployeeId);

            int currentAllocation = allocation.AllocationPercentage;
            int currentTotal = employee.ProjectAllocations.Select(pa => pa.AllocationPercentage).Sum();

            if (currentTotal + info.AllocationPercentage - currentAllocation > 100)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingAllocation + " " + Messages.EmployeeFreeTimeNotEnough);
            }

            return new OperationResult(true, Messages.SuccessfullyUpdatedAllocation);
        }
    }
}
