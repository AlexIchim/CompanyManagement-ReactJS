using Manager.InputInfoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Validators
{
    static class DepartmentValidator
    {
        private static OperationResult ValidateName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return new OperationResult(false, Messages.ErrorWhileAddingDepartment_EmptyName);
            }
            else if (name.Length > 100)
            {
                return new Manager.OperationResult(false, Messages.ErrorWhileAddingDepartment_NameTooLong);
            }
            else return null;
        }
        public static OperationResult Validate(AddDepartmentInputInfo info)
        {
            OperationResult result = ValidateName(info.Name);
            if (result == null)
            {
                return new Manager.OperationResult(true, Messages.SuccessfullyAddedDepartment);
            }
            return result;
        }

        public static OperationResult Validate(UpdateDepartmentInputInfo info)
        {
            OperationResult result = ValidateName(info.Name);
            if (result == null)
            {
                return new Manager.OperationResult(true, Messages.SuccessfullyUpdatedDepartment);
            }
            return result;
        }
    }
}
