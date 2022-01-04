using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class AppendSignatureDto
    {
        public long SignaturePin { get; set; }
        public IFormFile Signature { get; set; }
    }
}
