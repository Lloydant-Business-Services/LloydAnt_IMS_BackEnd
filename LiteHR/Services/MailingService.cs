//using ImageMagick;
//using LiteHR.Dtos;
//using LiteHR.Infrastructure;
//using LiteHR.Interface;
//using LiteHR.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Hosting;
//using Newtonsoft.Json.Linq;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Drawing.Imaging;
//using System.IO;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Net;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;
//using Windows.UI.Xaml.Media.Imaging;

//namespace LiteHR.Services
//{
//    public class MailingService : IMailingService
//    {
//        private readonly HRContext _context;
//        private readonly IFileUpload _fileupload;
//        private readonly string baseUrl;
//        private readonly IConfiguration _configuration;
//        private readonly IHostEnvironment _hostingEnvironment;

//        //private readonly IMailingService _mailingService;
//        public int _saved { get; set; } = 1;

//        public MailingService(HRContext context, IFileUpload fileupload, IConfiguration configuration, IHostEnvironment hostingEnvironment)
//        {
//            _context = context;
//            _fileupload = fileupload;
//            _configuration = configuration;
//            _hostingEnvironment = hostingEnvironment;
//            // _mailingService = service;
//            baseUrl = _configuration.GetValue<string>("Url:root");
//        }


//        public async Task<long> PostNewMail(NewMailingDto mailingDto, string filePath, string directory)
//        {
//            try
//            {
//                //JObject json = JObject.Parse(mailingDto.CopyList);
//                //string jsonString = JsonSerializer.Serialize(mailingDto.CopyList);
//                Mailing mailing = new Mailing();
//                MailingDeskChain mailingDesk = new MailingDeskChain();
//                User getUser = await _context.USER.Where(d => d.Id == mailingDto.OriginatorUserId)
//                       .Include(s => s.Staff).ThenInclude(d => d.Department).ThenInclude(f => f.Faculty)
//                       .Include(r => r.Role)
//                       .FirstOrDefaultAsync();
//                var saveDocLink = string.Empty;
//                if (getUser == null || getUser.Staff.DepartmentId == null)
//                    throw new NullReferenceException("User not Found");
//                if (mailingDto.AttachmentUrl != null)
//                {
//                    var docNamePrefix = "Memo" + "_" + mailingDto.Title.Replace(" ", "_") + "_" + DateTime.Now.Millisecond;
//                    saveDocLink = await _fileupload.UploadWithoutWaterMark(mailingDto.AttachmentUrl, filePath, directory, docNamePrefix);
                    
//                }
//                mailing.Title = mailingDto.Title;
//                mailing.Body = mailingDto.Body;
//                if (!string.IsNullOrEmpty(saveDocLink))
//                {
//                    mailing.AttachmentUrl = saveDocLink;
//                }
//                if(mailingDto.FileTypeId > 0)
//                {
//                    mailing.FileTypeId = mailingDto.FileTypeId;
//                }
//                if (mailingDto.IsConfidential)
//                {
//                    mailing.IsConfidential = true;
//                }
//                mailing.DateEntered = DateTime.Now;
//                mailing.OriginatorId = getUser.Id;
//                if (!mailingDto.IsExternal)
//                {
                    
//                    mailing.OriginatorRoleDepartment = getUser.Role.Id == (int)UserRole.HOD || getUser.Role.Id == (int)UserRole.DeputyRegistrar ? getUser.Role.Name + " " + getUser.Staff.Department.Name : getUser.Role.Id == (int)UserRole.Dean ? getUser.Role.Name + " " + getUser.Staff.Department.Faculty.Name : getUser
//                   .Role.Name;
//                }
//                else
//                {
//                    mailing.IsExternal = true;
//                    mailing.OriginatorRoleDepartment = mailingDto.OriginatorExternal;
//                    mailing.OriginatorEmail = mailingDto.OriginatorEmail.Trim();


//                }
               
//                if (!String.IsNullOrEmpty(mailingDto.SignatureUrl))
//                {
//                    mailing.OriginatorSignatureUrl = mailingDto.SignatureUrl;

//                }

               
//                _context.Add(mailing);
//                var isSaved = await _context.SaveChangesAsync();

//                if(isSaved == _saved)
//                {
//                    mailingDesk.MailingId = mailing.Id;
                    
//                    if(mailingDto.RoleId == (int)UserRole.VC)
//                    {
//                        mailingDesk.RoleId = (int)UserRole.VC_IncomingMailOfficer;
//                    }
//                    else if (mailingDto.RoleId > 0)
//                    {
//                        mailingDesk.RoleId = mailingDto.RoleId;
//                    }

//                    if (mailingDto.DepartmentId > 0)
//                    {
//                        mailingDesk.InstitutionDepartmentId = mailingDto.DepartmentId;
//                    }
//                    if(mailingDto.ToStaffId > 0)
//                    {
//                        mailingDesk.StaffId = mailingDto.ToStaffId;
//                    }
//                    if (mailingDto.FacultyId > 0)
//                    {
//                        mailingDesk.FacultyId = mailingDto.FacultyId;
//                    }
//                    mailingDesk.IsActive = true;
//                    _context.Add(mailingDesk);
//                    await _context.SaveChangesAsync();


//                    return mailing.Id;
//                }

//                return 0;
//            }
//            catch(Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public async Task<int> CopyMailToAction(CopyListDto mailingDto, long mailingId)
//        {
//            try
//            {
//                Mailing mailing = new Mailing();
//                MailingDeskChain mailingDesk = new MailingDeskChain();
//                if (mailingDto != null && mailingDto.RoleId > 0 && mailingId > 0)
//                {
                   
//                        if (mailingDto.RoleId > 0 && mailingDto.RoleId == (int)UserRole.Dean)
//                        {
//                            mailingDesk.FacultyId = mailingDto.Id;
//                        }
//                        if (mailingDto.RoleId > 0 && mailingDto.RoleId == (int)UserRole.HOD)
//                        {
//                            mailingDesk.InstitutionDepartmentId = mailingDto.Id;
//                        }
//                        if (mailingDto.RoleId > 0 && mailingDto.RoleId == (int)UserRole.DeputyRegistrar)
//                        {
//                            mailingDesk.InstitutionDepartmentId = mailingDto.Id;
//                        }

//                    mailingDesk.RoleId = mailingDto.RoleId;
//                        mailingDesk.MailingId = mailingId;
//                        mailingDesk.IsActive = false;
//                        mailingDesk.IsCopied = true;
//                        _context.Add(mailingDesk);
//                        await _context.SaveChangesAsync();
//                        return StatusCodes.Status200OK;
//                    }
                

