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
    public class EmployeeValidator: IEmployeeValidator
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

        private bool ValidateDates(DateTime ed, DateTime? rd)
        {
            if (ed.Equals(ed.Millisecond))
                return false;
            if (rd != null && ed.CompareTo(rd) == 1) //>=
                return false;
            return true;
        }

        public bool ValidateEmployeeInfo(EmployeeInfo inputInfo)
        {
            bool validation = ValidateId(inputInfo.Id) && ValidateString(inputInfo.Name) && ValidateInt(inputInfo.TotalAllocation);
            return validation;
        }

        public bool ValidateAddEmployeeToDepartmentInfo(AddEmployeeToDepartmentInputInfo addInfo)
        {
            bool validation = ValidateId(addInfo.DepartmentId) && ValidateString(addInfo.Name) && ValidateString(addInfo.Address) && ValidateDates(addInfo.EmploymentDate, addInfo.ReleaseDate);
            return validation;
        }

        public bool ValidateAddEmployeeToProjectInfo(AddEmployeeToProjectInputInfo addInfo)
        {
            bool validation = ValidateId(addInfo.EmployeeId) && ValidateId(addInfo.ProjectId) && ValidateId(addInfo.Allocation);
            return validation;
        }

        public bool ValidateUpdateEmployeeInfo(UpdateEmployeeInputInfo updateInfo)
        {
            bool validation = ValidateId(updateInfo.Id) && ValidateString(updateInfo.Name) && ValidateString(updateInfo.Address) && ValidateDates(updateInfo.EmploymentDate, updateInfo.ReleaseDate) && ValidateId(updateInfo.DepartmentId);
            return validation;
        }


    }
}
