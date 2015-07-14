
namespace Qz.GPS.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    using Qz.Common;

    public partial class Application
    {
        public Application()
        {
            //this.Modules = new List<Module>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Remark { get; set; }
        public int Enabled { get; set; }
        public int SortCode { get; set; }
        public int IsDelete { get; set; }

        [JsonConverter(typeof(DateTimeConverter))] // 日期转换 ( yyyy-MM-dd hh:mm:ss )
        public System.DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public int ModifyUserId { get; set; }
        public string ModifyUserName { get; set; }

        [JsonConverter(typeof(DateTimeConverter))] // 日期转换 ( yyyy-MM-dd hh:mm:ss )
        public Nullable<System.DateTime> ModifyDate { get; set; }
        //[JsonIgnore] // 忽略 JsonConvert 序列化 
        //public virtual ICollection<Module> Modules { get; set; }
    }
}
