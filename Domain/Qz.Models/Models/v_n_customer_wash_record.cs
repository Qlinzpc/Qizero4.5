using System;
using System.Collections.Generic;

namespace Qz.Models
{
    public partial class v_n_customer_wash_record
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int CustomerCategory { get; set; }
        public int CustomerSource { get; set; }
        public int CustomerLevel { get; set; }
        public int WashUserId { get; set; }
        public string WashUserName { get; set; }
        public int WashDeptId { get; set; }
        public string WashDeptName { get; set; }
        public Nullable<System.DateTime> WashDate { get; set; }
        public string WashTag { get; set; }
    }
}
