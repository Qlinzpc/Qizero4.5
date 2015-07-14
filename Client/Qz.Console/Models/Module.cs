using System;
using System.Collections.Generic;

namespace Qz.Console.Models
{
    public partial class Module
    {
        public Module()
        {
            this.ModuleButtonMaps = new List<ModuleButtonMap>();
            this.RoleModuleMaps = new List<RoleModuleMap>();
            this.RoleModuleButtonMaps = new List<RoleModuleButtonMap>();
            this.RoleModuleColumnMaps = new List<RoleModuleColumnMap>();
        }

        public int Id { get; set; }
        public int ParentId { get; set; }
        public int ApplicationId { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string Icon { get; set; }
        public string IconURL { get; set; }
        public string Remark { get; set; }
        public int Enabled { get; set; }
        public int SortCode { get; set; }
        public int IsDelete { get; set; }
        public int IsVisible { get; set; }
        public int CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<int> ModifyUserId { get; set; }
        public string ModifyUserName { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string Code { get; set; }
        public virtual Application Application { get; set; }
        public virtual ICollection<ModuleButtonMap> ModuleButtonMaps { get; set; }
        public virtual ICollection<RoleModuleMap> RoleModuleMaps { get; set; }
        public virtual ICollection<RoleModuleButtonMap> RoleModuleButtonMaps { get; set; }
        public virtual ICollection<RoleModuleColumnMap> RoleModuleColumnMaps { get; set; }
    }
}
