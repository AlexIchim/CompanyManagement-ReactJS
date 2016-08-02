﻿using AutoMapper;
using Contracts;
using Manager.InfoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager.InputInfoModels;
using Domain.Models;

namespace Manager.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public IEnumerable<ProjectsOfAnEmployeeInfo> GetProjectByEmployeeId(int employeeId)
        {
            return _employeeRepository.GetProjectByEmployeeId(employeeId).
                Select(emp => new ProjectsOfAnEmployeeInfo
                {
                    Id = emp.ProjectId,
                    Name = emp.Project.Name,
                    Allocation = emp.Allocation
                }).ToList();
        }

        public OperationResult ReleaseEmployee(int employeeId)
        {
            _employeeRepository.ReleaseEmployee(employeeId);
            _employeeRepository.UpdateTotalAllocation(employeeId,0);
            return new OperationResult(true, Messages.SuccessfullyDeletedEmployee);
        }

        public OperationResult UpdateEmployee(UpdateEmployeeInputInfo inputInfo)
        {
            var employee = _employeeRepository.GetById(inputInfo.Id);
            //verify department

            if (employee == null)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingEmployee);
            }

            employee.Name = inputInfo.Name;
            employee.Address = inputInfo.Address;
            employee.EmploymentDate = inputInfo.EmploymentDate;
            employee.ReleaseDate = inputInfo.ReleaseDate;
            employee.JobType = inputInfo.JobType;
            employee.PositionType = inputInfo.PositionType;

            _employeeRepository.Save();

            return new OperationResult(true, Messages.SuccessfullyUpdatedEmployee);

        }

        public OperationResult AddEmployeeToProject(AddEmployeeToProjectInputInfo inputInfo)
        {
            var newEp = _mapper.Map<EmployeeProject>(inputInfo);

            int totalAllocation = _employeeRepository.ComputeTotalAllocation(newEp.EmployeeId);

            int newTotalAllocation = totalAllocation + newEp.Allocation;

            if (newTotalAllocation >= 100)
            {
                return new OperationResult(false, Messages.ErrorAddingEmployeeToProject);
            }

            _employeeRepository.UpdateTotalAllocation(newEp.EmployeeId, newTotalAllocation);
            _employeeRepository.AddEmployeeToProject(newEp);
            return new OperationResult(true, Messages.SuccessfullyAddedEmployeeToProject);
        }

        public OperationResult UpdatePartialAllocation(UpdateAllocationInputInfo inputInfo)
        {
            var employeeProjects = _employeeRepository.GetEmployeeProjectById(inputInfo.ProjectId).ToList();

            if (employeeProjects != null)
            {
                EmployeeProject ep = employeeProjects.SingleOrDefault(e => e.EmployeeId == inputInfo.EmployeeId);
                if (ep != null)
                {
                    int totalAllocation = _employeeRepository.ComputeTotalAllocation(ep.EmployeeId);

                    int newTotalAllocation = totalAllocation - ep.Allocation + inputInfo.Allocation;

                    if (newTotalAllocation > 100)
                    {
                        return new OperationResult(false, Messages.ErrorWhileUpdatingPartialAllocation);
                    }

                    ep.Allocation = inputInfo.Allocation;
                    
                    _employeeRepository.UpdateTotalAllocation(ep.EmployeeId,newTotalAllocation);
                    return new OperationResult(true, Messages.SuccessfullyUpdatedPartialAllocation);
                }
            }
            return new OperationResult(false, Messages.ErrorWhileUpdatingPartialAllocation);
        }
    }
}
