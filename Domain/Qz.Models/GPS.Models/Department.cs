
namespace Qz.GPS.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    using Qz.Common;

    public partial class Department
    {
        public Department()
        {
            //this.DepartmentRoles = new List<DepartmentRole>();
            //this.Users = new List<User>();
        }

        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int ParentId { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string Nature { get; set; }
        public string Manager { get; set; }
        public string Phone { get; set; }
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
        //public virtual Company Company { get; set; }

        //[JsonIgnore] // 忽略 JsonConvert 序列化
        //public virtual ICollection<DepartmentRole> DepartmentRoles { get; set; }

        //[JsonIgnore] // 忽略 JsonConvert 序列化
        //public virtual ICollection<User> Users { get; set; }
    }
}
