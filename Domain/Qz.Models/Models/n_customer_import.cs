using System;
using System.Collections.Generic;

namespace Qz.Models
{
    public partial class n_customer_import
    {
        public string import_batch { get; set; }
        public string import_name { get; set; }
        public int customer_category { get; set; }
        public Nullable<int> owner_deptid { get; set; }
        public string owner_deptname { get; set; }
        public Nullable<System.DateTime> owner_date { get; set; }
        public int add_userid { get; set; }
        public string add_username { get; set; }
        public int add_deptid { get; set; }
        public string add_deptname { get; set; }
        public System.DateTime add_date { get; set; }
    }
}
