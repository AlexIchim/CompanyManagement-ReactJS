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

        [Route("add")]
        [HttpPost]
        public IHttpActionResult Add([FromBody] AddProjectInputInfo inputInfo)
        {
            var result = _projectService.Add(inputInfo);
            return Json(result);
        }

        [Route("getById")]
        [HttpGet]
        public IHttpActionResult GetById([FromBody] int id)
        {
            var result = _projectService.GetById(id);
            return Json(result);
        }

        [Route("getAllProjectMembers")]
        [HttpGet]
        public IHttpActionResult GetAllMembersFromProject( int projectId)
        {
            var result = _projectService.GetAllMembersFromProject(projectId);
            return Json(result);
        }

        [Route("getAllocation")]
        [HttpGet]
        public IHttpActionResult GetAllocationOfEmployeeFromProject(int projectId, int employeeId)
        {
            var result = _projectService.GetAllocationOfEmployeeFromProject(projectId, employeeId);
            return Json(result);
        }
        [Route("getNrTeamMembers")]
        [HttpGet]
        public IHttpActionResult GetNrTeamMembers(int projectId)
        {
            var result = _projectService.GetNrTeamMembers(projectId);
            return Json(result);
        }



    }
}
