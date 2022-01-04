using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class NewStaffCountDto
    {
        public string DepartmentName { get; set; }
        public List<RankStaffCount> RankStaffCounts { get; set; }
        public int Count { get; set; }

    }
    public class RankStaffCount
    {
        public string RankName { get; set; }
        public int Count { get; set; }
    }
}