//                return 0;

//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public async Task<IEnumerable<GetMailDto>> GetSentMail(long userId)
//        {
//            List<GetMailDto> mailing = new List<GetMailDto>();
//            mailing = await _context.MAILING.Where(f => f.OriginatorId == userId)
//                .Include(f => f.FileType)
//                .Select(m => new GetMailDto
//                {
//                    MailingId = m.Id,
//                    Body = m.Body,
//                    DateEntered = m.DateEntered,
//                    IsAcknowledged = m.IsAcknowledged,
//                    IsClosed = m.IsClosed,
//                    IsRejected = m.IsRejected,
//                    OriginatorInfo = m.OriginatorRoleDepartment,
//                    AttachmentUrl = !string.IsNullOrWhiteSpace(m.AttachmentUrl) ? baseUrl + m.AttachmentUrl : null,
//                    Title = m.Title,
//                    SignatureUrl = m.OriginatorSignatureUrl,
//                    FileName = m.FileType.Title,
//                    FileTypeId = m.FileType.Id,
//                    FileNumber = m.FileType.FileNumber

//                })
//                .OrderByDescending(d => d.MailingId)
//                .ToListAsync();
//            return mailing;
//        }
//        public async Task<IEnumerable<GetMailDto>> GetSentMailExternal(string originatorEmail)
//        {
//            List<GetMailDto> mailing = new List<GetMailDto>();
//            var _OriginatorEmail = originatorEmail.Trim();
//           var _mailing = await _context.MAILING.Where(f => f.OriginatorEmail == _OriginatorEmail)
//                .Include(f => f.FileType)              
//                .OrderByDescending(d => d.Id)
//                .ToListAsync();
//            if(_mailing != null && _mailing.Count > 0)
//            {
//                foreach (var item in _mailing)
//                {
//                    var mailStatus = await GetExternalDeskStatus(item.Id);
//                    GetMailDto dto = new GetMailDto();
//                    dto.MailingId = item.Id;
//                    dto.Body = item.Body;
//                    dto.DateEntered = item.DateEntered;
//                    dto.IsAcknowledged = item.IsAcknowledged;
//                    dto.IsClosed = item.IsClosed;
//                    dto.IsRejected = item.IsRejected;
//                    dto.OriginatorInfo = item.OriginatorRoleDepartment;
//                    dto.AttachmentUrl = !string.IsNullOrWhiteSpace(item.AttachmentUrl) ? baseUrl + item.AttachmentUrl : null;
//                    dto.Title = item.Title;
//                    //dto.SignatureUrl = item.OriginatorSignatureUrl != null ? item.OriginatorSignatureUrl : null;
//                    //dto.FileName = item.FileType.Title;
//                    //dto.FileTypeId = item.FileType.Id;
//                    //dto.FileNumber = item.FileType.FileNumber;
//                    dto.CurrentDeskName = mailStatus;
//                    mailing.Add(dto);
//                    //MailingStatusDto = await GetMailingDeskStatus(m.Id)


//                }
//            }
          
           

//            return mailing;
//        }

//        public async Task<MailingStatusDto> GetMailingDeskStatus(long mailingId)
//        {
//            MailingStatusDto mailStatus = new MailingStatusDto();
//            var getMail = await _context.MAILING_DESK_CHAIN.Where(d => d.MailingId == mailingId && d.IsActive)
//                .Include(r => r.Role)
//                .Include(r => r.InstitutionDepartment)
//                .ThenInclude(f => f.Faculty)
//                .Select(f => new MailingStatusDto
//                {
//                    CurrentDeskName = f.RoleId == (int)UserRole.Dean ? f.Role.Name + " " + f.InstitutionDepartment.Faculty.Name :
//                        f.RoleId == (int)UserRole.HOD ? f.Role.Name + " " + f.InstitutionDepartment.Name :
//                         f.RoleId == (int)UserRole.DeputyRegistrar ? f.Role.Name + " " + f.InstitutionDepartment.Name :
//                         f.Role.Name,
//                    DeskId = f.Id
//                })
//                .OrderBy(f => f.DeskId).LastOrDefaultAsync();
//            return getMail;
//        }
//        public async Task<string> GetExternalDeskStatus(long mailingId)
//        {
//            MailingStatusDto mailStatus = new MailingStatusDto();
//            var getMail = await _context.MAILING_DESK_CHAIN.Where(d => d.MailingId == mailingId && d.IsActive)
//                .Include(r => r.Role)
//                .Include(r => r.InstitutionDepartment)
//                .ThenInclude(f => f.Faculty)
//                .Select(f => new MailingStatusDto
//                {
//                    CurrentDeskName = f.RoleId == (int)UserRole.Dean ? f.Role.Name + " " + f.InstitutionDepartment.Faculty.Name :
//                        f.RoleId == (int)UserRole.HOD ? f.Role.Name + " " + f.InstitutionDepartment.Name :
//                         f.RoleId == (int)UserRole.DeputyRegistrar ? f.Role.Name + " " + f.InstitutionDepartment.Name :
//                         f.Role.Name,
//                    DeskId = f.Id
//                })
//                .OrderBy(f => f.DeskId).LastOrDefaultAsync();
//            return getMail.CurrentDeskName;
//        }
//        public async Task<IEnumerable<GetMailDto>> GetMailInbox(long RoleId, long DeptId, long FacultyId, long staffId)
//        {
//            List<GetMailDto> mailing = new List<GetMailDto>();
//            try
//            {
                
