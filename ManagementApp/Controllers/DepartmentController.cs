using System.Web.Http;
using System.Web.Http.Cors;
using Manager.InputInfoModels;
using Manager.Services;

namespace ManagementApp.Controllers
{
    [RoutePrefix("api/department")]
    [EnableCors("*", "*", "GET, POST, PUT")]
    public class DepartmentController : ApiController
    {
        private readonly DepartmentService _departmentService;

        public DepartmentController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [Route("getAll")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var result = _departmentService.GetAllDepartments();
            return Json(result);
        }

        [Route("getById/{departmentId}")]
        [HttpGet]
        public IHttpActionResult GetDepartmentById(int departmentId) {
            var result = _departmentService.GetDepartmentById(departmentId);
            return Json(result);
        }

        [Route("members/{departmentId}")]
        [HttpGet]
        public IHttpActionResult GetMembersOfDepartment(int departmentId, string name = "", int? jobType = null, int? position = null, int? allocation = null) {
            var result = _departmentService.GetMembersOfDepartment(departmentId, name, jobType, position, allocation);
            return Json(result);
        }   

        [Route("projects/{departmentId}")]
        [HttpGet]
        public IHttpActionResult GetProjectsOfDepartment(int departmentId, int? status = null) {
            var result = _departmentService.GetProjectsOfDepartment(departmentId, status);
            return Json(result);
        }

        [Route("projectsByStatus")]
        [HttpGet]
        public IHttpActionResult FilterProjectsOfADepartmentByStatus(int departmentId, string status)
        {
            var result = _departmentService.FilterProjectsOfADepartmentByStatus(departmentId, status);
            return Json(result);
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult Add([FromBody] AddDepartmentInputInfo inputInfo)
        {
            var result = _departmentService.AddDepartment(inputInfo);
            return Json(result);
        }

        [Route("update")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]UpdateDepartmentInputInfo inputInfo)
        {
            var result = _departmentService.UpdateDepartment(inputInfo);
            return Json(result);
        }
    }
}
