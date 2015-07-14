using System;
using System.Collections.Generic;

namespace Qz.Models
{
    public partial class n_customer_follow
    {
        public int follow_id { get; set; }
        public int customer_id { get; set; }
        public int follow_userid { get; set; }
        public string follow_username { get; set; }
        public int follow_deptid { get; set; }
        public string follow_deptname { get; set; }
        public int follow_type { get; set; }
        public int follow_status { get; set; }
        public string follow_content { get; set; }
        public int follow_dltflag { get; set; }
        public System.DateTime follow_date { get; set; }
    }
}