//                    mailing = await _context.MAILING_DESK_CHAIN
//                    .Include(f => f.Role)
//                    .Include(d => d.InstitutionDepartment)
//                    .Include(d => d.Faculty)
//                    .Include(m => m.Mailing)
//                    .ThenInclude(m => m.Originator)
//                    .ThenInclude(m => m.Role)
//                    .Include(d => d.Mailing)
//                    .ThenInclude(x => x.Originator)
//                    .ThenInclude(s => s.Staff)
//                    .ThenInclude(d => d.Department)
//                    .ThenInclude(f => f.Faculty)
//                    .Include(m => m.Mailing)
//                    .ThenInclude(f => f.FileType)
//                    .Include(m => m.Mailing)
//                    .ThenInclude(m => m.Originator)
//                    .ThenInclude(s => s.Staff)
//                    .ThenInclude(p => p.Person)
//                   .Where(r => (r.RoleId == RoleId && r.InstitutionDepartmentId == null && r.FacultyId == null) || (r.RoleId == RoleId && r.InstitutionDepartmentId == DeptId) ||
//                   (r.RoleId == RoleId && r.FacultyId == FacultyId) || (r.StaffId == staffId))
//                   .Select(f => new GetMailDto
//                   {
//                       Body = f.Mailing.Body,
//                       Title = f.Mailing.Title,
//                       DateEntered = f.Mailing.DateEntered,
//                       IsAcknowledged = f.Mailing.IsAcknowledged,
//                       IsClosed = f.Mailing.IsClosed,
//                       IsRejected = f.Mailing.IsRejected,
//                       DeskIsActive = f.IsActive,
//                       DeskIsRead = f.IsRead,
//                       OriginatorInfo = f.Mailing.IsExternal ? f.Mailing.OriginatorRoleDepartment : (f.Mailing.Originator.RoleId == (int)UserRole.Dean ? f.Mailing.Originator.Role.Name + " " + f.Mailing.Originator.Staff.Department.Faculty.Name :
//                        f.Mailing.Originator.RoleId == (int)UserRole.HOD ? f.Mailing.Originator.Role.Name + " " + f.Mailing.Originator.Staff.Department.Name :
//                         f.Mailing.Originator.RoleId == (int)UserRole.DeputyRegistrar ? f.Role.Name + " " + f.Mailing.Originator.Staff.Department.Name :
//                         f.Mailing.Originator.Role.Name),
//                       AttachmentUrl = !string.IsNullOrWhiteSpace(f.Mailing.AttachmentUrl) ? baseUrl + f.Mailing.AttachmentUrl : null,
//                       DeskId = f.Id,
//                       MailingId = f.MailingId,
//                       IsCopied = f.IsCopied,
//                       IsExternal = f.Mailing.IsExternal,
//                       SignatureUrl = f.Mailing.OriginatorSignatureUrl,
//                       FileName = f.Mailing.FileType.Title,
//                       FileTypeId = f.Mailing.FileType.Id,
//                       FileNumber = f.Mailing.FileType.FileNumber,
//                       IsConfidential = f.Mailing.IsConfidential,
                       
//                       OriginatorName = f.Mailing.Originator.Staff.Person.Surname + " " + f.Mailing.Originator.Staff.Person.Firstname + " " + f.Mailing.Originator.Staff.Person.Othername

//                   })
//                   .OrderByDescending(d => d.MailingId)
//                   .ToListAsync();
//                    return mailing;

              


//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        public async Task<IEnumerable<GetMailDto>> SearchMailInbox(long RoleId, long DeptId, long FacultyId, long staffId, long fileTypeId, string searchParam, DateTime dateFrom, DateTime dateTo)
//        {
//            List<GetMailDto> mailing = new List<GetMailDto>();
//            try
//            {

//                mailing = await _context.MAILING_DESK_CHAIN
//                .Include(f => f.Role)
//                .Include(d => d.InstitutionDepartment)
//                .Include(d => d.Faculty)
//                .Include(m => m.Mailing)
//                .ThenInclude(m => m.Originator)
//                .ThenInclude(m => m.Role)
//                .Include(d => d.Mailing)
//                .ThenInclude(x => x.Originator)
//                .ThenInclude(s => s.Staff)
//                .ThenInclude(d => d.Department)
//                .ThenInclude(f => f.Faculty)
//                .Include(m => m.Mailing)
//                .ThenInclude(f => f.FileType)
//                .Include(m => m.Mailing)
//                .ThenInclude(m => m.Originator)
//                .ThenInclude(s => s.Staff)
//                .ThenInclude(p => p.Person)
//               .Where(r => ((r.RoleId == RoleId && r.InstitutionDepartmentId == null && r.FacultyId == null) || (r.RoleId == RoleId && r.InstitutionDepartmentId == DeptId) || (r.RoleId == RoleId && r.FacultyId == FacultyId) || (r.StaffId == staffId)) && (r.Mailing.Title.Contains(searchParam) || r.Mailing.FileTypeId == fileTypeId && (r.Mailing.DateEntered.Date >= dateFrom && r.Mailing.DateEntered.Date <= dateTo)))
//               .Select(f => new GetMailDto
//               {
//                   Body = f.Mailing.Body,
//                   Title = f.Mailing.Title,
//                   DateEntered = f.Mailing.DateEntered,
//                   IsAcknowledged = f.Mailing.IsAcknowledged,
//                   IsClosed = f.Mailing.IsClosed,
//                   IsRejected = f.Mailing.IsRejected,
//                   DeskIsActive = f.IsActive,
//                   DeskIsRead = f.IsRead,
//                   OriginatorInfo = f.Mailing.Originator.RoleId == (int)UserRole.Dean ? f.Mailing.Originator.Role.Name + " " + f.Mailing.Originator.Staff.Department.Faculty.Name :
//                    f.Mailing.Originator.RoleId == (int)UserRole.HOD ? f.Mailing.Originator.Role.Name + " " + f.Mailing.Originator.Staff.Department.Name :
//                     f.Mailing.Originator.RoleId == (int)UserRole.DeputyRegistrar ? f.Role.Name + " " + f.Mailing.Originator.Staff.Department.Name :
//                     f.Mailing.Originator.Role.Name,
//                   AttachmentUrl = !string.IsNullOrWhiteSpace(f.Mailing.AttachmentUrl) ? baseUrl + f.Mailing.AttachmentUrl : null,
//                   DeskId = f.Id,
//                   MailingId = f.MailingId,
//                   IsCopied = f.IsCopied,
//                   SignatureUrl = f.Mailing.OriginatorSignatureUrl,
//                   FileName = f.Mailing.FileType.Title,
//                   FileTypeId = f.Mailing.FileType.Id,
//                   FileNumber = f.Mailing.FileType.FileNumber,
//                   IsConfidential = f.Mailing.IsConfidential,
//                   OriginatorName = f.Mailing.Originator.Staff.Person.Surname + " " + f.Mailing.Originator.Staff.Person.Firstname + " " + f.Mailing.Originator.Staff.Person.Othername

//               })
//               .OrderByDescending(d => d.MailingId)
//               .ToListAsync();
//                return mailing;




