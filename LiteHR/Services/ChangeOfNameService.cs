using LiteHR.Dtos;
using LiteHR.Infrastructure;
using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Services
{
    public class ChangeOfNameService : IChangeOfNameService
    {
        public readonly HRContext _context;
        private readonly IFileUpload _fileupload;
        private readonly string baseUrl;
        private readonly IConfiguration _configuration;

        public ChangeOfNameService(HRContext context, IFileUpload fileupload, IConfiguration configuration)
        {
            _context = context;
            _fileupload = fileupload;
            _configuration = configuration;

            baseUrl = _configuration.GetValue<string>("Url:root");


        }


        public async Task<int> MakeChangeOfNameReuest(ChangeOfNameDto changeOfNameDto, string filePath, string directory)
        {
            //Checks for existing unprocessed request
            var checkRequest = await _context.CHANGE_OF_NAME.Where(s => s.StaffId == changeOfNameDto.StaffId && s.IsClosed == false).FirstOrDefaultAsync();

            //If Request is still being processed, throw Status 203
            if(checkRequest != null)
            {
                return StatusCodes.Status203NonAuthoritative;
            }

            var saveDocLink = string.Empty;
            if (changeOfNameDto.Attachment != null)
            {
                var docNamePrefix = "Document" + "_" + changeOfNameDto.StaffId + "_" + DateTime.Now.Millisecond;
                saveDocLink = await _fileupload.UploadDocumentGeneral(changeOfNameDto.Attachment, filePath, directory, docNamePrefix);
            }

            ChangeOfName changeOfName = new ChangeOfName()
            {
                RequestedFirstname = changeOfNameDto.Firstname,
                RequestedSurname = changeOfNameDto.Surname,
                RequestedOthername = changeOfNameDto.Othername,
                StaffId = changeOfNameDto.StaffId,
                Comments = changeOfNameDto.Comments,
                Attachment = saveDocLink,
                IsApproved = false,
                DateOfRequest = DateTime.Now,
                IsClosed = false

            };

            _context.Add(changeOfName);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
        }

        public async Task<int> ApproveChangeOfNameRequest(long staffId, bool requestAction)
        {
            var doubleCheck = await _context.CHANGE_OF_NAME.Where(n => n.StaffId == staffId && n.IsClosed == true).FirstOrDefaultAsync();
            if (doubleCheck != null)
                    return StatusCodes.Status203NonAuthoritative;
            

            var newName = await _context.CHANGE_OF_NAME.Where(n => n.StaffId == staffId && n.IsClosed == false).FirstOrDefaultAsync();
            var updateName = await _context.STAFF.Where(s => s.Id == staffId)
                .Include(p => p.Person)
                .FirstOrDefaultAsync();

            if (!requestAction)
            {
                newName.IsApproved = false;
                newName.IsClosed = true;
                _context.Update(newName);
                await _context.SaveChangesAsync();
                return StatusCodes.Status200OK;
            }

          

            updateName.Person.Firstname = newName.RequestedFirstname;
            updateName.Person.Surname = newName.RequestedSurname;
            updateName.Person.Othername = newName.RequestedOthername;
            _context.Update(updateName);
            newName.IsApproved = true;
            newName.IsClosed = true;
            _context.Update(newName);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
       
        }

        public async Task<IEnumerable<ChangeofNameListDto>> AdminGetChangeOfNameRequestList()
        {
            return await _context.CHANGE_OF_NAME
                .Include(s => s.Staff)
                .Select(c => new ChangeofNameListDto
                {
                    Firstname = c.RequestedFirstname,
                    Surname = c.RequestedSurname,
                    Othername = c.RequestedOthername,
                    Comments = c.Comments,
                    IsApproved = c.IsApproved,
                    IsClosed = c.IsClosed,
                    Attachment = baseUrl + c.Attachment,
                    DateOfRequest = c.DateOfRequest,
                    StaffId = c.StaffId,
                    StaffNumber = c.Staff.GeneratedStaffNumber

                }).ToListAsync();
        }

        public async Task<IEnumerable<ChangeofNameListDto>> GetRequestListByStaff(long staffId)
        {
            return await _context.CHANGE_OF_NAME.Where(s => s.StaffId == staffId)
                .Select(c => new ChangeofNameListDto
                {
                    Firstname = c.RequestedFirstname,
                    Surname = c.RequestedSurname,
                    Othername = c.RequestedOthername,
                    Comments = c.Comments,
                    IsApproved = c.IsApproved,
                    IsClosed = c.IsClosed,
                    Attachment = baseUrl + c.Attachment,
                    DateOfRequest = c.DateOfRequest,
                    StaffId = c.StaffId

                }).ToListAsync();
        }
    }
 }
