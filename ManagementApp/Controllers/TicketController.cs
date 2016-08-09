using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Contracts;
using Manager.InfoModels;
using Manager.InputInfoModels;
using Manager.Services;

namespace ManagementApp.Controllers
{
    [RoutePrefix("api/ticket")]
    [EnableCors("*","*","GET,POST,PUT,DELETE")]
    public class TicketController : ApiController
    {

        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [Route("getAll")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var result = _ticketService.GetAll();
            return Json(result);
        }

        [Route("create")]
        [HttpPost]
        public IHttpActionResult Create([FromBody] AddTicketInputInfo ticketInputInfo)
        {
            var result = _ticketService.Create(ticketInputInfo);
            return Json(result);

        }

        [Route("update")]
        [HttpPost]
        public IHttpActionResult Update([FromBody] UpdateTicketInputInfo ticketInputInfo)
        {
            var result = _ticketService.Update(ticketInputInfo);
            return Json(result);
        }

    }
}