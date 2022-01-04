using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class StaffGrade: BaseModel
    {
        public long SalaryGradeId { get; set; }

        public SalaryGrade SalaryGrade { get; set; }

        public long StaffId { get; set; }

        public Staff Staff { get; set; }

        public bool Active { get; set; }

        public DateTime DatePromoted { get; set; }
    }
}