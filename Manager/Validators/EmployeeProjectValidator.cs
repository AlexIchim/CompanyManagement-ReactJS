using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Manager.InfoModels;
using Manager.InputInfoModels;

namespace Manager.Validators
{
    public class EmployeeProjectValidator: IEmployeeProjectValidator
    {
        public bool ValidateId(int id)
        {
            if (id <= 0)
                return false;
            return true;
        }

        private bool ValidateString(string s)
        {
            if (s == "" || s == null)
                return false;
            return true;
        }

        private bool ValidateInt(int n)
        {
            if (n < 0)
                return false;
            return true;
        }

        private bool ValidateNullableId(int? n)
        {
            if (n == null && n <= 0)
                return false;
            return true;
        }

        public bool ValidateEmployeeAllocationInfo(EmployeeAllocationInfo inputInfo)
        {
            bool validation = ValidateId(inputInfo.Id) && ValidateString(inputInfo.Name) && ValidateInt(inputInfo.Allocation);
            return validation;
        }

        public bool ValidateProjectOfAnEmployeeInfo(ProjectsOfAnEmployeeInfo inputInfo)
        {
            bool validation = ValidateId(inputInfo.Id) && ValidateString(inputInfo.Name) && ValidateId(inputInfo.Allocation);
            return validation;
        }

        public bool ValidateUpdateAllocationInfo(UpdateAllocationInputInfo updateInfo)
        {
            bool validation = ValidateId(updateInfo.EmployeeId) && ValidateId(updateInfo.ProjectId) && ValidateId(updateInfo.Allocation);
            return validation;
        }
    }
}
