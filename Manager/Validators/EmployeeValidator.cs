using System;
using Manager.InputInfoModels;

namespace Manager.Validators
{
    class EmployeeValidator
    {
        public enum NameValidationResult { Success, EmptyName, TooLongName };
        public enum EmailValidationResult { Success, TooLongEmail };
        public enum AddressValidationResult { Success, TooLongAddress };
        public enum EmploymentHoursValidationResult { Success, TooFewEmploymentHours, TooManyEmploymentHours };
        public enum EmploymentDateValidationResult { Success, NullEmploymentDate };
        public enum PositionIdValidationResult { Success, NullPositionId };
        public enum DepartmentIdValidationResult { Success, NullDepartmentId };

        private static NameValidationResult ValidateName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return NameValidationResult.EmptyName;
            }
            if (name.Length > 100)
            {
                return NameValidationResult.TooLongName;
            }
            return NameValidationResult.Success;
        }

        private static EmailValidationResult ValidateEmail(string email)
        {
            if (email.Length > 200)
            {
                return EmailValidationResult.TooLongEmail;
            }
            return EmailValidationResult.Success;
        }

        private static AddressValidationResult ValidateAddress(string address)
        {
            if (address == null || address.Length > 300)
            {
                return AddressValidationResult.TooLongAddress;
            }
            return AddressValidationResult.Success;
        }

        private static EmploymentHoursValidationResult ValidateEmploymentHours(int employmentHours)
        {
            if (employmentHours <= 0)
            {
                return EmploymentHoursValidationResult.TooFewEmploymentHours;
            }
            if (employmentHours > 8)
            {
                return EmploymentHoursValidationResult.TooManyEmploymentHours;
            }
            return EmploymentHoursValidationResult.Success;
        }

        private static EmploymentDateValidationResult ValidateEmploymentDate(DateTime? employmentDate)
        {
            if (employmentDate == null)
            {
                return EmploymentDateValidationResult.NullEmploymentDate;
            }
            return EmploymentDateValidationResult.Success;
        }

        public static PositionIdValidationResult ValidatePositionId(int? positionId)
        {
            if (positionId == null)
            {
                return PositionIdValidationResult.NullPositionId;
            }
            return PositionIdValidationResult.Success;
        }

        public static DepartmentIdValidationResult ValidateDepartmentId(int? departmentId)
        {
            if (departmentId == null)
            {
                return DepartmentIdValidationResult.NullDepartmentId;
            }
            return DepartmentIdValidationResult.Success;
        }

        public static OperationResult Validate(AddEmployeeInputInfo info)
        {
            var resultOfNameValidation = ValidateName(info.Name);
            var resultOfEmailValidation = ValidateEmail(info.Email);
            var resultOfAddressValidation = ValidateAddress(info.Address);
            var resultOfEmploymentHoursValidation = ValidateEmploymentHours(info.EmploymentHours);
            var resultOfEmploymentDateValidation = ValidateEmploymentDate(info.EmploymentDate);
            var resultOfPositionIdValidation = ValidatePositionId(info.PositionId);
            var resultofDepartmentIdValidation = ValidateDepartmentId(info.DepartmentId);
            string messageToReturn = "";

            if (resultOfNameValidation == NameValidationResult.EmptyName)
            {
                messageToReturn = messageToReturn + ((messageToReturn == "") ? "" : " ") + Messages.EmptyEmployeeName;
            }
            if (resultOfNameValidation == NameValidationResult.TooLongName)
            {
                messageToReturn = messageToReturn + ((messageToReturn == "") ? "" : " ") + Messages.TooLongEmployeeName;
            }
            if (resultOfEmailValidation == EmailValidationResult.TooLongEmail)
            {
                messageToReturn = messageToReturn + ((messageToReturn == "") ? "" : " ") + Messages.TooLongEmployeeEmail;
            }
            if (resultOfAddressValidation == AddressValidationResult.TooLongAddress)
            {
                messageToReturn = messageToReturn + ((messageToReturn == "") ? "" : " ") + Messages.TooLongEmployeeAddress;
            }
            if (resultOfEmploymentHoursValidation == EmploymentHoursValidationResult.TooFewEmploymentHours)
            {
                messageToReturn = messageToReturn + ((messageToReturn == "") ? "" : " ") + Messages.TooFewEmploymentHours;
            }
            if (resultOfEmploymentHoursValidation == EmploymentHoursValidationResult.TooManyEmploymentHours)
            {
                messageToReturn = messageToReturn + ((messageToReturn == "") ? "" : " ") + Messages.TooManyEmploymentHours;
            }
            if (resultOfEmploymentDateValidation == EmploymentDateValidationResult.NullEmploymentDate)
            {
                messageToReturn = messageToReturn + ((messageToReturn == "") ? "" : " ") + Messages.NullEmploymentDate;
            }
            if (resultOfPositionIdValidation == PositionIdValidationResult.NullPositionId)
            {
                messageToReturn = messageToReturn + ((messageToReturn == "") ? "" : " ") + Messages.NullPositionId;
            }
            if (resultofDepartmentIdValidation == DepartmentIdValidationResult.NullDepartmentId)
            {
                messageToReturn = messageToReturn + ((messageToReturn == "") ? "" : " ") + Messages.NullDepartmentId;
            }
            if (messageToReturn == "")
            {
                return new OperationResult(true, Messages.SuccessfullyAddedEmployee);
            }
            return new OperationResult(false, messageToReturn);
        }

        public static OperationResult Validate(UpdateEmployeeInputInfo info)
        {
            var resultOfNameValidation = ValidateName(info.Name);
            var resultOfEmailValidation = ValidateEmail(info.Email);
            var resultOfAddressValidation = ValidateAddress(info.Address);
            var resultOfEmploymentHoursValidation = ValidateEmploymentHours(info.EmploymentHours);
            var resultOfEmploymentDateValidation = ValidateEmploymentDate(info.EmploymentDate);
            var resultOfPositionIdValidation = ValidatePositionId(info.PositionId);
            var resultofDepartmentIdValidation = ValidateDepartmentId(info.DepartmentId);
            string messageToReturn = "";

            if (resultOfNameValidation == NameValidationResult.EmptyName)
            {
                messageToReturn = messageToReturn + ((messageToReturn == "") ? "" : " ") + Messages.EmptyEmployeeName;
            }
            if (resultOfNameValidation == NameValidationResult.TooLongName)
            {
                messageToReturn = messageToReturn + ((messageToReturn == "") ? "" : " ") + Messages.TooLongEmployeeName;
            }
            if (resultOfEmailValidation == EmailValidationResult.TooLongEmail)
            {
                messageToReturn = messageToReturn + ((messageToReturn == "") ? "" : " ") + Messages.TooLongEmployeeEmail;
            }
            if (resultOfAddressValidation == AddressValidationResult.TooLongAddress)
            {
                messageToReturn = messageToReturn + ((messageToReturn == "") ? "" : " ") + Messages.TooLongEmployeeAddress;
            }
            if (resultOfEmploymentHoursValidation == EmploymentHoursValidationResult.TooFewEmploymentHours)
            {
                messageToReturn = messageToReturn + ((messageToReturn == "") ? "" : " ") + Messages.TooFewEmploymentHours;
            }
            if (resultOfEmploymentHoursValidation == EmploymentHoursValidationResult.TooManyEmploymentHours)
            {
                messageToReturn = messageToReturn + ((messageToReturn == "") ? "" : " ") + Messages.TooManyEmploymentHours;
            }
            if (resultOfEmploymentDateValidation == EmploymentDateValidationResult.NullEmploymentDate)
            {
                messageToReturn = messageToReturn + ((messageToReturn == "") ? "" : " ") + Messages.NullEmploymentDate;
            }
            if (resultOfPositionIdValidation == PositionIdValidationResult.NullPositionId)
            {
                messageToReturn = messageToReturn + ((messageToReturn == "") ? "" : " ") + Messages.NullPositionId;
            }
            if (resultofDepartmentIdValidation == DepartmentIdValidationResult.NullDepartmentId)
            {
                messageToReturn = messageToReturn + ((messageToReturn == "") ? "" : " ") + Messages.NullDepartmentId;
            }
            if (messageToReturn == "")
            {
                return new OperationResult(true, Messages.SuccessfullyUpdatedEmployee);
            }
            return new OperationResult(false, messageToReturn);

        }
    }
}
