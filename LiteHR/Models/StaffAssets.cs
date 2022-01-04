using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class StaffAssets:BaseModel
    {
        public Staff Staff { get; set; }
        public long StaffId { get; set; }
        public Asset Asset { get; set; }
        public long AssetId { get; set; }
        public string  SerialNumber { get; set; }
        public string AssetNumber { get; set; } = Guid.NewGuid().ToString();
    }
}
