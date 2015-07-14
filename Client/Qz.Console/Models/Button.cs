using System;
using System.Collections.Generic;

namespace Qz.Console.Models
{
    public partial class Button
    {
        public Button()
        {
            this.ModuleButtonMaps = new List<ModuleButtonMap>();
            this.RoleModuleButtonMaps = new List<RoleModuleButtonMap>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Icon { get; set; }
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
        public virtual ICollection<ModuleButtonMap> ModuleButtonMaps { get; set; }
        public virtual ICollection<RoleModuleButtonMap> RoleModuleButtonMaps { get; set; }
    }
}
