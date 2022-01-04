using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class ChangeOfNameDto
    {
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Othername { get; set; }
        public IFormFile Attachment { get; set; }
        public string Comments { get; set; }
        public long StaffId { get; set; }
        
    }
}