//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        public async Task<IEnumerable<GetMailDto>> SearchSentMail(long userId, long fileTypeId, string searchParam, DateTime dateFrom, DateTime dateTo)
//        {
//            List<GetMailDto> mailing = new List<GetMailDto>();
//            mailing = await _context.MAILING.Where(f => f.OriginatorId == userId && ((f.DateEntered.Date >= dateFrom && f.DateEntered.Date <= dateTo) || (f.Title.Contains(searchParam) || f.Body.Contains(searchParam)) || f.FileTypeId == fileTypeId))
//                .Include(f => f.FileType)
//                .Select(m => new GetMailDto
//                {
//                    MailingId = m.Id,
//                    Body = m.Body,
//                    DateEntered = m.DateEntered,
//                    IsAcknowledged = m.IsAcknowledged,
//                    IsClosed = m.IsClosed,
//                    IsRejected = m.IsRejected,
//                    OriginatorInfo = m.OriginatorRoleDepartment,
//                    AttachmentUrl = !string.IsNullOrWhiteSpace(m.AttachmentUrl) ? baseUrl + m.AttachmentUrl : null,
//                    Title = m.Title,
//                    SignatureUrl = m.OriginatorSignatureUrl,
//                    FileName = m.FileType.Title,
//                    FileTypeId = m.FileType.Id,
//                    FileNumber = m.FileType.FileNumber

//                })
//                .OrderByDescending(d => d.MailingId)
//                .ToListAsync();
//            return mailing;
//        }
//        public async Task<IEnumerable<GetMailDto>> GetConfidentialMail()
//        {
//            List<GetMailDto> mailing = new List<GetMailDto>();
//            try
//            {

//                mailing = await _context.MAILING_DESK_CHAIN
//                .Include(f => f.Role)
//                .Include(d => d.InstitutionDepartment)
//                .Include(d => d.Faculty)
//                .Include(m => m.Mailing)
//                .ThenInclude(m => m.Originator)
//                .ThenInclude(m => m.Role)
//                .Include(d => d.Mailing)
//                .ThenInclude(x => x.Originator)
//                .ThenInclude(s => s.Staff)
//                .ThenInclude(d => d.Department)
//                .ThenInclude(f => f.Faculty)
//                .Include(m => m.Mailing)
//                .ThenInclude(f => f.FileType)
//                .Include(m => m.Mailing)
//                .ThenInclude(m => m.Originator)
//                .ThenInclude(s => s.Staff)
//                .ThenInclude(p => p.Person)
//               .Where(r => r.Mailing.IsConfidential)
//               .Select(f => new GetMailDto
//               {
//                   Body = f.Mailing.Body,
//                   Title = f.Mailing.Title,
//                   DateEntered = f.Mailing.DateEntered,
//                   IsAcknowledged = f.Mailing.IsAcknowledged,
//                   IsClosed = f.Mailing.IsClosed,
//                   IsRejected = f.Mailing.IsRejected,
//                   DeskIsActive = f.IsActive,
//                   DeskIsRead = f.IsRead,
//                   OriginatorInfo = f.Mailing.Originator.RoleId == (int)UserRole.Dean ? f.Mailing.Originator.Role.Name + " " + f.Mailing.Originator.Staff.Department.Faculty.Name :
//                    f.Mailing.Originator.RoleId == (int)UserRole.HOD ? f.Mailing.Originator.Role.Name + " " + f.Mailing.Originator.Staff.Department.Name :
//                     f.Mailing.Originator.RoleId == (int)UserRole.DeputyRegistrar ? f.Role.Name + " " + f.Mailing.Originator.Staff.Department.Name :
//                     f.Mailing.Originator.Role.Name,
//                   AttachmentUrl = !string.IsNullOrWhiteSpace(f.Mailing.AttachmentUrl) ? baseUrl + f.Mailing.AttachmentUrl : null,
//                   DeskId = f.Id,
//                   MailingId = f.MailingId,
//                   IsCopied = f.IsCopied,
//                   SignatureUrl = f.Mailing.OriginatorSignatureUrl,
//                   FileName = f.Mailing.FileType.Title,
//                   FileTypeId = f.Mailing.FileType.Id,
//                   FileNumber = f.Mailing.FileType.FileNumber,
//                   IsConfidential = f.Mailing.IsConfidential,
//                   OriginatorName = f.Mailing.Originator.Staff.Person.Surname + " " + f.Mailing.Originator.Staff.Person.Firstname + " " + f.Mailing.Originator.Staff.Person.Othername


//               })
//               .OrderByDescending(d => d.MailingId)
//               .ToListAsync();
//                return mailing;




//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }


//        public async Task<long> GetNewMailCount(long RoleId, long DeptId, long FacultyId)
//        {
//            List<GetMailDto> mailing = new List<GetMailDto>();
//            try
//            {

//                mailing = await _context.MAILING_DESK_CHAIN
//                .Include(f => f.Role)
//                .Include(d => d.InstitutionDepartment)
//                .Include(d => d.Faculty)
//                .Include(m => m.Mailing)
//                .ThenInclude(m => m.Originator)
//                .ThenInclude(m => m.Role)
//                .Include(d => d.Mailing)
//                .ThenInclude(x => x.Originator)
//                .ThenInclude(s => s.Staff)
//                .ThenInclude(d => d.Department)
//                .ThenInclude(f => f.Faculty)
//               .Where(r => ((r.RoleId == RoleId && r.InstitutionDepartmentId == null && r.FacultyId == null) || (r.RoleId == RoleId && r.InstitutionDepartmentId == DeptId) || (r.RoleId == RoleId && r.FacultyId == FacultyId)) && (!r.IsRead))
//               .Select(f => new GetMailDto
//               {
//                   Body = f.Mailing.Body,
//                   Title = f.Mailing.Title,
//                   DateEntered = f.Mailing.DateEntered,
//                   IsAcknowledged = f.Mailing.IsAcknowledged,
//                   IsClosed = f.Mailing.IsClosed,
//                   IsRejected = f.Mailing.IsRejected,
//                   DeskIsActive = f.IsActive,
//                   DeskIsRead = f.IsRead,
//                   OriginatorInfo = f.Mailing.Originator.RoleId == (int)UserRole.Dean ? f.Mailing.Originator.Role.Name + " " + f.Mailing.Originator.Staff.Department.Faculty.Name :
//                    f.Mailing.Originator.RoleId == (int)UserRole.HOD ? f.Mailing.Originator.Role.Name + " " + f.Mailing.Originator.Staff.Department.Name :
//                     f.Mailing.Originator.RoleId == (int)UserRole.DeputyRegistrar ? f.Role.Name + " " + f.Mailing.Originator.Staff.Department.Name :
//                     f.Mailing.Originator.Role.Name,
//                   AttachmentUrl = f.Mailing.AttachmentUrl,
//                   DeskId = f.Id,
//                   MailingId = f.MailingId
//               })
//               .ToListAsync();
//                return mailing.Count();




