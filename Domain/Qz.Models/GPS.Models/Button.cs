
namespace Qz.GPS.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    using Qz.Common;

    public partial class Button
    {
        public Button()
        {
            //this.ModuleButtonMaps = new List<ModuleButtonMap>();
            //this.RoleModuleButtonMaps = new List<RoleModuleButtonMap>();
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

        [JsonConverter(typeof(DateTimeConverter))] // 日期转换 ( yyyy-MM-dd hh:mm:ss )
        public System.DateTime CreateDate { get; set; }
        public Nullable<int> ModifyUserId { get; set; }
        public string ModifyUserName { get; set; }

        [JsonConverter(typeof(DateTimeConverter))] // 日期转换 ( yyyy-MM-dd hh:mm:ss )
        public Nullable<System.DateTime> ModifyDate { get; set; }

        //[JsonIgnore] // 忽略 JsonConvert 序列化
        //public virtual ICollection<ModuleButtonMap> ModuleButtonMaps { get; set; }

        //[JsonIgnore] // 忽略 JsonConvert 序列化
        //public virtual ICollection<RoleModuleButtonMap> RoleModuleButtonMaps { get; set; }
    }
}
