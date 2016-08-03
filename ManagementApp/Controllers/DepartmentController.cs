using System.Web.Http;
using Manager.InputInfoModels;
using Manager.Services;
using System.Web.Http.Cors;

namespace ManagementApp.Controllers
{
    [RoutePrefix("api/department")]
    [EnableCors("*", "*", "*")]
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
            var result = _departmentService.GetAll();
            return Json(result);
        }

        [Route("getById")]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var result = _departmentService.GetById(id);
            return Json(result);
        }

        [Route("getProjectsByDepartmentId")]
        [HttpGet]
        public IHttpActionResult GetProjectsByDepartmentId(int ID, int? pageSize = null, int? pageNumber = null)
        {
            var result = _departmentService.GetProjectsByDepartmentId(ID, pageSize, pageNumber);
            return Json(result);
        }

        [Route("getEmployeesByDepartmentId")]
        [HttpGet]
        public IHttpActionResult GetEmployeesByDepartmentId(int ID, int? pageSize = null, int? pageNumber = null)
        {
            var result = _departmentService.GetEmployeesByDepartmentId(ID, pageSize, pageNumber);
            return Json(result);
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult Add([FromBody] AddDepartmentInputInfo inputInfo)
        {
            var result = _departmentService.Add(inputInfo);
            return Json(result);
        }

        [Route("update")]
        [HttpPost]
        public IHttpActionResult Update([FromBody]UpdateDepartmentInputInfo inputInfo)
        {
            var result = _departmentService.Update(inputInfo);
            return Json(result);
        }
    }
}
