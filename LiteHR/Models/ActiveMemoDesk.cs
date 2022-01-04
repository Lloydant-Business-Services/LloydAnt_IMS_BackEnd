using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class ActiveMemoDesk : BaseModel
    {
        public InstitutionMemorandum InstitutionMemorandum { get; set; }
        public long InstitutionMemorandumId { get; set; }
        public bool IsVetted { get; set; }
        public bool? IsActed { get; set; }
        public bool? IsPublished { get; set; }
        [ForeignKey("RoleId")]
        public long RoleId { get; set; }
        public Role Role { get; set; }
        public long? InstitutionDepartmentId { get; set; }
        public InstitutionDepartment InstitutionDepartment { get; set; }
        public string Comments { get; set; }

    }
}
