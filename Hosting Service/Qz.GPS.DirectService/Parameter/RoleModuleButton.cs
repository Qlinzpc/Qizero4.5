using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qz.GPS.DirectService.Parameter
{
    public class RoleModuleButton
    {
        public class Remove
        {
            public int RoleId { get; set; }
            public int ModuleId { get; set; }
            public string ButtonIds { get; set; }
        }
        public class Add
        {
            public int RoleId { get; set; }
            public int ModuleId { get; set; }
            public string ButtonIds { get; set; }
        }
    }
}
