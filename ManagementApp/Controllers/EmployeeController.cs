using Manager.InputInfoModels;
using Manager.Services;
using System.Web.Http;
using System.Web.Http.Cors;
namespace ManagementApp.Controllers
{
    [RoutePrefix("api/employee")]
    [EnableCors("*", "*", "GET, POST, PUT, DELETE")]
    public class EmployeeController: ApiController
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
        public IHttpActionResult GetAllDepartmentEmployees(int departmentId, int? pageSize, int? pageNr)
        {
            var result = _employeeService.GetAllDepartmentEmployees(departmentId, pageSize, pageNr);
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
        public IHttpActionResult GetEmployeesThatAreNotFullyAllocated()
        {
            var result = _employeeService.GetEmployeesThatAreNotFullyAllocated();
            return Json(result);
        }

        [Route("getJobTypes")]
        [HttpGet]
        public IHttpActionResult GetJobTypes()
        {
            var result = _employeeService.GetJobTypesDescriptions();
            return Json(result);
        }

        [Route("getPoisitionTypes")]
        [HttpGet]
        public IHttpActionResult GetPositionTypes()
        {
            var result = _employeeService.GetPositionTypesDescriptions();
            return Json(result);
        }


    }
}