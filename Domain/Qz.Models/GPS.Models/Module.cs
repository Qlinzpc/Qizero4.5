
namespace Qz.GPS.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    using Qz.Common;

    public partial class Module
    {
        public Module()
        {
            //this.ModuleButtonMaps = new List<ModuleButtonMap>();
            //this.RoleModuleMaps = new List<RoleModuleMap>();
            //this.RoleModuleButtonMaps = new List<RoleModuleButtonMap>();
            //this.RoleModuleColumnMaps = new List<RoleModuleColumnMap>();
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
        
        [JsonConverter(typeof(DateTimeConverter))] // 日期转换 ( yyyy-MM-dd hh:mm:ss )
        public System.DateTime CreateDate { get; set; }
        public Nullable<int> ModifyUserId { get; set; }
        public string ModifyUserName { get; set; }
        
        [JsonConverter(typeof(DateTimeConverter))] // 日期转换 ( yyyy-MM-dd hh:mm:ss )
        public Nullable<System.DateTime> ModifyDate { get; set; }
        
        //[JsonIgnore] // 忽略 JsonConvert 序列化 
        //public virtual Application Application { get; set; }
        
        //[JsonIgnore] // 忽略 JsonConvert 序列化 
        //public virtual ICollection<ModuleButtonMap> ModuleButtonMaps { get; set; }
        
        //[JsonIgnore] // 忽略 JsonConvert 序列化 
        //public virtual ICollection<RoleModuleMap> RoleModuleMaps { get; set; }
        
        //[JsonIgnore] // 忽略 JsonConvert 序列化 
        //public virtual ICollection<RoleModuleButtonMap> RoleModuleButtonMaps { get; set; }
        
        //[JsonIgnore] // 忽略 JsonConvert 序列化 
        //public virtual ICollection<RoleModuleColumnMap> RoleModuleColumnMaps { get; set; }
    }
}
