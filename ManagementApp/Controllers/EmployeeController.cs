using Manager.Services;
using Manager.InputInfoModels;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ManagementApp.Controllers
{
    [RoutePrefix("api/employee")]
    [EnableCors("*", "*", "*")]
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
        public IHttpActionResult GetgetAvailableEmployees(int? departmentId = null, int? positionId = null, int? pageSize = null, int? pageNumber = null)
        {
            var result = _employeeService.GetAvailableEmployees(departmentId, positionId, pageSize, pageNumber);
            return Json(result);
        }


        [Route("getById")]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var result = _employeeService.GetById(id);
            return Json(result);
        }

        [Route("getAllocationsByEmployeeId")]
        [HttpGet]
        public IHttpActionResult GetAllocationsByEmployeeId(int id)
        {
            var result = _employeeService.GetAllocationsByEmployeeId(id);
            return Json(result);
        }

        [Route("delete/{employeeId}")]
        [HttpDelete]
        public IHttpActionResult Delete(int employeeId)
        {
            var result = _employeeService.Delete(employeeId);
            return Json(result);
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult Add([FromBody] AddEmployeeInputInfo inputInfo)
        {
            var result = _employeeService.Add(inputInfo);
            return Json(result);
        }

        [Route("update")]
        [HttpPut]
        public IHttpActionResult Update([FromBody] UpdateEmployeeInputInfo inputInfo)
        {
            var result = _employeeService.Update(inputInfo);
            return Json(result);
        }
    }
}