using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Domain.Models;
using Manager.InfoModels;
using Manager.InputInfoModels;

namespace Manager.Services
{
    public class PositionService
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;

        public PositionService(IMapper mapper, IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
            _mapper = mapper;
        }

        public IEnumerable<PositionInfo> GetAll()
        {
            var positions = _positionRepository.GetAll();
            var positionInfos = _mapper.Map<IEnumerable<PositionInfo>>(positions);

            return positionInfos;
        }

        public PositionInfo GetById(int id)
        {
            var position = _positionRepository.GetById(id);
            var positionInfo = _mapper.Map<PositionInfo>(position);

            return positionInfo;
        }

        public OperationResult Add(AddPositionInputInfo inputInfo)
        {
            var result = Validators.PositionValidator.Validate(inputInfo);

            if (result.Success == true)
            {
                _positionRepository.Add(_mapper.Map<Position>(inputInfo));
            }

            return result;
        }

        public OperationResult Update(UpdatePositionInputInfo inputInfo)
        {
            var position = _positionRepository.GetById(inputInfo.Id);

            if (position == null)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingPosition_InvalidId);
            }
            else
            {
                position.Name = inputInfo.Name;
                _positionRepository.Save();
                return new Manager.OperationResult(true, Messages.SuccessfullyUpdatedPosition);
            }
        }
    }
}