//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public async Task<int> PostMailingAction(MailingActionDto actionDto)
//        {
//            try
//            {
//                Mailing mailing = new Mailing();
//                MailingDeskChain mailingDesk = new MailingDeskChain();
//                MailingComments mailingComments = new MailingComments();
//                mailing = await _context.MAILING.Where(s => s.Id == actionDto.MailingId).FirstOrDefaultAsync();
//                MailingDeskChain Active_Desk = await _context.MAILING_DESK_CHAIN.Where(d => d.MailingId == actionDto.MailingId).OrderBy(f => f.Id).LastOrDefaultAsync();

//                //Checks to ascertain if mail is still intended for transitioning
//                if (actionDto.IsTransition && mailing != null)
//                {
                    
                      
//                    if (Active_Desk != null)
//                    {
                       
//                            //Toggle Previous Desk to inactive
//                            Active_Desk.IsActive = false;
//                            Active_Desk.IsActed = true;
//                            _context.Update(Active_Desk);
//                            var isSaved = await _context.SaveChangesAsync();

//                            if (isSaved == _saved)
//                            {
//                                //Transitioned to a new desk -> Create next desk record
//                            mailingDesk.RoleId = actionDto.ToRoleId;
//                            if(actionDto.ToDepartmentId > 0)
//                            {
//                                mailingDesk.InstitutionDepartmentId = actionDto.ToDepartmentId;

//                            }
//                            if (actionDto.ToFacultyId > 0)
//                            {
//                                mailingDesk.FacultyId = actionDto.ToFacultyId;

//                            }
//                            mailingDesk.IsActive = true;
//                            mailingDesk.MailingId = mailing.Id;
//                            _context.Add(mailingDesk);
//                            //await _context.AddAsync(mailingDesk);

//                            var isSaved2 = await _context.SaveChangesAsync();

//                            //Commit comments if there are any
//                            if (isSaved2 == _saved && actionDto.Comments != null)
//                            {
//                                mailingComments.MailingDeskChainId = Active_Desk.Id;
//                                mailingComments.Comments = actionDto.Comments;
//                                mailingComments.Active = true;
//                                mailingComments.DateEntered = DateTime.Now;
//                            _context.Add(mailingComments);
//                            //await _context.AddAsync(mailingComments);
//                              var isSaved3 = await _context.SaveChangesAsync();

//                            //Execute mailing action if detected
//                            if(isSaved3 == _saved && actionDto.ActionType > 0)
//                            {
//                            var get_user = await _context.USER
//                                            .Where(f => f.Id == actionDto.UserId)
//                                            .Include(r => r.Role)
//                                            .Include(s => s.Staff)
//                                            .ThenInclude(d => d.Department)
//                                            .ThenInclude(f => f.Faculty)
//                                            .FirstOrDefaultAsync();
//                                    if (actionDto.ActionType == (int)MailingActionType.Acknowledged)
//                                    {                                        
//                                       if(get_user != null)
//                                        {
//                                            mailing.IsAcknowledged = true;
//                                            mailing.IsClosed = true;
//                                            mailing.AcknowledgedUserId = get_user.Id;
//                                            mailing.AcknowledgedRoleOffice = get_user.RoleId == (int)UserRole.Dean ? get_user.Role.Name + " " +
//                                    get_user.Staff.Department.Faculty.Name : get_user.RoleId == (int)UserRole.HOD
//                                    ? get_user.Role.Name + " " + get_user.Staff.Department.Name : get_user.RoleId == (int)UserRole.DeputyRegistrar
//                                    ? get_user.Role.Name + " " + get_user.Staff.Department.Name : get_user.Role.Name;
//                                    //_context.Update(mailing);
//                                    _context.Update(mailing);
//                                            await _context.SaveChangesAsync();
//                                            return StatusCodes.Status200OK;

//                                         }
//                                    }
                               
//                                    return StatusCodes.Status200OK;
//                                }
//                            }
//                       }

                        
//                    }
//                    //await transaction.CommitAsync();
//                    await _context.SaveChangesAsync();
//                        return StatusCodes.Status200OK;
//                    //});
//                }
//                else
//                {
//                    var get_user = await _context.USER
//                                            .Where(f => f.Id == actionDto.UserId)
//                                            .Include(r => r.Role)
//                                            .Include(s => s.Staff)
//                                            .ThenInclude(d => d.Department)
//                                            .ThenInclude(f => f.Faculty)
//                                            .FirstOrDefaultAsync();
//                    if (actionDto.ActionType == (int)MailingActionType.Acknowledged)
//                    {
//                        if (get_user != null)
//                        {
//                            mailing.IsAcknowledged = true;
//                            mailing.IsClosed = true;
//                            mailing.AcknowledgedUserId = get_user.Id;
//                            mailing.AcknowledgedRoleOffice = get_user.RoleId == (int)UserRole.Dean ? get_user.Role.Name + " " +
//                                    get_user.Staff.Department.Faculty.Name : get_user.RoleId == (int)UserRole.HOD
//                                    ? get_user.Role.Name + " " + get_user.Staff.Department.Name : get_user.RoleId == (int)UserRole.DeputyRegistrar
//                                    ? get_user.Role.Name + " " + get_user.Staff.Department.Name : get_user.Role.Name;
//                            _context.Update(mailing);


//                            mailingComments.MailingDeskChainId = Active_Desk.Id;
//                            mailingComments.Comments = actionDto.Comments;
//                            mailingComments.Active = true;
//                            mailingComments.DateEntered = DateTime.Now;
//                            _context.Add(mailingComments);
//                            //await _context.AddAsync(mailingComments);
//                            //var isSaved3 = await _context.SaveChangesAsync();
//                            await _context.SaveChangesAsync();
//                            return StatusCodes.Status200OK;

//                        }
//                    }
//                    else if (actionDto.ActionType == (int)MailingActionType.Rejected)
//                    {
//                        if (get_user != null)
//                        {
//                            mailing.IsRejected = true;
//                            mailing.IsClosed = true;
//                            mailing.AcknowledgedUserId = get_user.Id;
//                            mailing.AcknowledgedRoleOffice = get_user.RoleId == (int)UserRole.Dean ? get_user.Role.Name + " " +
//                                    get_user.Staff.Department.Faculty.Name : get_user.RoleId == (int)UserRole.HOD
//                                    ? get_user.Role.Name + " " + get_user.Staff.Department.Name : get_user.RoleId == (int)UserRole.DeputyRegistrar
//                                    ? get_user.Role.Name + " " + get_user.Staff.Department.Name : get_user.Role.Name;
//                            _context.Update(mailing);


