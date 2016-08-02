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

    }
}