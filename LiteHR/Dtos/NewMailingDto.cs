using LiteHR.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class NewMailingDto
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string SignatureUrl { get; set; }
        public string OriginatorExternal { get; set; }
        public string OriginatorEmail { get; set; }
        public IFormFile AttachmentUrl { get; set; }
        public long RoleId { get; set; }
        public long ToStaffId { get; set; }
        public long DepartmentId { get; set; }
        public long FacultyId { get; set; }
        public long OriginatorUserId { get; set; }
        public long FileTypeId { get; set; }
        public bool IsConfidential { get; set; }
        public bool IsExternal { get; set; }
        //public string CopyList { get; set; }
    }

    public class CopyListDto
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
    }
}
