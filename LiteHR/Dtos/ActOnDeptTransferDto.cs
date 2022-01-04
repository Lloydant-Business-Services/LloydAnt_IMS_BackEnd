using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class ActOnDeptTransferDto
    {
        public long StaffId { get; set; }
        public bool ApproveRequest { get; set; }
    }
}
