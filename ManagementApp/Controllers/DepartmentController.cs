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

        [Route("getAllDepartmentProjects")]
        [HttpGet]
        public IHttpActionResult GetAllDepartmentProjects(int inputInfo)
        {
            var result = _departmentService.GetAllDepartmentProjects(inputInfo);
            return Json(result);
        }

        [Route("GetAllUnAllocatedEmployeesOnProject")]
        [HttpGet]
        public IHttpActionResult GetAllUnAllocatedEmployeesOnProject()
        {
            var result = _departmentService.GetAllUnAllocatedEmployeesOnProject();
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
