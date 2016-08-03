using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager.InfoModels;
using Manager.InputInfoModels;

namespace Contracts
{
    public interface IOfficeValidator
    {
        bool ValidateId(int id);
        bool ValidateOfficeInfo(OfficeInfo inputInfo);

        bool ValidateAddOfficeInfo(AddOfficeInputInfo addInfo);

        bool ValidateUpdateOfficeInfo(UpdateOfficeInputInfo updateInfo);
    }
}
