﻿using AutoMapper;
using Contracts;
using Domain.Models;
using Manager.InfoModels;
using Manager.InputInfoModels;
using System.Collections.Generic;

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

        public ProjectInfo GetById(int id)
        {

            Project project = _projectRepository.GetById(id);
            var newProject = _mapper.Map<ProjectInfo>(project);
            return newProject;

        }

        public IEnumerable<EmployeeInfo> GetAllMembersFromProject(int projectId)
        {
            var employees = _projectRepository.GetAllMembersFromProject(projectId);
            return _mapper.Map<IEnumerable<EmployeeInfo>>(employees);
        }

        public int GetAllocationOfEmployeeFromProject(int projectId, int employeeId)
        {
            return _projectRepository.GetAllocationOfEmployeeFromProject(projectId, employeeId);
        }

        public int GetNrTeamMembers(int projectId)
        {
            return _projectRepository.GetNrTeamMembers(projectId);
        }
    }
}
