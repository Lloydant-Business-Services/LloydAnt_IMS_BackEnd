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
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IJobVacancyService _jobVacancyService;

        public HomeController(IJobVacancyService jobVacancyService)
        {
            _jobVacancyService = jobVacancyService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobVacancy>>> GetOPENJOBVACANCY()
        {
            return await _jobVacancyService.ActiveJobVacancy();
        }

    }
}