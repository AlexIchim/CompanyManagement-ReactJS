using System;
using Manager.InfoModels;
using Manager.InputInfoModels;


namespace Contracts
{
    public interface IEmployeeProjectValidator
    {
        bool ValidateId(int id);

        bool ValidateEmployeeAllocationInfo(EmployeeAllocationInfo inputInfo);

        bool ValidateProjectOfAnEmployeeInfo(ProjectsOfAnEmployeeInfo inputInfo);

        bool ValidateUpdateAllocationInfo(UpdateAllocationInputInfo updateInfo);
    }
}
