using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Domain.Models;
using Manager.InfoModels;
using Manager.InputInfoModels;

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
            var officeInfo = _mapper.Map<IEnumerable<OfficeInfo>>(offices);

            return officeInfo;
        }

        public OfficeInfo GetById(int officeId)
        {
            var office = _officeRepository.GetById(officeId);
            var officeInfo = _mapper.Map<OfficeInfo>(office);

            return officeInfo;
        }
    }
}
