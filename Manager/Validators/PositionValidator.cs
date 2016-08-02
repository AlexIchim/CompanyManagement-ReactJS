using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Manager.InputInfoModels;

namespace Manager.Validators
{
    static class PositionValidator
    {
        public enum NameValidationResult{Success, NullName, TooLongName};

        private static NameValidationResult ValidateName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return NameValidationResult.NullName;
            }
            else if (name.Length > 100)
            {
                return NameValidationResult.TooLongName;
            }
            else return NameValidationResult.Success;
        }

        public static OperationResult Validate(AddPositionInputInfo info)
        {
            var resultOfNameValidation = ValidateName(info.Name);
            if (resultOfNameValidation == NameValidationResult.NullName)
            {
                return new Manager.OperationResult(false, Messages.ErrorWhileAddingPosition_NullPositionName);
            }
            else if(resultOfNameValidation == NameValidationResult.TooLongName)
            {
                return new Manager.OperationResult(false, Messages.ErrorWhileAddingPosition_TooLongPositionName);
            }

            return new Manager.OperationResult(true, Messages.SuccessfullyAddedPosition);
        }

        public static OperationResult Validate(UpdatePositionInputInfo info)
        {
            if (info.Id < 0)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingPosition_InvalidId);
            }
            else
            {
                var resultOfNameValidation = ValidateName(info.Name);

                if(resultOfNameValidation == NameValidationResult.NullName)
                {
                    return new Manager.OperationResult(false, Messages.ErrorWhileUpdatingPosition_NullPositionName);
                }
                else if(resultOfNameValidation == NameValidationResult.TooLongName)
                {
                    return new Manager.OperationResult(false, Messages.ErrorWhileUpdatingPosition_TooLongPositionName);
                }

                return new Manager.OperationResult(true, Messages.SuccessfullyUpdatedPosition);
            }
        }
    }
}
