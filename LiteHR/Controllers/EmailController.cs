using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LiteHR.Controllers
{

    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailBodyService _service;

        public EmailController(IEmailBodyService service)
        {
            _service = service;
        }



        [HttpPost]
        public async Task<ActionResult> GetInterviewEmail(List<EmailBody> emailBody)
        {

            await _service.SendEmailMessage(emailBody);
            return Ok("success");
        }



    }
}