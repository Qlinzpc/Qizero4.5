using System;
using System.Collections.Generic;

namespace Qz.GPS.Models
{
    public partial class RoleModuleButtonMap
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int ModuleId { get; set; }
        public int ButtonId { get; set; }
        public virtual Button Button { get; set; }
        public virtual Module Module { get; set; }
        public virtual Role Role { get; set; }
    }
}