//                            mailingComments.MailingDeskChainId = Active_Desk.Id;
//                            mailingComments.Comments = actionDto.Comments;
//                            mailingComments.Active = true;
//                            mailingComments.DateEntered = DateTime.Now;
//                            _context.Add(mailingComments);

//                            await _context.SaveChangesAsync();
//                            return StatusCodes.Status200OK;

//                        }
//                    }
//                }
//                return StatusCodes.Status500InternalServerError;
//            }
//            catch(Exception ex)
//            {
//                throw ex;
//            }
//        }

//       public async Task<int> ActivateRead(long deskChainId)
//        {
//            MailingDeskChain deskChain = await _context.MAILING_DESK_CHAIN.Where(f => f.Id == deskChainId).FirstOrDefaultAsync();
//            if(deskChain != null)
//            {
//                deskChain.IsRead = true;
//                _context.Update(deskChain);
//                await _context.SaveChangesAsync();
//                return StatusCodes.Status200OK;
//            }
//            return StatusCodes.Status500InternalServerError;
//        }

//        //public async Task<string> GetDeskAlias(long RoleId,)
//        //{
//        //    var get_last = await _context.MAILING.Where(a => a.Id > 0).OrderBy(i => i.Id).LastOrDefaultAsync();
//        //}

//        public async Task<IEnumerable<TrailingCommentsDto>> GetMailTrailingCommentsByMailId(long mailingId)
//        {
//            try
//            {
//                var commentList = await _context.MAILING_COMMENTS.Where(m => m.MailingDeskChain.MailingId == mailingId)
//                    .Include(f => f.MailingDeskChain)
//                    .ThenInclude(d => d.InstitutionDepartment)
//                    .Include(f => f.MailingDeskChain)
//                    .ThenInclude(d => d.Faculty)
//                    .Include(f => f.MailingDeskChain)
//                    .ThenInclude(d => d.Role)
//                    .Select(f => new TrailingCommentsDto { 
//                        Comments = f.Comments,
//                        UserInfo = f.MailingDeskChain.RoleId == (int)UserRole.Dean ? f.MailingDeskChain.Role.Name + " " + f.MailingDeskChain.Faculty.Name :
//                        f.MailingDeskChain.RoleId == (int)UserRole.HOD ? f.MailingDeskChain.Role.Name + " " + f.MailingDeskChain.InstitutionDepartment.Name :
//                         f.MailingDeskChain.RoleId == (int)UserRole.DeputyRegistrar ? f.MailingDeskChain.Role.Name + " " + f.MailingDeskChain.InstitutionDepartment.Name :
//                         f.MailingDeskChain.Role.Name,
//                        Id = f.Id,
//                        DateEntered = f.DateEntered

//                    })
//                    .ToListAsync();
//                return commentList;
//            }
//            catch(Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public async Task<IEnumerable<FilterByRoleDto>> FilterByRole(long roleId)
//        {
//            try
//            {
//                //List<User> userList = new List<User>();
//                if(roleId == (int)UserRole.Dean)
//                {
//                    var userList = await _context.USER.Where(u => u.RoleId == roleId)
//                        .Include(s => s.Staff)
//                        .ThenInclude(s => s.Department)
//                        .ThenInclude(s => s.Faculty)
//                    .Select(f => new FilterByRoleDto
//                    {
//                        Name = f.Staff.Department.Faculty.Name,
//                        Id = f.Staff.Department.Faculty.Id
//                    })
//                    .Distinct()
//                    .ToListAsync();
//                    return userList;
//                }
//                else
//                {
//                    var userList = await _context.USER.Where(u => u.RoleId == roleId)
//                         .Include(s => s.Staff)
//                        .ThenInclude(s => s.Department)
//                    .Select(f => new FilterByRoleDto
//                    {
//                        Name = f.Staff.Department.Name,
//                        Id = f.Staff.Department.Id
//                    })
//                    .Distinct()
//                    .ToListAsync();
//                    return userList;
//                }
                
                
//            }
//            catch(Exception ex)
//            {
//                throw ex;
//            }
//        }
//        public async Task<IEnumerable<GetMailingStaffDto>> GetSpecificMailingStaff(long departmentId)
//        {
//            return await _context.STAFF.Where(d => d.DepartmentId == departmentId)
//                .Include(p => p.Person)
//                .Select(s => new GetMailingStaffDto
//                {
//                    StaffDetail = s.StaffNumber != null ? s.StaffNumber  + ": " + s.Person.Surname + " " + s.Person.Firstname + " " + s.Person.Othername :
//                    "..." + ": " + s.Person.Surname + " " + s.Person.Firstname + " " + s.Person.Othername,
//                    StaffId = s.Id
//                })
//                .ToListAsync();
//        }

//        public async Task<IEnumerable<GetMailDto>> GetMailActionArchive(long RoleId, long DeptId, long FacultyId, long staffId)
//        {
//            List<GetMailDto> mailing = new List<GetMailDto>();
//            try
//            {

//                mailing = await _context.MAILING_DESK_CHAIN
//                .Include(f => f.Role)
//                .Include(d => d.InstitutionDepartment)
//                .Include(d => d.Faculty)
//                .Include(m => m.Mailing)
//                .ThenInclude(m => m.Originator)
//                .ThenInclude(m => m.Role)
//                .Include(d => d.Mailing)
//                .ThenInclude(x => x.Originator)
//                .ThenInclude(s => s.Staff)
//                .ThenInclude(d => d.Department)
//                .ThenInclude(f => f.Faculty)
//               .Where(r => ((r.RoleId == RoleId && r.InstitutionDepartmentId == null && r.FacultyId == null) || (r.RoleId == RoleId && r.InstitutionDepartmentId == DeptId) || (r.RoleId == RoleId && r.FacultyId == FacultyId) || (r.StaffId == staffId)) && (r.IsActed))
//               .Select(f => new GetMailDto
//               {
//                   Body = f.Mailing.Body,
//                   Title = f.Mailing.Title,
//                   DateEntered = f.Mailing.DateEntered,
//                   IsAcknowledged = f.Mailing.IsAcknowledged,
//                   IsClosed = f.Mailing.IsClosed,
//                   IsRejected = f.Mailing.IsRejected,
//                   DeskIsActive = f.IsActive,
//                   DeskIsRead = f.IsRead,
//                   OriginatorInfo = f.Mailing.Originator.RoleId == (int)UserRole.Dean ? f.Mailing.Originator.Role.Name + " " + f.Mailing.Originator.Staff.Department.Faculty.Name :
//                    f.Mailing.Originator.RoleId == (int)UserRole.HOD ? f.Mailing.Originator.Role.Name + " " + f.Mailing.Originator.Staff.Department.Name :
//                     f.Mailing.Originator.RoleId == (int)UserRole.DeputyRegistrar ? f.Role.Name + " " + f.Mailing.Originator.Staff.Department.Name :
//                     f.Mailing.Originator.Role.Name,
//                   AttachmentUrl = f.Mailing.AttachmentUrl,
//                   DeskId = f.Id,
//                   MailingId = f.MailingId
//               })
//               .ToListAsync();
//                return mailing;




