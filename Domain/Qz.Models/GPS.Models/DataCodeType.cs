using System;
using System.Collections.Generic;

namespace Qz.GPS.Models
{
    public partial class DataCodeType
    {
        public int Id { get; set; }
        public string CodeType { get; set; }
        public string CodeTypeName { get; set; }
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
