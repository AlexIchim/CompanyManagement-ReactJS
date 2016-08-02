using Manager.InputInfoModels;
using Manager.Services;
using System.Web.Http;

namespace ManagementApp.Controllers
{
    [RoutePrefix("api/employee")]
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



    }
}