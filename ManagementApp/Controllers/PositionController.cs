using System.Web.Http;
using Manager.InputInfoModels;
using Manager.Services;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ManagementApp.Controllers
{
    [RoutePrefix("api/positions")]
    [EnableCors("*", "*", "*")]
    public class PositionController : ApiController
    {
        private readonly PositionService _positionService;
        private readonly JsonSerializerSettings _camelCaseJsonSettings;

        public PositionController(PositionService positionService)
        {
            _positionService = positionService;
            _camelCaseJsonSettings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var result = _positionService.GetAll();
            return Json(result, _camelCaseJsonSettings);
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var result = _positionService.GetById(id);
            return Json(result, _camelCaseJsonSettings);
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult Add([FromBody] AddPositionInputInfo inputInfo)
        {
            var result = _positionService.Add(inputInfo);
            return Json(result, _camelCaseJsonSettings);
        }

        [Route("update")]
        [HttpPut]
        public IHttpActionResult Update([FromBody] UpdatePositionInputInfo inputInfo)
        {
            var result = _positionService.Update(inputInfo);
            return Json(result, _camelCaseJsonSettings);
        }
    }
}