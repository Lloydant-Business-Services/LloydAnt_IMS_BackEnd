using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace LiteHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffAdditionalInfoController : ControllerBase
    {
        private readonly HRContext _context;
        private readonly IStaffAdditionalInfoService _service;
        private readonly IHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;
        private readonly string baseUrl;

        public StaffAdditionalInfoController(HRContext context, IStaffAdditionalInfoService service, IHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _context = context;
            _service = service;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            baseUrl = _configuration.GetValue<string>("Url:root");
        }

        [HttpGet("[action]")]
        public async Task<StaffAdditionalInfo> GetStaffAdditionalInfoByStaffId(long id)
        {
            StaffAdditionalInfo info = new StaffAdditionalInfo();
            var staff = await _context.STAFF_ADDITIONAL_INFO.Where(a => a.staffId == id).FirstOrDefaultAsync();
            if(staff != null)
            {
                info.AreaOfSpecializationId = staff.AreaOfSpecializationId;
                info.RsaNumber = staff.RsaNumber;
                info.SignatureUrl = staff.SignatureUrl != null ? baseUrl + staff.SignatureUrl : null;
                info.PfaNameId = staff.PfaStatusId;
                info.PfaStatusId = staff.PfaStatusId;
                return info;

            }
            return null;
        }

        [HttpPost("[action]")]
        public async Task<long> UploadStaffSignature(IFormFile file, long staffId, long pin)
        {
            var directory = Path.Combine("Resources", "Passport");
            var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, directory);
            return await _service.UploadStaffSignature(file, staffId, pin, filePath, directory);

        }
        [HttpGet("[action]")]
        public async Task<string> GetStaffSignatureByPin(long signaturePin, long staffId) => await _service.GetStaffSignatureByPin(signaturePin, staffId);
    }
}
