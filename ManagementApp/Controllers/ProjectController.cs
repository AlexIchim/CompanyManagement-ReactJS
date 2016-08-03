﻿using System.Web.Http;
using Manager.InputInfoModels;
using Manager.Services;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ManagementApp.Controllers
{
    [RoutePrefix("api/projects")]
    [EnableCors("*", "*", "*")]
    public class ProjectController : ApiController
    {
        private readonly ProjectService _projectService;
        private readonly JsonSerializerSettings _camelCaseJsonSettings;

        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
            _camelCaseJsonSettings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var result = _projectService.GetAll();
            return Json(result, _camelCaseJsonSettings);
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var result = _projectService.GetById(id);
            return Json(result, _camelCaseJsonSettings);
        }

        [Route("{id}/employees")]
        [HttpGet]
        public IHttpActionResult GetEmployeesByProjectId(int id, int? pageSize = null, int? pageNumber = null)
        {
            var result = _projectService.GetEmployeesByProjectId(id, pageSize, pageNumber);

            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return Json(result, _camelCaseJsonSettings);
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult Add([FromBody] AddProjectInputInfo inputInfo)
        {
            var result = _projectService.Add(inputInfo);
            return Json(result, _camelCaseJsonSettings);
        }

        [Route("update")]
        [HttpPut]
        public IHttpActionResult Update([FromBody] UpdateProjectInputInfo inputInfo)
        {
            var result = _projectService.Update(inputInfo);
            return Json(result, _camelCaseJsonSettings);
        }

        [Route("delete/{projectId}")]
        [HttpDelete]
        public IHttpActionResult Delete(int projectId)
        {
            var result = _projectService.Delete(projectId);
            return Json(result, _camelCaseJsonSettings);
        }
    }
}