//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public async Task<IEnumerable<GetMailingStaffDto>> SearchRecipient(string search)
//        {
//            var list = await _context.STAFF
//                .Include(d => d.Person)
//                .Include(s => s.Department)
//                .Where(s => s.StaffNumber.Contains(search) || s.Person.Firstname.Contains(search) 
//            || s.Person.Surname.Contains(search) || s.Person.Othername.Contains(search) || s.Department.Name.Contains(search))
//                .Select(s => new GetMailingStaffDto {
//                    StaffDetail = s.StaffNumber != null ? s.StaffNumber + ": " + s.Person.Surname + " " + s.Person.Firstname + " " + s.Person.Othername :
//                    "..." + ": " + s.Person.Surname + " " + s.Person.Firstname + " " + s.Person.Othername,
//                    StaffId = s.Id
//                })
//                .ToListAsync();

//            return list;
//        }

//        public async Task<long> ArchiveMail(ArchiveMailDto dto)
//        {
//            try
//            {
//                var isArchived = await _context.MAIL_ARCHIVE.Where(f => f.MailingId == dto.MailingId).FirstOrDefaultAsync();
//                if(isArchived != null)
//                {
//                    return StatusCodes.Status208AlreadyReported;
//                }
//                MailArchive mailArchive = new MailArchive()
//                {
//                    MailingId = dto.MailingId,
//                    MailArchiveFileTypeId = dto.FileTypeId,
//                    DateArchived = DateTime.Now,
//                    ArchivedById = dto.UserId,
//                    Active = true
//                };
//                _context.Add(mailArchive);
//                await _context.SaveChangesAsync();
//                return StatusCodes.Status200OK;
//            }
//            catch(Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public async Task<IEnumerable<MailArchiveFileType>> GetMailingFileType()
//        {
//            return await _context.MAIL_ARCHIVE_FILE_TYPE.Where(s => s.Active).ToListAsync();
//        }

//        public async Task<IEnumerable<GetMailDto>> GetMailArchives(long fileTypeId, string searchParam, DateTime dateFrom, DateTime dateTo)
//        {
//            List<GetMailDto> mailing = new List<GetMailDto>();
//            try
//            {

//                mailing = await _context.MAIL_ARCHIVE
//                .Include(m => m.Mailing)
//                .ThenInclude(m => m.Originator)
//                .ThenInclude(m => m.Role)
//                .Include(d => d.Mailing)
//                .ThenInclude(x => x.Originator)
//                .ThenInclude(r => r.Role)
//                .Include(m => m.Mailing)
//                .ThenInclude(o => o.Originator)
//                .ThenInclude(s => s.Staff)
//                .ThenInclude(d => d.Department)
//                .ThenInclude(f => f.Faculty)
//                .Include(m => m.Mailing)
//                .ThenInclude(c => c.FileType)
//               .Where(r => (r.MailArchiveFileTypeId == fileTypeId) || (r.DateArchived.Date >= dateFrom && r.DateArchived.Date <= dateTo) || (r.Mailing.Title.Contains(searchParam)))
//               .Select(f => new GetMailDto
//               {
//                   Body = f.Mailing.Body,
//                   Title = f.Mailing.Title,
//                   DateEntered = f.Mailing.DateEntered,
//                   IsAcknowledged = f.Mailing.IsAcknowledged,
//                   IsClosed = f.Mailing.IsClosed,
//                   IsRejected = f.Mailing.IsRejected,
//                   DeskIsActive = f.Active,
//                   OriginatorInfo = f.Mailing.Originator.RoleId == (int)UserRole.Dean ? f.Mailing.Originator.Role.Name + " " + f.Mailing.Originator.Staff.Department.Faculty.Name :
//                    f.Mailing.Originator.RoleId == (int)UserRole.HOD ? f.Mailing.Originator.Role.Name + " " + f.Mailing.Originator.Staff.Department.Name :
//                     f.Mailing.Originator.RoleId == (int)UserRole.DeputyRegistrar ? f.Mailing.Originator.Role.Name + " " + f.Mailing.Originator.Staff.Department.Name :
//                     f.Mailing.Originator.Role.Name,
//                   AttachmentUrl = !string.IsNullOrWhiteSpace(f.Mailing.AttachmentUrl) ? baseUrl + f.Mailing.AttachmentUrl : null,
//                   DeskId = f.Id,
//                   MailingId = f.MailingId,
//                   SignatureUrl = f.Mailing.OriginatorSignatureUrl,
//                   FileName = f.Mailing.FileType.Title,
//                   FileTypeId = f.Mailing.FileType.Id,
//                   FileNumber = f.Mailing.FileType.FileNumber

//               })
//               .OrderByDescending(d => d.MailingId)
//               .ToListAsync();
//                return mailing;




//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        public async Task<IEnumerable<GetMailDto>> GetAllMailArchives()
//        {
//            List<GetMailDto> mailing = new List<GetMailDto>();
//            try
//            {

