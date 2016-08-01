using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Domain.Models;
using Manager.InfoModels;
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

        public IEnumerable<ProjectInfo> GetAll()
        {
            var projects = _projectRepository.GetAll();
            var projectInfos = _mapper.Map<IEnumerable<ProjectInfo>>(projects);

            return projectInfos;
        }

        public ProjectInfo GetById(int id)
        {
            var project = _projectRepository.GetById(id);
            var projectInfo = _mapper.Map<ProjectInfo>(project);

            return projectInfo;
        }

    }
}
