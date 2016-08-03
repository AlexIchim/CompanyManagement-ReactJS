using Manager.InputInfoModels;
using System;

namespace Manager.Validators
{
    static class DepartmentValidator
    {
        public static OperationResult Validate(AddDepartmentInputInfo info)
        {
            if (String.IsNullOrEmpty(info.Name))
            {
                return new OperationResult(false, Messages.ErrorWhileAddingDepartment_EmptyName);
            }
            if (info.Name.Length > 100)
            {
                return new Manager.OperationResult(false, Messages.ErrorWhileAddingDepartment_NameTooLong);
            }
            return new Manager.OperationResult(true, Messages.SuccessfullyAddedDepartment);
        }

        public static OperationResult Validate(UpdateDepartmentInputInfo info)
        {
            if (String.IsNullOrEmpty(info.Name))
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingDepartment_EmptyName);
            }
            if (info.Name.Length > 100)
            {
                return new Manager.OperationResult(false, Messages.ErrorWhileUpdatingDepartment_NameTooLong);
            }
            return new Manager.OperationResult(true, Messages.SuccessfullyUpdatedDepartment);
        }
    }
}
