﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Manager.InputInfoModels;
using Manager.Services;

namespace ManagementApp.Controllers
{
    [RoutePrefix("api/office")]
    [EnableCors("*", "*", "*")]
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

        [Route("departmentsCount/{officeId}")]
        [HttpGet]
        public IHttpActionResult GetAllDepartmentsOfAnOffice(int officeId)
        {
            return Json(_officeService.CountAllDepartmentsOfAnOffice(officeId));
        }

        [Route("departments/{officeId}/{pageSize}/{pageNumber}")]
        [HttpGet]
        public IHttpActionResult GetAllDepartmentsOfAnOffice(int officeId, int pageSize, int pageNumber) {
            var result = _officeService.GetAllDepartmentsOfAnOffice(officeId, pageSize, pageNumber);
            return Json(result);
        }

        [Route("availableEmployees/{officeId}/{pageSize}/{pageNumber}")]
        [HttpGet]
        public IHttpActionResult GetAllAvailableEmployeesOfAnOffice(int officeId, int pageSize, int pageNumber, int? department = null, int? position = null) {
            var result = _officeService.GetAllAvailableEmployeesOfAnOffice(officeId, pageSize, pageNumber, department, position);
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
