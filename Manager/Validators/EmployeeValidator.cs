using System;
using Manager.InputInfoModels;
using Manager.Services;

namespace Manager.Validators
{
    class EmployeeValidator
    {
        public enum NameValidationResult{Success, EmptyName, TooLongName };
        public enum EmailValidationResult { Success, TooLongEmail};
        public enum AddressValidationResult { Success, TooLongAddress};
        public enum EmploymentHoursValidationResult { Success, NegativeEmploymentHours, TooManyEmploymentHours };
        public enum EmploymentDateValidationResult { Success, NullEmploymentDate};
        public enum PositionIdValidationResult { Success, NullPositionId};
        public enum DepartmentIdValidationResult { Success, NullDepartmentId};

        private static NameValidationResult ValidateName(string name)
        {
            if(String.IsNullOrEmpty(name))
            {
                return NameValidationResult.EmptyName;
            }
            else if(name.Length > 100)
            {
                return NameValidationResult.TooLongName;
            }
            return NameValidationResult.Success;
        }

        private static EmailValidationResult ValidateEmail(string email)
        {
            if(email.Length > 200)
            {
                return EmailValidationResult.TooLongEmail;
            }
            return EmailValidationResult.Success;
        }

        private static AddressValidationResult ValidateAddress(string address)
        {
            if(address.Length > 300)
            {
                return AddressValidationResult.TooLongAddress;
            }
            return AddressValidationResult.Success;
        }

        private static EmploymentHoursValidationResult ValidateEmploymentHours(int employmentHours)
        {
            if(employmentHours <= 0)
            {
                return EmploymentHoursValidationResult.NegativeEmploymentHours;
            }
            else if(employmentHours > 8)
            {
                return EmploymentHoursValidationResult.TooManyEmploymentHours;
            }
            return EmploymentHoursValidationResult.Success;
        }

        private static EmploymentDateValidationResult ValidateEmploymentDate(DateTime? employmentDate)
        {
            if(employmentDate == null)
            {
                return EmploymentDateValidationResult.NullEmploymentDate;
            }
            return EmploymentDateValidationResult.Success;
        }

        public static PositionIdValidationResult ValidatePositionId(int? positionId)
        {
            if(positionId == null)
            {
                return PositionIdValidationResult.NullPositionId;
            }
            return PositionIdValidationResult.Success;
        }

        public static DepartmentIdValidationResult ValidateDepartmentId(int? departmentId)
        {
            if(departmentId == null)
            {
                return DepartmentIdValidationResult.NullDepartmentId;
            }
            return DepartmentIdValidationResult.Success;
        }

        public static OperationResult Validate(AddEmployeeInputInfo info)
        {
            var resultOfNameValidation = ValidateName(info.Name);
            switch (resultOfNameValidation)
            {
                case NameValidationResult.EmptyName: return new Manager.OperationResult(false, Messages.ErrorWhileAddingEmployee_EmptyEmployeeName);
                case NameValidationResult.TooLongName: return new Manager.OperationResult(false, Messages.ErrorWhileAddingEmployee_TooLongEmployeeName);
            }

            var resultOfEmailValidation = ValidateEmail(info.Email);
            if(resultOfEmailValidation == EmailValidationResult.TooLongEmail)
                return new Manager.OperationResult(false, Messages.ErrorWhileAddingEmployee_TooLongEmployeeEmail);

            var resultOfAddressValidation = ValidateAddress(info.Address);
            if(resultOfAddressValidation == AddressValidationResult.TooLongAddress)
            {
                return new Manager.OperationResult(false, Messages.ErrorWhileAddingEmployee_TooLongEmployeeAddress);
            }

            var resultOfEmploymentHoursValidation = ValidateEmploymentHours(info.EmploymentHours);
            switch (resultOfEmploymentHoursValidation)
            {
                case EmploymentHoursValidationResult.NegativeEmploymentHours: return new Manager.OperationResult(false, Messages.ErrorWhileAddingEmployee_NegativeEmploymentHours);
                case EmploymentHoursValidationResult.TooManyEmploymentHours: return new Manager.OperationResult(false, Messages.ErrorWhileAddingEmployee_TooManyHours);
            }

            var resultOfEmploymentDateValidation = ValidateEmploymentDate(info.EmploymentDate);
            if (resultOfEmploymentDateValidation == EmploymentDateValidationResult.NullEmploymentDate)
                return new Manager.OperationResult(false, Messages.ErrorWhileAddingEmployee_NullEmploymentDate);

            var resultOfPositionIdValidation = ValidatePositionId(info.PositionId);
            if(resultOfPositionIdValidation == PositionIdValidationResult.NullPositionId)
            {
                return new Manager.OperationResult(false, Messages.ErrorWhileAddingEmployee_NullPositionId);
            }

            var resultOfDepartmentIdValidation = ValidateDepartmentId(info.DepartmentId);
            if(resultOfDepartmentIdValidation == DepartmentIdValidationResult.NullDepartmentId)
            {
                return new Manager.OperationResult(false, Messages.ErrorWhileAddingEmployee_NullDepartmentId);
            }

            return new Manager.OperationResult(true, Messages.SuccessfullyAddedEmployee);

        }

