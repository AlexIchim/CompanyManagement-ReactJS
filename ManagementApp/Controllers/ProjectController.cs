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

        [Route("addAssignment")]
        [HttpPost]
        public IHttpActionResult AddAssignment(int employeeId, int projectId, int allocation)
        {
            var result = _projectService.AddAssignment(employeeId, projectId, allocation);
            return Json(result);
        }

        [Route("delete")]
        [HttpDelete]
        public IHttpActionResult Delete(int projectId) {
            var result = _projectService.Delete(projectId);
            return Json(result);
        }

        [Route("deleteEmployeeFromProject")]
        [HttpDelete]
        public IHttpActionResult DeleteEmployeeFromProject(int employeeId, int projectId)
        {
            var result = _projectService.DeleteEmployeeFromProject(employeeId, projectId);
            return Json(result);
        }

        [Route("update")]
        [HttpPut]
        public IHttpActionResult Update([FromBody] UpdateProjectInputInfo inputInfo) {
            var result = _projectService.Update(inputInfo);
            return Json(result);
        }
        [Route("editAllocation")]
        [HttpPut]
        public IHttpActionResult EditAllcation([FromBody] EditAllocationInputInfo inputInfo) {
            var result = _projectService.EditAllocation(inputInfo);
            return Json(result);
        }

        [Route("getAllProjects")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var result = _projectService.GetAll();
            return Json(result);
        }

        [Route("getById")]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var result = _projectService.GetById(id);
            return Json(result);
        }

        [Route("getMembersFromProject")]
        [HttpGet]
        public IHttpActionResult GetMembersFromProject( int projectId)
        {
            var result = _projectService.GetMembersFromProject(projectId);
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
