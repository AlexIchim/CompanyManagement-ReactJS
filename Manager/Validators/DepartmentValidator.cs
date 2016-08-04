using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Manager.InfoModels;
using Manager.InputInfoModels;

namespace Manager.Services
{
    public class DepartmentValidator: IDepartmentValidator{

        public bool ValidateId(int id)
        {
            if (id <= 0)
                return false;
            return true;
        }
        private bool ValidateInt(int n)
        {
            if (n < 0)
                return false;
            return true;
        }

        private bool ValidateString(string s)
        {
            if (s == "" || s == null)
                return false;
            return true;
        }

        private bool ValidateNullableId(int? n)
        {
            if (n == null && n <= 0)
                return false;
            return true;
        }

        public bool ValidateDepartmentInfo(DepartmentInfo inputInfo)
        {
            bool validation = ValidateInt(inputInfo.Id) && ValidateString(inputInfo.Name) && ValidateInt(inputInfo.NbrOfEmployees) && ValidateInt(inputInfo.NbrOfProjects) && ValidateString(inputInfo.DepartmentManager);
            return validation;
        }

        public bool ValidateAddDepartmentInfo(AddDepartmentInputInfo addInfo)
        {
            bool validation = ValidateString(addInfo.Name) && ValidateId(addInfo.OfficeId) && ValidateNullableId(addInfo.DepartmentManagerId);
            return validation;
        }

        public bool ValidateUpdateDepartmentInfo(UpdateDepartmentInputInfo updateInfo)
        {
            bool validation = ValidateId(updateInfo.Id) && ValidateString(updateInfo.Name) && ValidateId(updateInfo.OfficeId) && ValidateNullableId(updateInfo.DepartmentManagerId);
            return validation;
        }

    }
}
