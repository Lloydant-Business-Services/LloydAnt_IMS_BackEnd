using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class StaffAssetDto
    {
        public long StaffId { get; set; }
        public long AssetId { get; set; }
        public string SerialNumber { get; set; }
        public string AssetName { get; set; }
        public string AssetTypeName { get; set; }
        //public string AssetNumber { get; set; } = Guid.NewGuid().ToString();
    }
}
