//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using LiteHR.Dtos;
//using LiteHR.Interface;
//using LiteHR.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Hosting;

//namespace LiteHR.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class MailingController : ControllerBase
//    {
//        private readonly HRContext _context;
//        private readonly IMailingService _service;
//        private readonly IHostEnvironment _hostingEnvironment;

//        public MailingController(HRContext context, IMailingService service, IHostEnvironment hostingEnvironment)
//        {
//            _context = context;
//            _service = service;
//            _hostingEnvironment = hostingEnvironment;
//        }

//        [HttpPost("[action]")]
//        public async Task<long> PostNewMail([FromForm]NewMailingDto mailingDto)
//        {
//            var directory = Path.Combine("Resources", "Memos");
//            var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, directory);
//            return await _service.PostNewMail(mailingDto, filePath, directory);
//        }

//        [HttpGet("[action]")]
//        public async Task<IEnumerable<GetMailDto>> GetMailInbox(long RoleId, long DeptId, long FacultyId, long staffId) => await _service.GetMailInbox(RoleId, DeptId, FacultyId, staffId);

//        [HttpPost("[action]")]
//        public async Task<int> PostMailingAction(MailingActionDto actionDto) => await _service.PostMailingAction(actionDto);

//        [HttpPost("[action]")]
//        public async Task<int> ActivateRead(long deskChainId) => await _service.ActivateRead(deskChainId);
//        [HttpGet("[action]")]
//        public async Task<IEnumerable<TrailingCommentsDto>> GetMailTrailingCommentsByMailId(long mailingId) => await _service.GetMailTrailingCommentsByMailId(mailingId);
//        [HttpGet("[action]")]
//        public async Task<IEnumerable<FilterByRoleDto>> FilterByRole(long roleId) => await _service.FilterByRole(roleId);

//        [HttpGet("[action]")]
//        public async Task<MailingStatusDto> GetMailingDeskStatus(long mailingId) => await _service.GetMailingDeskStatus(mailingId);
//        [HttpGet("[action]")]
//        public async Task<IEnumerable<GetMailDto>> GetSentMail(long userId) => await _service.GetSentMail(userId);

//        [HttpGet("[action]")]
//        public async Task<long> GetNewMailCount(long RoleId, long DeptId, long FacultyId) => await _service.GetNewMailCount(RoleId, DeptId, FacultyId);
//        [HttpGet("[action]")]
//        public async Task<IEnumerable<GetMailingStaffDto>> GetSpecificMailingStaff(long departmentId) => await _service.GetSpecificMailingStaff(departmentId);
//        [HttpGet("[action]")]
//        public async Task<IEnumerable<GetMailDto>> GetMailActionArchive(long RoleId, long DeptId, long FacultyId, long staffId) => await _service.GetMailActionArchive(RoleId, DeptId, FacultyId, staffId);
//        [HttpPost("[action]")]
//        public async Task<int> CopyMailToAction(CopyListDto mailingDto, long mailingId) => await _service.CopyMailToAction(mailingDto, mailingId);
//        [HttpGet("[action]")]
//        public async Task<IEnumerable<GetMailingStaffDto>> SearchRecipient(string search) => await _service.SearchRecipient(search);
//        [HttpPost("[action]")]
//        public async Task<long> ArchiveMail(ArchiveMailDto dto) => await _service.ArchiveMail(dto);
//        [HttpGet("[action]")]
//        public async Task<IEnumerable<MailArchiveFileType>> GetMailingFileType() => await _service.GetMailingFileType();
//        [HttpGet("[action]")]
//        public async Task<IEnumerable<GetMailDto>> GetMailArchives(long fileTypeId, string searchParam, DateTime dateFrom, DateTime dateTo) => await _service.GetMailArchives(fileTypeId, searchParam, dateFrom, dateTo);
//        [HttpGet("[action]")]
//        public async Task<string> SignatureToBase64(string signatureUrl) => await _service.SignatureToBase64(signatureUrl);
//        [HttpGet("[action]")]
//        public async Task<IEnumerable<GetMailDto>> GetConfidentialMail() => await _service.GetConfidentialMail();
//        [HttpGet("[action]")]
//        public async Task<IEnumerable<GetMailDto>> SearchMailInbox(long RoleId, long DeptId, long FacultyId, long staffId, long fileTypeId, string searchParam, DateTime dateFrom, DateTime dateTo) => await _service.SearchMailInbox(RoleId, DeptId, FacultyId, staffId, fileTypeId, searchParam, dateFrom, dateTo);
//        [HttpGet("[action]")]
//        public async Task<IEnumerable<GetMailDto>> SearchSentMail(long userId, long fileTypeId, string searchParam, DateTime dateFrom, DateTime dateTo) => await _service.SearchSentMail(userId, fileTypeId, searchParam, dateFrom, dateTo);
//        [HttpGet("[action]")]
//        public async Task<IEnumerable<GetMailDto>> GetAllMailArchives() => await _service.GetAllMailArchives();
//        [HttpGet("[action]")]
//        public async Task<IEnumerable<GetMailDto>> GetSentMailExternal(string originatorEmail) => await _service.GetSentMailExternal(originatorEmail);
//    }

//}
