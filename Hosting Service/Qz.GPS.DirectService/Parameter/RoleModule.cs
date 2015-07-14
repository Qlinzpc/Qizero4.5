using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qz.GPS.DirectService.Parameter
{
    public class RoleModule
    {
        public class ByRole
        {
            public int RoleId { get; set; }
        }

        public class Add
        {
            public int RoleId { get; set; }
            public string ModuleIds { get; set; }
        }
        public class Remove
        {
            public int RoleId { get; set; }
            public string ModuleIds { get; set; }
        }

    }
}
