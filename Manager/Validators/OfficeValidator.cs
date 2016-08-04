using Contracts;
using Manager.InfoModels;
using Manager.InputInfoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Services
{
    public class OfficeValidator: IOfficeValidator
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
        public bool ValidateOfficeInfo(OfficeInfo inputInfo)
        {
            bool validation = ValidateId(inputInfo.Id) && ValidateString(inputInfo.Name) && ValidateString(inputInfo.Address) && ValidateString(inputInfo.PhoneNumber);
            //de validat imaginea
            return validation;
        }

        public bool ValidateAddOfficeInfo(AddOfficeInputInfo addInfo)
        {
            bool validation = ValidateId(addInfo.Id) && ValidateString(addInfo.Name) && ValidateString(addInfo.Address) && ValidateString(addInfo.PhoneNumber);
            //de validat imaginea
            return validation;
        }

        public bool ValidateUpdateOfficeInfo(UpdateOfficeInputInfo updateInfo)
        {
            bool validation = ValidateId(updateInfo.Id) && ValidateString(updateInfo.Name) && ValidateString(updateInfo.Address) && ValidateString(updateInfo.PhoneNumber);
            //de validat imaginea
            return validation;
        }
    }
}
