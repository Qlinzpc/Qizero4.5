using System;
using System.Collections.Generic;

namespace Qz.Console.Models
{
    public partial class UserPermission
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
        public virtual User User { get; set; }
    }
}
