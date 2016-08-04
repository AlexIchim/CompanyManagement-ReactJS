using Manager.InputInfoModels;
using Manager.Services;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ManagementApp.Controllers
{
    [RoutePrefix("api/office")]
    [EnableCors("*", "*", "GET,POST")]
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

        [Route("getAllDepOffice")]
        [HttpGet]
        public IHttpActionResult GetAllDepartmentsOfAnOffice(int officeId,int? pageSize, int? pageNr)
        {
            var result = _officeService.GetAllDepartmentsOfAnOffice(officeId, pageSize, pageNr);
            return Json(result);
        }


        [Route("add")]
        [HttpPost]
        public IHttpActionResult Add([FromBody] AddOfficeInputInfo inputInfo)
        {
            var result = _officeService.Add(inputInfo);
            return Json(result);
        }

        [Route("updateOffice")]
        [HttpPut]
        public IHttpActionResult UpdateOffice([FromBody] UpdateOfficeInputInfo inputInfo)
        {
            var result = _officeService.UpdateOffice(inputInfo);
            return Json(result);
        }

    }
}