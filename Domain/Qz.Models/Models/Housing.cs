using System;
using System.Collections.Generic;

namespace Qz.Models
{
    public partial class Housing
    {
        public int Id { get; set; }
        public Nullable<int> DId { get; set; }
        public string DName { get; set; }
        public Nullable<int> AId { get; set; }
        public string Area { get; set; }
        public Nullable<int> BuildId { get; set; }
        public string BuildName { get; set; }
        public int EId { get; set; }
        public string EstateName { get; set; }
        public Nullable<int> RoomId { get; set; }
        public string RoomNo { get; set; }
        public Nullable<int> TradeType { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public Nullable<decimal> LeasePrice { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string Owner { get; set; }
        public string OwnerTel { get; set; }
        public Nullable<decimal> CostPrice { get; set; }
        public string PocNo { get; set; }
        public string PocStatus { get; set; }
        public Nullable<int> Redeem { get; set; }
        public string UseStatus { get; set; }
        public string PropertyRight { get; set; }
        public string Fitment { get; set; }
        public string HouseStyle { get; set; }
        public int Use { get; set; }
        public string MergePy { get; set; }
        public Nullable<System.DateTime> MarkDate { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public Nullable<int> AddUId { get; set; }
        public string AddUser { get; set; }
        public string AddUDept { get; set; }
        public Nullable<int> AddUDeptId { get; set; }
        public string MarkUDept { get; set; }
        public Nullable<System.DateTime> UDDate { get; set; }
        public Nullable<int> MarkUId { get; set; }
        public string MarkUser { get; set; }
        public Nullable<int> DelFalg { get; set; }
    }
}