//                mailing = await _context.MAIL_ARCHIVE
//                .Include(m => m.Mailing)
//                .ThenInclude(m => m.Originator)
//                .ThenInclude(m => m.Role)
//                .Include(d => d.Mailing)
//                .ThenInclude(x => x.Originator)
//                .ThenInclude(r => r.Role)
//                .Include(m => m.Mailing)
//                .ThenInclude(o => o.Originator)
//                .ThenInclude(s => s.Staff)
//                .ThenInclude(d => d.Department)
//                .ThenInclude(f => f.Faculty)
//                .Include(m => m.Mailing)
//                .ThenInclude(c => c.FileType)
//               .Where(r => r.Id > 0)
//               .Select(f => new GetMailDto
//               {
//                   Body = f.Mailing.Body,
//                   Title = f.Mailing.Title,
//                   DateEntered = f.Mailing.DateEntered,
//                   IsAcknowledged = f.Mailing.IsAcknowledged,
//                   IsClosed = f.Mailing.IsClosed,
//                   IsRejected = f.Mailing.IsRejected,
//                   DeskIsActive = f.Active,
//                   OriginatorInfo = f.Mailing.Originator.RoleId == (int)UserRole.Dean ? f.Mailing.Originator.Role.Name + " " + f.Mailing.Originator.Staff.Department.Faculty.Name :
//                    f.Mailing.Originator.RoleId == (int)UserRole.HOD ? f.Mailing.Originator.Role.Name + " " + f.Mailing.Originator.Staff.Department.Name :
//                     f.Mailing.Originator.RoleId == (int)UserRole.DeputyRegistrar ? f.Mailing.Originator.Role.Name + " " + f.Mailing.Originator.Staff.Department.Name :
//                     f.Mailing.Originator.Role.Name,
//                   AttachmentUrl = !string.IsNullOrWhiteSpace(f.Mailing.AttachmentUrl) ? baseUrl + f.Mailing.AttachmentUrl : null,
//                   DeskId = f.Id,
//                   MailingId = f.MailingId,
//                   SignatureUrl = f.Mailing.OriginatorSignatureUrl,
//                   FileName = f.Mailing.FileType.Title,
//                   FileTypeId = f.Mailing.FileType.Id,
//                   FileNumber = f.Mailing.FileType.FileNumber

//               })
//               .OrderByDescending(d => d.MailingId)
//               .ToListAsync();
//                return mailing;




//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        public static byte[] GetTransparentArrayFromFileWithDelete(string pathToFile)
//        {
//            byte[] newImage = new byte[0]; 
//            var error = string.Empty;
//            using (Bitmap bmp = new Bitmap(pathToFile))
//            {
//                Color pixel = bmp.GetPixel(0, 0);
//                if (pixel.A != 0)
//                {
//                    Color backColor = bmp.GetPixel(0, 0);
//                    Color backColorGray = Color.Gray;
//                    Color backColorGrayLight = Color.LightGray;
//                    Color backColorWhiteSmoke = Color.WhiteSmoke;
//                    Color backColorWhite = Color.White;
//                    Color backColorWheat = Color.Wheat;

//                    //Make backColor transparent for myBitmap.
//                   bmp.MakeTransparent(Color.Transparent);
//                    bmp.MakeTransparent(backColor);

//                    bmp.MakeTransparent(backColorGray);
//                    bmp.MakeTransparent(backColorGrayLight);
//                    bmp.MakeTransparent(backColorWhiteSmoke);

//                    ImageConverter converter = new ImageConverter();
//                    newImage = (byte[])converter.ConvertTo(bmp, typeof(byte[]));
//                    bmp.Dispose();
//                }
//                else
//                {
//                    FileStream fs = new FileStream(pathToFile, FileMode.OpenOrCreate, FileAccess.Read);
//                    newImage = new byte[fs.Length];
//                    fs.Read(newImage, 0, System.Convert.ToInt32(fs.Length));
//                    fs.Close();
//                }
//            }
//            try
//            {
//                File.Delete(pathToFile);
//            }
//            catch
//            {
//            }
//            return newImage;
//        }
      
        
//        //public async Task<byte> Base64StringToBitmap(string base64String)
//        //{
//        //    Image myImg;
//        //    Bitmap myBitmap;
//        //    try
//        //    {
//        //        myImg = cls_convertImagesByte.GetImageFromByte(newImg);
//        //        myBitmap = new Bitmap(myImg); 

              
//        //        Color backColor = myBitmap.GetPixel(0, 0); 
//        //        Color backColorGray = Color.Gray;
//        //        Color backColorGrayLight = Color.LightGray;
//        //        Color backColorWhiteSmoke = Color.WhiteSmoke;
//        //        Color backColorWhite = Color.White;
//        //        Color backColorWheat = Color.Wheat;

              
//        //        myBitmap.MakeTransparent(backColor);
               
//        //        myBitmap.MakeTransparent(backColorGray);
//        //        myBitmap.MakeTransparent(backColorGrayLight);
//        //        myBitmap.MakeTransparent(backColorWhiteSmoke);

       
//        //        e.Graphics.DrawImage(myBitmap, 0, 0, pictureBox1.Width, pictureBox1.Height);
//        //    }
//        //    catch
//        //    {
//        //        try { pictureBox1.Image = cls_convertImagesByte.GetImageFromByte(newImg); }
//        //        catch { } 
//        //    }
      
//        //}


//        class cls_convertImagesByte
//        {

//            public static Image GetImageFromByte(byte[] byteArrayIn)
//            {
//                MemoryStream ms = new MemoryStream(byteArrayIn);
//                Image returnImage = Image.FromStream(ms);
//                return returnImage;
//            }

//            public static byte[] GetByteArrayFromImage(System.Drawing.Image imageIn)
//            {
//                MemoryStream ms = new MemoryStream();
//                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
//                return ms.ToArray();
//            }
//        }
//        public Task<string> SignatureToBase64(string signatureUrl)
//        {
          

//            StringBuilder _sb = new StringBuilder();
//            Byte[] _byte = GetImg(signatureUrl);
//            _sb.Append(Convert.ToBase64String(_byte, 0, _byte.Length));
//            var bi = Task.FromResult(string.Format(@"data:image/jpg;base64, {0}", _sb.ToString()));
//            //var convToImg = Base64StringToBitmap(_sb.ToString());
//            return bi;

//        }


//        private byte[] GetImg(string url)
//        {
//            Stream stream = null;
//            byte[] buf;
//            try
//            {
//                WebProxy myProxy = new WebProxy();
//                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
//                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
//                //get the response stream
//                stream = response.GetResponseStream();

//                using (BinaryReader br = new BinaryReader(stream))
//                {
//                    int len = (int)(response.ContentLength);
//                    buf = br.ReadBytes(len);
//                    br.Close();
//                }
//                stream.Close();
//                response.Close();
//            }
//            catch (Exception exp)
//            {
//                buf = null;
//            }
//            return (buf);
//        }

//    }


//}
