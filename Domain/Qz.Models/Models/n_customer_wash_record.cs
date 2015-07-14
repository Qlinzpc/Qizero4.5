using System;
using System.Collections.Generic;

namespace Qz.Models
{
    public partial class n_customer_wash_record
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Nullable<System.DateTime> WashDate { get; set; }
        public int WashUserId { get; set; }
        public string WashUserName { get; set; }
        public int WashDeptId { get; set; }
        public string WashDeptName { get; set; }
        public string WashTag { get; set; }
        public int DltFlag { get; set; }
    }
}
