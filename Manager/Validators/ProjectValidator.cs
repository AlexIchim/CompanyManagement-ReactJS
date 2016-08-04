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
    public class ProjectValidator : IProjectValidator
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

        public bool ValidateMemberInfo(MemberInfo inputInfo)
        {
            bool validation = ValidateString(inputInfo.Name) && ValidateString(inputInfo.Address) && ValidateDates(inputInfo.EmploymentDate, inputInfo.ReleaseDate) && ValidateInt(inputInfo.TotalAllocation);
            return validation;
        }

        public bool ValidateProjectInfo(ProjectInfo inputInfo)
        {
            bool validation = ValidateId(inputInfo.Id) && ValidateString(inputInfo.Name) && ValidateString(inputInfo.Status) && ValidateId(inputInfo.Duration) && ValidateInt(inputInfo.EmployeesNumber);
            return validation;
        }

        public bool ValidateAddProjectInfo(AddProjectInputInfo addInfo)
        {
            bool validation = ValidateString(addInfo.Name) && ValidateString(addInfo.Status) && ValidateNullableId(addInfo.Duration) && ValidateNullableId(addInfo.DepartmentId);
            return validation;
        }

        public bool ValidateUpdateProjectInfo(UpdateProjectInputInfo updateInfo)
        {
            bool validation = ValidateId(updateInfo.Id) && ValidateString(updateInfo.Name) && ValidateString(updateInfo.Status) && ValidateNullableId(updateInfo.Duration);
            return validation;
        }


    }
}
