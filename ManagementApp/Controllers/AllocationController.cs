using System.Web.Http;
using Manager.InputInfoModels;
using Manager.Services;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ManagementApp.Controllers
{
    [RoutePrefix("api/allocations")]
    [EnableCors("*", "*", "*")]
    public class AllocationController : ApiController
    {
        private readonly AllocationService _allocationService;
        private readonly JsonSerializerSettings _camelCaseJsonSettings;

        public AllocationController(AllocationService allocationService)
        {
            _allocationService = allocationService;
            _camelCaseJsonSettings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult Add([FromBody] AddAllocationInputInfo inputInfo)
        {
            var result = _allocationService.Add(inputInfo);
            return Json(result, _camelCaseJsonSettings);
        }

        [Route("update")]
        [HttpPut]
        public IHttpActionResult Update([FromBody] UpdateAllocationInputInfo inputInfo)
        {
            var result = _allocationService.Update(inputInfo);
            return Json(result, _camelCaseJsonSettings);
        }

        [Route("delete/{allocationId}")]
        [HttpDelete]
        public IHttpActionResult Update(int allocationId)
        {
            var result = _allocationService.Delete(allocationId);
            return Json(result, _camelCaseJsonSettings);
        }
    }
}