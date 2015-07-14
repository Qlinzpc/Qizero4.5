using System;
using System.Collections.Generic;

namespace Qz.Console.Models
{
    public partial class DepartmentRole
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
        public virtual Department Department { get; set; }
        public virtual Role Role { get; set; }
    }
}
