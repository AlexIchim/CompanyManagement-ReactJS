using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager.InfoModels;
using Manager.InputInfoModels;

namespace Contracts
{
    public interface IEmployeeValidator
    {
        bool ValidateId(int id);

        bool ValidateEmployeeInfo(EmployeeInfo inputInfo);

        bool ValidateAddEmployeeToDepartmentInfo(AddEmployeeToDepartmentInputInfo addInfo);

        bool ValidateAddEmployeeToProjectInfo(AddEmployeeToProjectInputInfo addInfo);

        bool ValidateUpdateEmployeeInfo(UpdateEmployeeInputInfo updateInfo);
    }
}
