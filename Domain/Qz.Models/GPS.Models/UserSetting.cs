using System;
using System.Collections.Generic;

namespace Qz.GPS.Models
{
    public partial class UserSetting
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
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
    }
}
