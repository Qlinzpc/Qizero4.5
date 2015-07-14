using System;
using System.Collections.Generic;

namespace Qz.Console.Models
{
    public partial class Role
    {
        public Role()
        {
            this.DepartmentRoles = new List<DepartmentRole>();
            this.RoleModuleButtonMaps = new List<RoleModuleButtonMap>();
            this.RoleModuleColumnMaps = new List<RoleModuleColumnMap>();
            this.RoleModuleMaps = new List<RoleModuleMap>();
            this.RolePermissions = new List<RolePermission>();
            this.UserRoles = new List<UserRole>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }
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
        public virtual ICollection<DepartmentRole> DepartmentRoles { get; set; }
        public virtual ICollection<RoleModuleButtonMap> RoleModuleButtonMaps { get; set; }
        public virtual ICollection<RoleModuleColumnMap> RoleModuleColumnMaps { get; set; }
        public virtual ICollection<RoleModuleMap> RoleModuleMaps { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
