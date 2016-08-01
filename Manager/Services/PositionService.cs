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
            var newPosition = _mapper.Map<Position>(inputInfo);
            _positionRepository.Add(newPosition);

            int result = inputInfo.InputInfoValidationResult();
            if (result == 1)
            {
                return new Manager.OperationResult(false, Messages.ErrorWhileAddingPosition_EmptyName);
            }
                else
                    if (result == 2)
                    {
                        return new Manager.OperationResult(false, Messages.ErrorWhileAddingPosition_NameTooLong);
                    }

            return new OperationResult(true, Messages.SuccessfullyAddedPosition);
        }

        public OperationResult Update(UpdatePositionInputInfo inputInfo)
        {
            var position = _positionRepository.GetById(inputInfo.Id);

            int result = inputInfo.InputInfoValidationResult();

            if(position == null)
            {
                return new Manager.OperationResult(false, Messages.ErrorWhileUpdatingPosition);
            }
            else
                if(result == 1)
                {
                    return new Manager.OperationResult(false, Messages.ErrorWhileUpdatingPosition_EmptyName);
                }
                else
                    if(result == 2)
                    {
                return new Manager.OperationResult(false, Messages.ErrorWhileUpdatingPosition_NameTooLong);
                    }  

            position.Name = inputInfo.Name;
            _positionRepository.Save();

            return new Manager.OperationResult(true, Messages.SuccessfullyAddedDepartment);
        }
    }
}