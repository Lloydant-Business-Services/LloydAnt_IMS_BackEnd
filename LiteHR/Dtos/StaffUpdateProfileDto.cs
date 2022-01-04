using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class StaffUpdateProfileDto
    {
        public IFormFile Passport { get; set; }
        public string ContactAddress { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public string BiometricNo { get; set; }
        public long StaffId { get; set; }
        public long PFANameId { get; set; }
        public long PFAStatusId { get; set; }
        public long AreaOfSpecializationId { get; set; }
        public string RSANumber { get; set; }
        public Nullable<DateTime> DateOfBirth { get; set; }
        public Nullable<DateTime> DateOfAssumption { get; set; }
        public Nullable<DateTime> DateOfConfirmation { get; set; }
        public Nullable<DateTime> DateOfEmployment { get; set; }
        public Nullable<DateTime> DateOfLastPromotion { get; set; }


    }
}
