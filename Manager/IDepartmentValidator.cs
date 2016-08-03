using Manager.InfoModels;
using Manager.InputInfoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IDepartmentValidator
    {
        bool ValidateId(int id);

        bool ValidateDepartmentInfo(DepartmentInfo inputInfo);

        bool ValidateAddDepartmentInfo(AddDepartmentInputInfo addInfo);

        bool ValidateUpdateDepartmentInfo(UpdateDepartmentInputInfo updateInfo);

    }
}
