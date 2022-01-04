using LiteHR.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class InstituitionMemorandumDto : BaseModel
    {
        public string Title { get; set; }
        public string RefNo { get; set; }
        public string Body { get; set; }
        public DateTime DatePosted { get; set; }
        public long UserId { get; set; }
        public long DepartmentId { get; set; }
        public long FacultyId { get; set; }
        //public string To { get; set; }
        public List<int> RoleIds { get; set; }
        //public int RoleIds { get; set; }
        public bool Active { get; set; }
        public IFormFile Attachment { get; set; }
    }
}
