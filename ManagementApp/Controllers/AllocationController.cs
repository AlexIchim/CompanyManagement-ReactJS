using System.Web.Http;
using Manager.InputInfoModels;
using Manager.Services;

namespace ManagementApp.Controllers
{
    [RoutePrefix("api/allocation")]
    public class AllocationController : ApiController
    {
        private readonly AllocationService _allocationService;

        public AllocationController(AllocationService allocationService)
        {
            _allocationService = allocationService;
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult Add([FromBody] AddAllocationInputInfo inputInfo)
        {
            var result = _allocationService.Add(inputInfo);
            return Json(result);
        }

        [Route("update")]
        [HttpPut]
        public IHttpActionResult Update([FromBody] UpdateAllocationInputInfo inputInfo)
        {
            var result = _allocationService.Update(inputInfo);
            return Json(result);
        }

        [Route("delete/{allocationId}")]
        [HttpDelete]
        public IHttpActionResult Update(int allocationId)
        {
            var result = _allocationService.Delete(allocationId);
            return Json(result);
        }
    }
}