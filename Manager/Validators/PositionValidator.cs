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
        private static OperationResult ValidateName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return new OperationResult(false, Messages.ErrorWhileAddingPosition_EmptyName);
            }
            else if (name.Length > 100)
            {
                return new Manager.OperationResult(false, Messages.ErrorWhileAddingPosition_NameTooLong);
            }
            else return new OperationResult(true, Messages.SuccessfullyAddedPosition);
        }

        public static OperationResult Validate(AddPositionInputInfo info)
        {
            return ValidateName(info.Name);
        }

        public static OperationResult Validate(UpdatePositionInputInfo info)
        {
            if (info.Id < 0)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingPosition_InvalidId);
            }
            else
            {
                return ValidateName(info.Name);
            }
        }
    }
}
