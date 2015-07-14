using System;
using System.Collections.Generic;

namespace Qz.Console.Models
{
    public partial class Permission
    {
        public Permission()
        {
            this.RolePermissions = new List<RolePermission>();
            this.UserPermissions = new List<UserPermission>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public string Remark { get; set; }
        public int Enabled { get; set; }
        public int SortCode { get; set; }
        public int IsDelete { get; set; }
        public int CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<int> ModifyUserId { get; set; }
        public string ModifyUserName { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
    }
}
