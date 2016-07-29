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

        [Route("getAll")]
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

        [Route("getByDepartmentId")]
        [HttpGet]
        public IHttpActionResult GetByDepartmentId(int ID)
        {
            var result = _projectService.GetByDepartmentId(ID);
            return Json(result);
        }

    }
}
