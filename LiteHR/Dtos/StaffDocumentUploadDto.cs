using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class StaffDocumentUploadDto
    {
        public long PersonId { get; set; }
        public long DocumentTypeId { get; set; }
        public IFormFile Document { get; set; }

    }
}
