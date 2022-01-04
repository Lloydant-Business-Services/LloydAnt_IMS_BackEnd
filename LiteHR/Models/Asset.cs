using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class Asset:BaseModel
    {
        public AssetType AssetType { get; set; }
        public long AssetTypeId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
