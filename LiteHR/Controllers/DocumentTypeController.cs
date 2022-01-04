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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace LiteHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTypeController : ControllerBase
    {
        private readonly IStaffDocumentService _service;
        private readonly HRContext _context;
        private readonly IHostEnvironment _hostingEnvironment;




        public DocumentTypeController(IStaffDocumentService service, HRContext context, IHostEnvironment hostingEnvironment)
        {
           
            _context = context;
            _service = service;
            _hostingEnvironment = hostingEnvironment;

        }

        [HttpPost]
        public async Task<ActionResult<int>> AddDocumentType(DocumentType documentType)
        {
            _context.DOCUMENT_TYPE.Add(documentType);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
        }

        [HttpGet]
        public async Task<IEnumerable<DocumentType>> GetDocumentType()
        {
            return await _context.DOCUMENT_TYPE.ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DocumentType>> EditDocumentType(long id, DocumentType documentType)
        {
            if (id != documentType.Id)
            {
                return BadRequest();
            }
            _context.DOCUMENT_TYPE.Update(documentType);
            await _context.SaveChangesAsync();
            return documentType;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteDocumentType(long id)
        {
            var getDocumentType = await _context.DOCUMENT_TYPE.FindAsync(id);
            if (getDocumentType == null)
            {
                return NotFound();
            }

            _context.DOCUMENT_TYPE.Remove(getDocumentType);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
        }

        [HttpPost("StaffDocumentUpload")]
        public async Task<int> StaffDocumentUpload([FromForm]StaffDocumentUploadDto staffDocumentUploadDto)
        {
            var directory = Path.Combine("Resources", "Document");
            var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, directory);
            return await _service.StaffDocumentUpload(staffDocumentUploadDto, filePath, directory);

        }
        [HttpGet("StaffUploadHistory")]
        public async Task<IEnumerable<StaffUploadedDocxDto>> StaffDocumentUploadHistory(long personId) => await _service.StaffDocumentUploadHistory(personId);

        
        [HttpPost("StaffDocumentUploadByAdmin")]
        public async Task<int> StaffDocumentUploadByAdmin([FromForm]StaffDocumentUploadDto staffDocumentUploadDto)
        {
            var directory = Path.Combine("Resources", "Document");
            var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, directory);
            return await _service.StaffDocumentUploadByAdmin(staffDocumentUploadDto, filePath, directory);
        }

        [HttpGet("GetStaffDocumentbyStaffNumber")]
        public async Task<IEnumerable<StaffUploadedDocxDto>> GetStaffDocumentUploadHistoryByAdmin(string staffNumber) => await _service.GetStaffDocumentUploadHistoryByAdmin(staffNumber);

        [HttpPost("VerifyStaffDocument")]
        public async Task<int> VerifyStaffDocumentByAdmin(DocumentVerificationDto documentVerificationDto) => await _service.VerifyStaffDocumentByAdmin(documentVerificationDto);
    }
}

