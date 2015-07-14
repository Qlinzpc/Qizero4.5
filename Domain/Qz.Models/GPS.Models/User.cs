
namespace Qz.GPS.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    using Qz.Common;

    public partial class User
    {
        public User()
        {
            //this.UserPermissions = new List<UserPermission>();
            //this.UserRoles = new List<UserRole>();
        }

        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public Nullable<int> InnerUser { get; set; }
        public string Code { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Secretkey { get; set; }
        public string UserName { get; set; }
        public string Spell { get; set; }
        public string Gender { get; set; }
        public string Birthday { get; set; }
        public string Mobile { get; set; }
        public string Telephone { get; set; }

        [JsonConverter(typeof(DateTimeConverter))] // 日期转换 ( yyyy-MM-dd hh:mm:ss )
        public Nullable<System.DateTime> ChangePasswordDate { get; set; }
        public Nullable<int> OpenId { get; set; }
        public Nullable<int> LoginCount { get; set; }

        [JsonConverter(typeof(DateTimeConverter))] // 日期转换 ( yyyy-MM-dd hh:mm:ss )
        public Nullable<System.DateTime> FirstVisit { get; set; }

        [JsonConverter(typeof(DateTimeConverter))] // 日期转换 ( yyyy-MM-dd hh:mm:ss )
        public Nullable<System.DateTime> PreviousVisit { get; set; }

        [JsonConverter(typeof(DateTimeConverter))] // 日期转换 ( yyyy-MM-dd hh:mm:ss )
        public Nullable<System.DateTime> LastVisit { get; set; }
        public Nullable<int> Online { get; set; }
        public string Config { get; set; }
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
        //public virtual Department Department { get; set; }

        //[JsonIgnore] // 忽略 JsonConvert 序列化
        //public virtual ICollection<UserPermission> UserPermissions { get; set; }
        
        //[JsonIgnore] // 忽略 JsonConvert 序列化 
        //public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
