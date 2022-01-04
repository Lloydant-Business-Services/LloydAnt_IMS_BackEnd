using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class SalaryExtraEarning: BaseModel
    {
        public long StaffId { get; set; }

        public Staff Staff { get; set; }

        public long SalaryExtraTypeId { get; set; }

        public SalaryExtraType SalaryExtraType { get; set; }

        public decimal Amount { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsDeductible { get; set; }

        public bool Active { get; set; }
    }
}