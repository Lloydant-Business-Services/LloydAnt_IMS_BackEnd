using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class DocumentVerificationDto
    {
        public long PersonId { get; set; }
        public long DocumentTypeId { get; set; }
        public long VerficationOfficerId { get; set; }
    }
}
