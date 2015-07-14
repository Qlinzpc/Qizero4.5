using System;
using System.Collections.Generic;

namespace Qz.Models
{
    public partial class ProjectDetail
    {
        public int Id { get; set; }
        public string ProjectNo { get; set; }
        public string Address { get; set; }
        public System.DateTime TransfereeDate { get; set; }
        public string DurableYears { get; set; }
        public string HousingUse { get; set; }
        public string LandUse { get; set; }
        public Nullable<decimal> LandArea { get; set; }
        public Nullable<decimal> OverallFloorage { get; set; }
        public Nullable<int> PreSaleTotal { get; set; }
        public Nullable<decimal> PreSaleArea { get; set; }
        public string SalePhone1 { get; set; }
        public string SalePhone2 { get; set; }
        public string PropertyManagementCompany { get; set; }
        public string ManagementCost { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
    }
}
