using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Models
{
    public class Settings:BaseModel
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string BaseColour { get; set; }
    }
}
