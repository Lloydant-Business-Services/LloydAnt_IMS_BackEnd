using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class SendEmailDto
    {
        public string VerificationGuid { get; set; }
        public string ReceiverEmail { get; set; }
        public string ReceiverName { get; set; }
        public string Subject { get; set; }
        public string CTAUrl { get; set; }
        public string SenderEmail { get; set; } = "noreply@unizik.edu.ng";
        public string SenderName { get; set; } = "UNIZIK HR";
        public int VerificationCategory { get; set; }
        public string Body { get; set; }
        public string VacancyName { get; set; }
        public long VacancyId { get; set; }
        public string AccessCode { get; set; }
        public string ButtonText { get; set; } = "Reset Password";
        //public string ButtonText { get; set; } = "Apply Now";
        public long Id { get; set; }
    }
}
