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
        public IHttpActionResult GetByOfficeId(int Id)
        {
            var result = _officeService.GetDepartmentsByOfficeId(Id);
            return Json(result);
        }

    }
}
