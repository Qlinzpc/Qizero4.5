
namespace Qz.GPS.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    using Qz.Common;

    public partial class Role
    {
        public Role()
        {
            //this.DepartmentRoles = new List<DepartmentRole>();
            //this.RoleModuleButtonMaps = new List<RoleModuleButtonMap>();
            //this.RoleModuleColumnMaps = new List<RoleModuleColumnMap>();
            //this.RoleModuleMaps = new List<RoleModuleMap>();
            //this.RolePermissions = new List<RolePermission>();
            //this.UserRoles = new List<UserRole>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Remark { get; set; }
        public int Enabled { get; set; }
        public int SortCode { get; set; }
        public int IsDelete { get; set; }
        public int CreateUserId { get; set; }
        public string CreateUserName { get; set; }

        [JsonConverter(typeof(DateTimeConverter))] // 日期转换 ( yyyy-MM-dd hh:mm:ss )
        public System.DateTime CreateDate { get; set; }
        public Nullable<int> ModifyUserId { get; set; }
        public string ModifyUserName { get; set; }

        [JsonConverter(typeof(DateTimeConverter))] // 日期转换 ( yyyy-MM-dd hh:mm:ss )
        public Nullable<System.DateTime> ModifyDate { get; set; }

        //[JsonIgnore] // 忽略 JsonConvert 序列化 
        //public virtual ICollection<DepartmentRole> DepartmentRoles { get; set; }
        
        //[JsonIgnore] // 忽略 JsonConvert 序列化 
        //public virtual ICollection<RoleModuleButtonMap> RoleModuleButtonMaps { get; set; }

        //[JsonIgnore] // 忽略 JsonConvert 序列化 
        //public virtual ICollection<RoleModuleMap> RoleModuleMaps { get; set; }

        //[JsonIgnore] // 忽略 JsonConvert 序列化 
        //public virtual ICollection<RoleModuleColumnMap> RoleModuleColumnMaps { get; set; }

        //[JsonIgnore] // 忽略 JsonConvert 序列化 
        //public virtual ICollection<RolePermission> RolePermissions { get; set; }

        //[JsonIgnore] // 忽略 JsonConvert 序列化 
        //public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
