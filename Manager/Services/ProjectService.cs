using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Domain.Models;
using Manager.InfoModels;
using Manager.InputInfoModels;
using System;
using System.Linq;

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

        public IEnumerable<ProjectEmployeeInfo> GetEmployeesByProjectId(int id)
        {
            var tuples = _projectRepository.GetEmployeesByProjectId(id);

            var res = tuples.Select(t => new ProjectEmployeeInfo()
            {
                Employee = _mapper.Map<EmployeeInfo>(t.Item1),
                Allocation = t.Item2
            });

            return res;
        }

        public OperationResult Delete(int? id)
        {
            if (id == null)
            {
                return new OperationResult(false, Messages.ErrorWhileDeletingProject);
            }
            else
            {
                var project = _projectRepository.GetById(id.Value);

                if (project == null)
                {
                    return new OperationResult(false, Messages.ErrorWhileDeletingProject);
                }

                _projectRepository.Delete(project);
                _projectRepository.Save();
            }
            return new OperationResult(true, Messages.SuccessfullyDeletedProject);
        }

    }
}
