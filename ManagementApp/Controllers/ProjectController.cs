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

        [Route("add")]
        [HttpPost]
        public IHttpActionResult Add([FromBody] AddProjectInputInfo inputInfo)
        {
            var result = _projectService.Add(inputInfo);
            return Json(result);
        }

        [Route("delete")]
        [HttpDelete]
        public IHttpActionResult Delete(int projectId)
        {
            var result = _projectService.Delete(projectId);
            return Json(result);
        }
    }
}