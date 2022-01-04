using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LiteHR.Dtos;
using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace LiteHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangeOfNameController : ControllerBase
    {
        private readonly IChangeOfNameService _service;
        private readonly HRContext _context;
        private readonly IHostEnvironment _hostingEnvironment;




        public ChangeOfNameController(IChangeOfNameService service, HRContext context, IHostEnvironment hostingEnvironment)
        {

            _context = context;
            _service = service;
            _hostingEnvironment = hostingEnvironment;

        }


        [HttpPost("MakeRequest")]
        public async Task<int> MakeChangeOfNameRequest([FromForm] ChangeOfNameDto changeOfNameDto)
        {
            var directory = Path.Combine("Resources", "Documents");
            var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, directory);
            return await _service.MakeChangeOfNameReuest(changeOfNameDto, filePath, directory);

        }

        [HttpPost("RequestAction")]
        public async Task<int> ApproveChangeOfNameRequest(long staffId, bool requestAction) => await _service.ApproveChangeOfNameRequest(staffId, requestAction);

        [HttpGet("GetRequestsByAdmin")]
        public async Task<IEnumerable<ChangeofNameListDto>> AdminGetChangeOfNameRequestList() => await _service.AdminGetChangeOfNameRequestList();

        [HttpGet("StaffRequestList")]
        public async Task<IEnumerable<ChangeofNameListDto>> GetRequestListByStaff(long staffId) => await _service.GetRequestListByStaff(staffId);
    }
}
