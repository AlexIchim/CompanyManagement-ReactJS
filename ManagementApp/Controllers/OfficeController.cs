using System.Web.Http;
using Manager.InputInfoModels;
using Manager.Services;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ManagementApp.Controllers
{
    [RoutePrefix("api/offices")]
    [EnableCors("*", "*", "*")]
    public class OfficeController : ApiController
    {
        private readonly OfficeService _officeService;
        private readonly JsonSerializerSettings _camelCaseJsonSettings;

        public OfficeController(OfficeService officeService)
        {
            _officeService = officeService;
            _camelCaseJsonSettings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var result = _officeService.GetAll();
            return Json(result, _camelCaseJsonSettings);
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var result = _officeService.GetById(id);
            return Json(result, _camelCaseJsonSettings);
        }

        [Route("{id}/departments")]
        [HttpGet]
        public IHttpActionResult GetDepartmentsByOfficeId(int id, int? pageSize = null, int? pageNumber = null)
        {
            var result = _officeService.GetDepartmentsByOfficeId(id, pageSize, pageNumber);
            return Json(result, _camelCaseJsonSettings);
        }

        [Route("{id}/departments/count")]
        [HttpGet]
        public IHttpActionResult GetDepartmentCountByOfficeId(int id)
        {
            var result = _officeService.GetDepartmentCountByOfficeId(id);
            return Json(result, _camelCaseJsonSettings);
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult Add([FromBody] AddOfficeInputInfo inputInfo)
        {
            var result = _officeService.Add(inputInfo);
            return Json(result, _camelCaseJsonSettings);
        }

        [Route("update")]
        [HttpPut]
        public IHttpActionResult Update([FromBody] UpdateOfficeInputInfo inputInfo)
        {
            var result = _officeService.Update(inputInfo);
            return Json(result, _camelCaseJsonSettings);
        }

    }
}
