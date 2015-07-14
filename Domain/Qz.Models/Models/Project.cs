using System;
using System.Collections.Generic;

namespace Qz.Models
{
    public partial class Project
    {
        public int Id { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public string Area { get; set; }
        public string DevelopEnterprise { get; set; }
        public System.DateTime ApproveTime { get; set; }
        public string PreSaleXuNo { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
    }
}
