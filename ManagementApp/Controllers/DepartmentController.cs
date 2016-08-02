using Manager.InputInfoModels;
using Manager.Services;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ManagementApp.Controllers 
{
    [RoutePrefix("api/department")]
    [EnableCors("*", "*", "GET")]
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

        [Route("getAllDepartmentMembers")]
        [HttpGet]
        public IHttpActionResult GetAllDepartmentMembers(int inputInfo)
        {
            var result = _departmentService.GetAllDepartmentMembers(inputInfo);
            return Json(result);
        }



        [Route("GetAllUnAllocatedEmployeesOnProject")]
        [HttpGet]
        public IHttpActionResult GetAllUnAllocatedEmployeesOnProject()
        {
            var result = _departmentService.GetAllUnAllocatedEmployeesOnProject();
            return Json(result);
        }

        [Route("GetEmployeesThatAreNotFullyAllocated")]
        [HttpGet]
        public IHttpActionResult GetEmployeesThatAreNotFullyAllocated()
        {
            var result = _departmentService.GetEmployeesThatAreNotFullyAllocated();
            return Json(result);
        }

        [Route("addEmployeeToDepartment")]
        [HttpPost]
        public IHttpActionResult AddEmployeeToDepartment([FromBody] AddEmployeeToDepartmentInputInfo inputInfo)
        {
            var result = _departmentService.AddEmployeeToDepartment(inputInfo);
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
