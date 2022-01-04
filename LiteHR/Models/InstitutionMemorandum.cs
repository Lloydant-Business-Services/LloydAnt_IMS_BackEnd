using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class InstitutionMemorandum : BaseModel
    {
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(50)]
        public string RefNo { get; set; }
        public string Body { get; set; }
        public DateTime DatePosted { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        [MaxLength(200)]
        public string To { get; set; }
        //public long? InstitutionDepartmentId { get; set; }
        //public InstitutionDepartment InstitutionDepartment { get; set; }
        //public long? FacultyId { get; set; }
        //public virtual Faculty Faculty { get; set; }
        //public bool? IsVetted { get; set; }
        public bool Active { get; set; }
        public string Attachment_Url { get; set; }

    }
}
