using Manager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ManagementApp.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Route("getAll")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var result = _employeeService.GetAll();
            return Json(result);
        }

        [Route("getAllDepartmentManagers")]
        [HttpGet]
        public IHttpActionResult GetAllDepartmentManagers()
        {
            var result = _employeeService.GetAllDepartmentManagers();
            return Json(result);
        }

        [Route("getAvailableEmployees")]
        [HttpGet]
        public IHttpActionResult GetgetAvailableEmployees(int? departmentId = null, int? positionId = null)
        {
            var result = _employeeService.GetAvailableEmployees(departmentId, positionId);
            return Json(result);
        }


        [Route("getById")]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var result = _employeeService.GetById(id);
            return Json(result);
        }


    }
}