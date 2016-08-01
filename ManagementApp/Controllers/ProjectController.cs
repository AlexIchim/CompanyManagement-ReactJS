using System.Web.Http;
using Manager.InputInfoModels;
using Manager.Services;

namespace ManagementApp.Controllers
{
    [RoutePrefix("api/project")]
    public class ProjectController : ApiController
    {
        
        private readonly ProjectService _projectService;

        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }
       
        [Route("getEmployeeByProjectId")]
        [HttpGet]
        public IHttpActionResult GetEmployeeByProjectId(int projectId)
        {
            var result = _projectService.GetByProjectId(projectId);
            return Json(result);
        }

        [Route ("getEmployeesAllocation")]
        [HttpGet]
        public IHttpActionResult GetEmployeesAllocation(int projectId)
        {
            var result = _projectService.GetEmployeesAllocation(projectId);
            return Json(result);
        }

        [Route("addEmployeeToProject")]
        [HttpPost]
        public IHttpActionResult AddEmployeeToProject([FromBody] AddEmployeeToProjectInputInfo inputInfo)
        {
            var result = _projectService.AddEmployeeToProject(inputInfo);
            return Json(result);
        }
    }
}