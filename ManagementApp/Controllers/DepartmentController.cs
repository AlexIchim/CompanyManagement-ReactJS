using System.Web.Http;
using Manager.InputInfoModels;
using Manager.Services;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ManagementApp.Controllers
{
    [RoutePrefix("api/departments")]
    [EnableCors("*", "*", "*")]
    public class DepartmentController : ApiController
    {
        private readonly DepartmentService _departmentService;
        private readonly JsonSerializerSettings _camelCaseJsonSettings;

        public DepartmentController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
            _camelCaseJsonSettings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var result = _departmentService.GetAll();
            return Json(result, _camelCaseJsonSettings);
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var result = _departmentService.GetById(id);
            return Json(result, _camelCaseJsonSettings);
        }

        [Route("{id}/projects")]
        [HttpGet]
        public IHttpActionResult GetProjectsByDepartmentId(int id, int? pageSize = null, int? pageNumber = null)
        {
            var result = _departmentService.GetProjectsByDepartmentId(id, pageSize, pageNumber);
            return Json(result, _camelCaseJsonSettings);
        }

        [Route("{id}/employees")]
        [HttpGet]
        public IHttpActionResult GetEmployeesByDepartmentId(int id, int? pageSize = null, int? pageNumber = null)
        {
            var result = _departmentService.GetEmployeesByDepartmentId(id, pageSize, pageNumber);
            return Json(result, _camelCaseJsonSettings);
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult Add([FromBody] AddDepartmentInputInfo inputInfo)
        {
            var result = _departmentService.Add(inputInfo);
            return Json(result, _camelCaseJsonSettings);
        }

        [Route("update")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]UpdateDepartmentInputInfo inputInfo)
        {
            var result = _departmentService.Update(inputInfo);
            return Json(result, _camelCaseJsonSettings);
        }
    }
}
