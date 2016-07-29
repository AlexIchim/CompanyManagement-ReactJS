using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Domain.Models;
using Manager.InputInfoModels;

namespace Manager.Services
{
    public class ProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IMapper mapper, IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public OperationResult Add(AddProjectInputInfo infoInput)
        {
            var newProject = _mapper.Map<Project>(infoInput);
            _projectRepository.Add(newProject);

            return new OperationResult(true, Messages.SuccessfullyAddedProject);
        }
    }
}
