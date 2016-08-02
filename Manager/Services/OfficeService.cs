using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Domain.Models;
using Manager.InfoModels;
using Manager.InputInfoModels;
using Manager.Validators;

namespace Manager.Services
{
    public class OfficeService
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IMapper _mapper;

        public OfficeService(IMapper mapper, IOfficeRepository officeRepository)
        {
            _officeRepository = officeRepository;
            _mapper = mapper;
        }

        public IEnumerable<OfficeInfo> GetAll()
        {
            var offices = _officeRepository.GetAll();
            var officeInfos = _mapper.Map<IEnumerable<OfficeInfo>>(offices);

            return officeInfos;
        }

        public OperationResult Add(AddOfficeInputInfo inputInfo)
        {
            AddOfficeValidator validator=new AddOfficeValidator();
            var result =validator.Validate(inputInfo);
            if (!result.IsValid)
            {
                return new OperationResult(false, result);
            }


            var newOffice = _mapper.Map<Office>(inputInfo);
            _officeRepository.Add(newOffice);

            return new OperationResult(true,Messages.SuccessfullyAddedOffice);
        }

        public OperationResult Update(UpdateOfficeInputInfo inputInfo)
        {
            var office = _officeRepository.GetById(inputInfo.Id);

            if (office == null)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingOffice);
            }

            office.Name = inputInfo.Name;
            office.Address = inputInfo.Address;
            office.Phone = inputInfo.Phone;
            office.Image = inputInfo.Image;

            _officeRepository.Save();

            return new OperationResult(true, Messages.SuccessfullyUpdatedOffice);
        }

        public OperationResult Delete(int officeId)
        {
            var office = _officeRepository.GetById(officeId);

            if (office==null)
            {
                return new OperationResult(false, Messages.ErrorWhileDeletingOffice);
            }

            _officeRepository.Delete(office);
            return new OperationResult(true, Messages.SuccessfullyDeletedOffice);
        }
    }
}
