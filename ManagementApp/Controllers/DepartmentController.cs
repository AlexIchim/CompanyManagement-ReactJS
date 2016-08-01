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
            var result = _departmentService.GetAll();
            return Json(result);
        }

        [Route("getProjectsByDepartmentId")]
        [HttpGet]
        public IHttpActionResult GetProjectsByDepartmentId(int ID)
        {
            var result = _departmentService.GetProjectsByDepartmentId(ID);
            return Json(result);
        }

        [Route("getEmployeesByDepartmentId")]
        [HttpGet]
        public IHttpActionResult GetEmployeesByDepartmentId(int ID)
        {
            var result = _departmentService.GetEmployeesByDepartmentId(ID);
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
