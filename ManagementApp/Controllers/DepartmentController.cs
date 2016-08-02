using System.Web.Http;
using Manager.InputInfoModels;
using Manager.Services;

namespace ManagementApp.Controllers
{
    [RoutePrefix("api/department")]
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
        public IHttpActionResult GetAllMembersOfADepartment(int departmentId) {
            var result = _departmentService.GetAllMembersOfADepartment(departmentId);
            return Json(result);
        }

        [Route("projects/{departmentId}")]
        [HttpGet]
        public IHttpActionResult GetAllProjectsOfADepartment(int departmentId) {
            var result = _departmentService.GetAllProjectsOfADepartment(departmentId);
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
