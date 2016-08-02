using Manager.InputInfoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Validators
{
    static class ProjectValidator
    {
        public enum ValidationResult { Success, NotEmpty, TooLong }

        private static ValidationResult ValidateName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return ValidationResult.NotEmpty;
            }
            else if (name.Length > 100)
            {
                return ValidationResult.TooLong;
            }
            else return ValidationResult.Success;
        }

        private static ValidationResult ValidateStatus(string status)
        {
            if (String.IsNullOrEmpty(status))
            {
                return ValidationResult.NotEmpty;
            }
            else if (status.Length > 100)
            {
                return ValidationResult.TooLong;
            }
            else return ValidationResult.Success;
        }

        public static OperationResult ValidateProperties(string name, string status)
        {
            var result = new OperationResult(true, "");

            switch (ValidateName(name))
            {
                case ValidationResult.NotEmpty:
                    result.Message += Messages.ProjectNameEmpty;
                    result.Success = false;
                    break;
                case ValidationResult.TooLong:
                    result.Message += Messages.ProjectNameTooLong;
                    result.Success = false;
                    break;
            }
            switch (ValidateStatus(status))
            {
                case ValidationResult.NotEmpty:
                    result.Message += Messages.ProjectStatusEmpty;
                    result.Success = false;
                    break;
                case ValidationResult.TooLong:
                    result.Message += Messages.ProjectStatusTooLong;
                    result.Success = false;
                    break;
            }
            return result;
        }

        public static OperationResult Validate(AddProjectInputInfo info)
        {
            OperationResult result = ValidateProperties(info.Name, info.Status);
            if (result.Success)
            {
                return new Manager.OperationResult(true, Messages.SuccessfullyAddedProject);
            }
            result.Message = result.Message.Insert(0, Messages.ErrorWhileAddingProject);
            return result;
        }

        public static OperationResult Validate(UpdateProjectInputInfo info)
        {
            OperationResult result = ValidateProperties(info.Name, info.Status);

            if (info.Id < 0)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingProject + Messages.ProjectInvalidId);
            }
            else if (result.Success)
            {
                return new Manager.OperationResult(true, Messages.SuccessfullyUpdatedProject);
            }
            result.Message = result.Message.Insert(0, Messages.ErrorWhileUpdatingProject);
            return result;
        }
    }
}
