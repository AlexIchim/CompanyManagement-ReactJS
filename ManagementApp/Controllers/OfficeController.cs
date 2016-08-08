using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Manager.InputInfoModels;
using Manager.Services;

namespace ManagementApp.Controllers
{
    [RoutePrefix("api/office")]
    [EnableCors("*","*","GET, POST, PUT")]
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

        [Route("departments/{officeId}")]
        [HttpGet]
        public IHttpActionResult GetAllDepartmentsOfAnOffice(int officeId) {
            var result = _officeService.GetAllDepartmentsOfAnOffice(officeId);
            return Json(result);
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult Add()
        {
            IEnumerable<HttpContent> parts = Request.Content.ReadAsMultipartAsync().Result.Contents;


            var media = parts.ToArray()[1].ReadAsByteArrayAsync().Result;

            //var result = _officeService.Add(inputInfo);
            return Json(Json(new Object()));
        }

        [Route("update")]
        [HttpPost]
        public IHttpActionResult Update([FromBody] UpdateOfficeInputInfo inputInfo)
        {
            var result = _officeService.Update(inputInfo);
            return Json(result); 
        }

    }
}
