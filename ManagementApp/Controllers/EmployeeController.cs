using Manager.Services;
using Manager.InputInfoModels;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ManagementApp.Controllers
{
    [RoutePrefix("api/employees")]
    [EnableCors("*", "*", "*")]
    public class EmployeeController : ApiController
    {
        private readonly EmployeeService _employeeService;
        private readonly JsonSerializerSettings _camelCaseJsonSettings;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
            _camelCaseJsonSettings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var result = _employeeService.GetAll();
            return Json(result, _camelCaseJsonSettings);
        }

        [Route("managers")]
        [HttpGet]
        public IHttpActionResult GetAllDepartmentManagers()
        {
            var result = _employeeService.GetAllDepartmentManagers();
            return Json(result, _camelCaseJsonSettings);
        }

        [Route("available")]
        [HttpGet]
        public IHttpActionResult GetAvailableEmployees(int? departmentId = null, int? positionId = null, int? projectId = null, int? pageSize = null, int? pageNumber = null)
        {
            var result = _employeeService.GetAvailableEmployees(departmentId, positionId, projectId, pageSize, pageNumber);
            return Json(result, _camelCaseJsonSettings);
        }


        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var result = _employeeService.GetById(id);
            return Json(result, _camelCaseJsonSettings);
        }

        [Route("{id}/allocations")]
        [HttpGet]
        public IHttpActionResult GetAllocationsByEmployeeId(int id)
        {
            var result = _employeeService.GetAllocationsByEmployeeId(id);
            return Json(result, _camelCaseJsonSettings);
        }

        [Route("delete/{employeeId}")]
        [HttpDelete]
        public IHttpActionResult Delete(int employeeId)
        {
            var result = _employeeService.Delete(employeeId);
            return Json(result, _camelCaseJsonSettings);
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult Add([FromBody] AddEmployeeInputInfo inputInfo)
        {
            var result = _employeeService.Add(inputInfo);
            return Json(result, _camelCaseJsonSettings);
        }

        [Route("update")]
        [HttpPut]
        public IHttpActionResult Update([FromBody] UpdateEmployeeInputInfo inputInfo)
        {
            var result = _employeeService.Update(inputInfo);
            return Json(result, _camelCaseJsonSettings);
        }
    }
}