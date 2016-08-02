using System.Web.Http;
using Manager.InputInfoModels;
using Manager.Services;

namespace ManagementApp.Controllers
{
    [RoutePrefix("api/office")]
    public class OfficeController : ApiController
    {
        private readonly OfficeService _officeService;

        public OfficeController(OfficeService officeService)
        {
            _officeService = officeService;
        }

        [Route("getAll")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var result = _officeService.GetAll();
            return Json(result);
        }

        [Route("getById")]
        [HttpGet]
        public IHttpActionResult GetById(int Id)
        {
            var result = _officeService.GetById(Id);
            return Json(result);
        }

        [Route("getDepartmentsByOfficeId")]
        [HttpGet]
        public IHttpActionResult GetByOfficeId(int Id, int? pageSize = null, int? pageNumber = null)
        {
            var result = _officeService.GetDepartmentsByOfficeId(Id, pageSize, pageNumber);
            return Json(result);
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult Add([FromBody] AddOfficeInputInfo inputInfo)
        {
            var result = _officeService.Add(inputInfo);
            return Json(result);
        }

        [Route("update")]
        [HttpPut]
        public IHttpActionResult Update([FromBody] UpdateOfficeInputInfo inputInfo)
        {
            var result = _officeService.Update(inputInfo);
            return Json(result);
        }

    }
}
