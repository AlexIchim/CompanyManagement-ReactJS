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
        public static OperationResult Validate(AddPositionInputInfo info)
        {
            if (String.IsNullOrEmpty(info.Name))
            {
                return new OperationResult(false, Messages.ErrorWhileAddingPosition_EmptyName);
            }
            else if (info.Name.Length > 100)
            {
                return new Manager.OperationResult(false, Messages.ErrorWhileAddingPosition_NameTooLong);
            }
            else return new OperationResult(true, Messages.SuccessfullyAddedPosition);
        }

        public static OperationResult Validate(UpdatePositionInputInfo info)
        {
            if (info.Id < 0)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingPosition_InvalidId);
            }
            else if (String.IsNullOrEmpty(info.Name))
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingPosition_EmptyName);
            }
            else if (info.Name.Length > 100)
            {
                return new Manager.OperationResult(false, Messages.ErrorWhileUpdatingPosition_NameTooLong);
            }
            else return new OperationResult(true, Messages.SuccessfullyUpdatedPosition);
        }
    }
}
