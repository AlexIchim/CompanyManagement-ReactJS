using AutoMapper;
using Contracts;
using Domain.Models;
using Manager.InfoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IEnumerable<ProjectInfo> GetByProjectId(int projectId)
        {
            var employees = _projectRepository.GetByProjectId(projectId);
            var employeeInfos = _mapper.Map<IEnumerable<ProjectInfo>>(employees);
            return employeeInfos;
        }
    }
}
