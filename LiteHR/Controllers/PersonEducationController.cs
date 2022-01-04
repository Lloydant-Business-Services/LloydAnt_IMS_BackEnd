using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteHR.Dtos;
using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace LiteHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonEducationController : ControllerBase
    {
        private readonly IPersonEducationService _service;
        private readonly IHostEnvironment _hostingEnvironment;
        private readonly HRContext _context;
        private readonly IConfiguration _configuration;
        private readonly string baseUrl;



        public PersonEducationController(IPersonEducationService service, IHostEnvironment hostingEnvironment, HRContext context, IConfiguration configuration)
        {
            _service = service;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _configuration = configuration;
            baseUrl = _configuration.GetValue<string>("Url:root");

        }
        [HttpGet("[action]")]
        public async Task<IEnumerable<GetPersonEducationDto>> GetPersonEducationByPersonId(long personId) => await _service.GetPersonEducationByPersonId(personId);
        [HttpPost("[action]")]
        public async Task<long> AddPersonEducation(long personId, GetPersonEducationDto dto) => await _service.AddPersonEducation(personId, dto);
        [HttpPut("[action]")]
        public async Task<int> UpdatePersonEducation(GetPersonEducationDto dto) => await _service.UpdatePersonEducation(dto);
        [HttpDelete("[action]")]
        public async Task<int> DeletePersonEducation(long personEducationId) => await _service.DeletePersonEducation(personEducationId);
    }
}