        public static OperationResult Validate(UpdateEmployeeInputInfo info)
        {
            if(info.Id < 0)
            {
                return new Manager.OperationResult(false, Messages.ErrorWhileUpdatingEmployee_InvalidId);
            }
            var resultOfNameValidation = ValidateName(info.Name);
            switch (resultOfNameValidation)
            {
                case NameValidationResult.EmptyName: return new Manager.OperationResult(false, Messages.ErrorWhileUpdatingEmployee_EmptyEmployeeName);
                case NameValidationResult.TooLongName: return new Manager.OperationResult(false, Messages.ErrorWhileUpdatingEmployee_TooLongEmployeeName);
            }

            var resultOfEmailValidation = ValidateEmail(info.Email);
            if (resultOfEmailValidation == EmailValidationResult.TooLongEmail)
                return new Manager.OperationResult(false, Messages.ErrorWhileUpdatingEmployee_TooLongEmployeeEmail);

            var resultOfAddressValidation = ValidateAddress(info.Address);
            if (resultOfAddressValidation == AddressValidationResult.TooLongAddress)
            {
                return new Manager.OperationResult(false, Messages.ErrorWhileUpdatingEmployee_TooLongEmployeeAddress);
            }

            var resultOfEmploymentHoursValidation = ValidateEmploymentHours(info.EmploymentHours);
            switch (resultOfEmploymentHoursValidation)
            {
                case EmploymentHoursValidationResult.NegativeEmploymentHours: return new Manager.OperationResult(false, Messages.ErrorWhileUpdatingEmployee_NegativeEmploymentHours);
                case EmploymentHoursValidationResult.TooManyEmploymentHours: return new Manager.OperationResult(false, Messages.ErrorWhileUpdatingEmployee_TooManyHours);
            }

            var resultOfEmploymentDateValidation = ValidateEmploymentDate(info.EmploymentDate);
            if (resultOfEmploymentDateValidation == EmploymentDateValidationResult.NullEmploymentDate)
            {
                return new Manager.OperationResult(false, Messages.ErrorWhileUpdatingEmployee_NullEmploymentDate);
            }

            var resultOfPositionIdValidation = ValidatePositionId(info.PositionId);
            if (resultOfPositionIdValidation == PositionIdValidationResult.NullPositionId)
            {
                return new Manager.OperationResult(false, Messages.ErrorWhileUpdatingEmployee_NullPositionId);
            }

            var resultOfDepartmentIdValidation = ValidateDepartmentId(info.DepartmentId);
            if (resultOfDepartmentIdValidation == DepartmentIdValidationResult.NullDepartmentId)
            {
                return new Manager.OperationResult(false, Messages.ErrorWhileUpdatingEmployee_NullDepartmentId);
            }

            return new Manager.OperationResult(true, Messages.SuccessfullyUpdatedEmployee);

        }
    }
}
