using LiteHR.Dtos;
using LiteHR.Helpers;
using LiteHR.Infrastructure;
using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Services
{
    public class StaffDocumentService : IStaffDocumentService
    {
        private readonly HRContext _context;
        private readonly IFileUpload _fileupload;
        private readonly string baseUrl;
        private readonly IConfiguration _configuration;




        public StaffDocumentService(HRContext context, IFileUpload fileUpload, IConfiguration configuration)
        {
            _context = context;
            _fileupload = fileUpload;
            _configuration = configuration;
            baseUrl = _configuration.GetValue<string>("Url:root");

        }

        public async Task<int> StaffDocumentUpload(StaffDocumentUploadDto staffDocumentUploadDto, string filePath, string directory)
        {
            StaffDocument staffDocument = new StaffDocument();
            if(staffDocumentUploadDto == null)
            {
                throw new NullReferenceException("Fields were left empty");
                
            }
            var staff = await _context.STAFF.Where(s => s.PersonId == staffDocumentUploadDto.PersonId).FirstOrDefaultAsync();

            var documentAlreadyUploaded = await _context.STAFF_DOCUMENT.Where(w => w.PersonId == staffDocumentUploadDto.PersonId && w.DocumentTypeId == staffDocumentUploadDto.DocumentTypeId).FirstOrDefaultAsync();
            
            //Return 203 is staff already uploaded the selected document type
            if (documentAlreadyUploaded != null)
                return StatusCodes.Status203NonAuthoritative;

            if (staff == null)
                throw new NullReferenceException("Staff does not exist");

            var saveDocLink = string.Empty;
            if(staffDocumentUploadDto.Document != null)
            {
                var docNamePrefix = "Document" + "_" + staff.Id + "_" + DateTime.Now.Millisecond;
                saveDocLink = await _fileupload.UploadDocument(staffDocumentUploadDto.Document, filePath, directory, docNamePrefix);
            }

            

            staffDocument.Active = true;
            staffDocument.DocumentTypeId = staffDocumentUploadDto.DocumentTypeId;
            staffDocument.DateEntered = DateTime.Now;
            staffDocument.Url = saveDocLink;
            staffDocument.PersonId = staffDocumentUploadDto.PersonId;
            staffDocument.isVerified = false;

            _context.Update(staffDocument);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
        }
      
        public async Task<IEnumerable<StaffUploadedDocxDto>> StaffDocumentUploadHistory(long personId)
        {
        
            

            var selection = await _context.STAFF_DOCUMENT.Where(d => d.PersonId == personId)
                .Select(f => new StaffUploadedDocxDto
                {
                    DocumentName = f.DocumentType.Name,
                    DateEntered = f.DateEntered,
                    ImageUrl = baseUrl + f.Url,
                    DocumentTypeId = f.DocumentTypeId,
                    isVerified = f.isVerified
                    

                }).ToListAsync();

            return selection;
        }


        public async Task<int> StaffDocumentUploadByAdmin(StaffDocumentUploadDto staffDocumentUploadDto, string filePath, string directory)
        {
            StaffDocument staffDocument = new StaffDocument();
            if (staffDocumentUploadDto == null)
            {
                throw new NullReferenceException("Fields were left empty");

            }
            var staff = await _context.STAFF.Where(s => s.PersonId == staffDocumentUploadDto.PersonId).FirstOrDefaultAsync();

            var documentAlreadyUploaded = await _context.STAFF_DOCUMENT.Where(w => w.PersonId == staffDocumentUploadDto.PersonId && w.DocumentTypeId == staffDocumentUploadDto.DocumentTypeId).FirstOrDefaultAsync();

            if (staff == null)
                throw new NullReferenceException("Staff does not exist");

            //Return 203 is staff already uploaded the selected document type
            var saveDocLink = string.Empty;

            if (documentAlreadyUploaded != null && staffDocumentUploadDto.Document != null)
            {
                var docNamePrefix = "Document" + "_" + staff.Id + "_" + DateTime.Now.Millisecond;
                saveDocLink = await _fileupload.UploadDocument(staffDocumentUploadDto.Document, filePath, directory, docNamePrefix);

                documentAlreadyUploaded.Active = true;
                documentAlreadyUploaded.DateEntered = DateTime.Now;
                documentAlreadyUploaded.Url = saveDocLink;
                

                _context.Update(documentAlreadyUploaded);
                await _context.SaveChangesAsync();
                return StatusCodes.Status200OK;
            }

            else
            {
                if (staffDocumentUploadDto.Document != null)
                {
                    var docNamePrefix = "Document" + "_" + staff.Id + "_" + DateTime.Now.Millisecond;
                    saveDocLink = await _fileupload.UploadDocument(staffDocumentUploadDto.Document, filePath, directory, docNamePrefix);
                }

                staffDocument.Active = true;
                staffDocument.DocumentTypeId = staffDocumentUploadDto.DocumentTypeId;
                staffDocument.DateEntered = DateTime.Now;
                staffDocument.Url = saveDocLink;
                staffDocument.PersonId = staffDocumentUploadDto.PersonId;

                _context.Update(staffDocument);
                await _context.SaveChangesAsync();
                return StatusCodes.Status200OK;

            }
            

        
        }

        public async Task<IEnumerable<StaffUploadedDocxDto>> GetStaffDocumentUploadHistoryByAdmin(string staffNumber)
        {

            var getStaff = await _context.STAFF.Where(d => d.GeneratedStaffNumber == staffNumber)
                .Include(d => d.Department)
                .FirstOrDefaultAsync();

         
            if(getStaff != null)
            {
                var staffDoc = await _context.STAFF_DOCUMENT.Where(d => d.PersonId == getStaff.PersonId)
                    .Select(f => new StaffUploadedDocxDto
                    {
                        DocumentName = f.DocumentType.Name,
                        DateEntered = f.DateEntered,
                        ImageUrl = baseUrl + f.Url,
                        isVerified = f.isVerified,
                        verifiedBy = f.verifiedBy,
                        PersonId = f.PersonId,
                        StaffName = f.Person.Firstname + " " + f.Person.Surname,
                        StaffNumber = getStaff.GeneratedStaffNumber,
                        Department = getStaff.Department.Name



                    }).ToListAsync();

                return staffDoc;
            }

            return null;

        }

        public async Task<int> VerifyStaffDocumentByAdmin(DocumentVerificationDto documentVerificationDto)
        {
            var getStaff = await _context.STAFF_DOCUMENT.Where(s => s.PersonId == documentVerificationDto.PersonId && s.DocumentTypeId == documentVerificationDto.DocumentTypeId).FirstOrDefaultAsync();


            if (getStaff == null)
                throw new AppException("Staff Does not exist");

            getStaff.isVerified = true;
            getStaff.verifiedBy = documentVerificationDto.VerficationOfficerId;

            _context.Update(getStaff);
            await _context.SaveChangesAsync();



            return StatusCodes.Status200OK;
        }

    }
}
