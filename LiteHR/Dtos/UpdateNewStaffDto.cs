using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class UpdateNewStaffDto
    {
        public string PhoneNumber { get; set; }
        public long ?GenderId { get; set; }
        public long ?StateId { get; set; }
        public long ?LgaId { get; set; }
        public long ?DepartmentId { get; set; }

    }
}
