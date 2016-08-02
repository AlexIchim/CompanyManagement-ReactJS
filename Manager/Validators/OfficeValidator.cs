using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Manager.InputInfoModels;

namespace Manager.Validators
{
    static class OfficeValidator
    {

        public enum NameValidationResult { Success, NotEmpty, TooLong }
        public enum AddressValidationResult { Success, NotEmpty, TooLong }
        public enum PhoneValidationResult { Success, NotEmpty, TooLong }

        private static NameValidationResult ValidateName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return NameValidationResult.NotEmpty;
            }
            else if (name.Length > 100)
            {
                return NameValidationResult.TooLong;
            }
            else return NameValidationResult.Success;
        }

        private static AddressValidationResult ValidateAddress(string address)
        {
            if (String.IsNullOrEmpty(address))
            {
                return AddressValidationResult.NotEmpty;
            }
            else if (address.Length > 300)
            {
                return AddressValidationResult.TooLong;
            }
            else return AddressValidationResult.Success;
        }

        private static PhoneValidationResult ValidatePhone(string phone)
        {
            if (String.IsNullOrEmpty(phone))
            {
                return PhoneValidationResult.NotEmpty;
            }
            else if (phone.Length > 20)
            {
                return PhoneValidationResult.TooLong;
            }
            else return PhoneValidationResult.Success;
        }

        public static OperationResult Validate(AddOfficeInputInfo info)
        {
            OperationResult result = new OperationResult(false, "");

            if (ValidateName(info.Name) == NameValidationResult.Success &&
                ValidatePhone(info.Phone) == PhoneValidationResult.Success &&
                ValidateAddress(info.Address) == AddressValidationResult.Success
                )
            {
                return new OperationResult(true, Messages.SuccessfullyAddedOffice);
            }
            else
            {
                switch (ValidateName(info.Name))
                {
                    case NameValidationResult.NotEmpty:
                        result.Message += Messages.ErrorWhileAddingOffice_EmptyName;
                        break;

                    case NameValidationResult.TooLong:
                        result.Message += Messages.ErrorWhileAddingOffice_NameTooLong;
                        break;
                }

                switch (ValidatePhone(info.Phone))
                {
                    case PhoneValidationResult.NotEmpty:
                        result.Message += Messages.ErrorWhileAddingOffice_EmptyPhone;
                        break;
                    case PhoneValidationResult.TooLong:
                        result.Message += Messages.ErrorWhileAddingOffice_PhoneTooLong;
                        break;
                }

                switch (ValidateAddress(info.Address))
                {
                    case AddressValidationResult.NotEmpty:
                        result.Message += Messages.ErrorWhileAddingOffice_EmptyAddress;
                        break;
                    case AddressValidationResult.TooLong:
                        result.Message += Messages.ErrorWhileAddingOffice_AddressTooLong;
                        break;
                }
                return result;
            }
        }

        public static OperationResult Validate(UpdateOfficeInputInfo info)
        {

            if (info.Id < 0)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingOffice_InvalidId);
            }
            else
            {
                OperationResult result = new OperationResult(false, "");


                if (ValidateName(info.Name) == NameValidationResult.Success &&
                    ValidatePhone(info.Phone) == PhoneValidationResult.Success &&
                    ValidateAddress(info.Address) == AddressValidationResult.Success
                    )
                {
                    return new OperationResult(true, Messages.SuccessfullyAddedOffice);
                }
                else
                {

                    switch (ValidateName(info.Name))
                    {
                        case NameValidationResult.NotEmpty:
                            result.Message += Messages.ErrorWhileUpdatingOffice_EmptyName;
                            break;

                        case NameValidationResult.TooLong:
                            result.Message += Messages.ErrorWhileUpdatingOffice_NameTooLong;
                            break;
                    }

                    switch (ValidatePhone(info.Phone))
                    {
                        case PhoneValidationResult.NotEmpty:
                            result.Message += Messages.ErrorWhileUpdatingOffice_EmptyPhone;
                            break;
                        case PhoneValidationResult.TooLong:
                            result.Message += Messages.ErrorWhileUpdatingOffice_PhoneTooLong;
                            break;
                    }

                    switch (ValidateAddress(info.Address))
                    {
                        case AddressValidationResult.NotEmpty:
                            result.Message += Messages.ErrorWhileUpdatingOffice_EmptyAddress;
                            break;
                        case AddressValidationResult.TooLong:
                            result.Message += Messages.ErrorWhileUpdatingOffice_AddressTooLong;
                            break;
                    }
                    return result;
                }
            }
        }
    }
}
