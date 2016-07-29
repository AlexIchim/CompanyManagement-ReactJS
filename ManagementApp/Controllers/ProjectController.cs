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
        public IHttpActionResult GetEmployeeByProjectId(int ProjectId)
        {
            var result = _projectService.GetByProjectId(ProjectId);
            return Json(result);
        }
    }
}