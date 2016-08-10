using Manager.InputInfoModels;
using Manager.Services;
using System.Web.Http;
using System.Web.Http.Cors;
using Domain.Enums;

namespace ManagementApp.Controllers
{
    [RoutePrefix("api/employee")]
    [EnableCors("*", "*", "GET, POST, PUT, DELETE")]
    public class EmployeeController : ApiController
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        [Route("getAllProjectsOfAnEmployee")]
        [HttpGet]
        public IHttpActionResult GetProjectByEmployeeId(int employeeId)
        {
            var result = _employeeService.GetProjectByEmployeeId(employeeId);


            return Json(result);
        }

        [Route("releaseEmployee")]
        [HttpDelete]
        public IHttpActionResult ReleaseEmployee(int employeeId)
        {
            var result = _employeeService.ReleaseEmployee(employeeId);

            return Json(result);

        }

        [Route("updateEmployee")]
        [HttpPut]
        public IHttpActionResult UpdateEmployee(UpdateEmployeeInputInfo inputInfo)
        {
            var result = _employeeService.UpdateEmployee(inputInfo);

            return Json(result);

        }

        [Route("addEmployeeToProject")]
        [HttpPost]
        public IHttpActionResult AddEmployeeToProject([FromBody] AddEmployeeToProjectInputInfo inputInfo)
        {

            var result = _employeeService.AddEmployeeToProject(inputInfo);
            return Json(result);
        }

        [Route("updatePartialAllocation")]
        [HttpPut]
        public IHttpActionResult Update([FromBody] UpdateAllocationInputInfo inputInfo)
        {

            var result = _employeeService.UpdatePartialAllocation(inputInfo);
            return Json(result);
        }

        [Route("getAllDepartmentEmployees")]
        [HttpGet]
        public IHttpActionResult GetAllDepartmentEmployees(int departmentId, string employeeName, int? pageSize, int? pageNr,int? allocation=null, PositionType? ptype = null,JobType? jtype = null)
        {
            var result = _employeeService.GetAllDepartmentEmployees(departmentId, employeeName, pageSize, pageNr,allocation,ptype,jtype);
            return Json(result);
        }

        [Route("addEmployee")]
        [HttpPost]
        public IHttpActionResult AddEmployee([FromBody] AddEmployeeToDepartmentInputInfo inputInfo)
        {
            var result = _employeeService.AddEmployee(inputInfo);
            return Json(result);
        }

        [Route("GetAllUnAllocatedEmployeesOnProject")]
        [HttpGet]
        public IHttpActionResult GetAllUnAllocatedEmployeesOnProject()
        {
            var result = _employeeService.GetAllUnAllocatedEmployeesOnProject();
            return Json(result);
        }

        [Route("GetEmployeesThatAreNotFullyAllocated")]
        [HttpGet]
        public IHttpActionResult GetEmployeesThatAreNotFullyAllocated(int projectId,int? pageSize, int? pageNr, int? departmentId=null, PositionType? ptype = null)
        {
            var result = _employeeService.GetEmployeesThatAreNotFullyAllocated(projectId,departmentId,pageSize,pageNr,ptype);
            return Json(result);
        }

        [Route("getTotalAllocation")]
        [HttpGet]
        public IHttpActionResult GetTotalAllocation(int employeeId)
        {
            var result = _employeeService.GetTotalAllocation(employeeId);
            return Json(result);
        }



        [Route("getJobTypes")]
        [HttpGet]
        public IHttpActionResult GetJobTypes()
        {
            var result = _employeeService.GetJobTypesDescriptions();
            return Json(result);
        }

        [Route("getPositionTypes")]
        [HttpGet]
        public IHttpActionResult GetPositionTypes()
        {
            var result = _employeeService.GetPositionTypesDescriptions();
            return Json(result);
        }

        [Route("assignEmployee")]
        [HttpPost]
        public IHttpActionResult AssignEmployee([FromBody] AssignEmployeeInputInfo inputInfo)
        {
            var result = _employeeService.AssignEmployee(inputInfo);
            return Json(result);
        }

        [Route("searchEmployeesByName")]
        [HttpGet]
        public IHttpActionResult SearchEmployeesByName(int departmentId, string employeeName, int? pageSize, int? pageNr)
        {
            var result = _employeeService.SearchEmployeesByName(departmentId, employeeName, pageSize, pageNr);
            return Json(result);
        }


        [Route("getEmployeeById")]
        [HttpPost]
        public IHttpActionResult GetEmployeeById(int employeeId)
        {
            var result = _employeeService.GetEmployeeById(employeeId);
            return Json(result);

        }



    }
}