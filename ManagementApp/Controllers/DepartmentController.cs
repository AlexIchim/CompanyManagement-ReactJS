using Manager.InputInfoModels;
using Manager.Services;
using System.Web.Http;
using System.Web.Http.Cors;
namespace ManagementApp.Controllers
{
    [RoutePrefix("api/department")]
    [EnableCors("*", "*", "GET,POST,PUT")]
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

        [Route("addDepartment")]
        [HttpPost]
        public IHttpActionResult AddDepartment([FromBody] AddDepartmentInputInfo inputInfo)
        {
            var result = _departmentService.AddDepartment(inputInfo);
            return Json(result);
        }

        [Route("getAllDepartmentManagers")]
        [HttpGet]
        public IHttpActionResult GetAllDepartmentManagers()
        {
            var result = _departmentService.GetAllDepartmentManagers();
            return Json(result);
        }

        [Route("UpdateDepartment")]
        [HttpPut]
        public IHttpActionResult UpdateDepartment([FromBody]UpdateDepartmentInputInfo inputInfo)
        {
            var result = _departmentService.UpdateDepartment(inputInfo);
            return Json(result);
        }

    }
}
