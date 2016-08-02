using Manager.InputInfoModels;
using Manager.InputInfoModels;
using Manager.Services;
using System.Web.Http;

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

        [Route("getAllDepOffice")]
        [HttpGet]
        public IHttpActionResult GetAllDepartmentsOfAnOffice(int officeId)
        {
            var result = _officeService.GetAllDepartmentsOfAnOffice(officeId);
            return Json(result);
        }


        [Route("add")]
        [HttpPost]
        public IHttpActionResult Add([FromBody] AddOfficeInputInfo inputInfo)
        {
            var result = _officeService.Add(inputInfo);
            return Json(result);
        }

        [Route("addDepartment")]
        [HttpPost]
        public IHttpActionResult AddDepartment([FromBody] AddDepartmentInputInfo inputInfo)
        {
            var result = _officeService.AddDepartment(inputInfo);
            return Json(result);
        }

        [Route("UpdateDepartment")]
        [HttpPut]
        public IHttpActionResult UpdateDepartment([FromBody]UpdateDepartmentInputInfo inputInfo)
        {
            var result = _officeService.UpdateDepartment(inputInfo);
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