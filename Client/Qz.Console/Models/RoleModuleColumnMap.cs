using System;
using System.Collections.Generic;

namespace Qz.Console.Models
{
    public partial class RoleModuleColumnMap
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int ModuleId { get; set; }
        public int IsReject { get; set; }
        public string FieleName { get; set; }
        public virtual Module Module { get; set; }
        public virtual Role Role { get; set; }
    }
}
