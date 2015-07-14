using System;
using System.Collections.Generic;

namespace Qz.Models
{
    public partial class n_customer_wash
    {
        public int customer_id { get; set; }
        public string customer_name { get; set; }
        public string customer_tel { get; set; }
        public string customer_tel_append { get; set; }
        public int customer_category { get; set; }
        public int customer_source { get; set; }
        public string customer_remarks { get; set; }
        public int customer_level { get; set; }
        public int customer_paytype { get; set; }
        public string city_ids { get; set; }
        public string city_names { get; set; }
        public string area_ids { get; set; }
        public string area_names { get; set; }
        public string estate_ids { get; set; }
        public string estate_names { get; set; }
        public Nullable<int> price { get; set; }
        public Nullable<int> acreage { get; set; }
        public Nullable<int> room { get; set; }
        public int customer_isself { get; set; }
        public int customer_dltflag { get; set; }
        public Nullable<int> follow_newstatus { get; set; }
        public Nullable<System.DateTime> follow_newsdate { get; set; }
        public Nullable<int> seehousing_count { get; set; }
        public Nullable<int> owner_userid { get; set; }
        public string owner_username { get; set; }
        public Nullable<int> owner_deptid { get; set; }
        public string owner_deptname { get; set; }
        public Nullable<System.DateTime> owner_date { get; set; }
        public Nullable<System.DateTime> WashDate { get; set; }
        public int WashUserId { get; set; }
        public int DataStatus { get; set; }
        public int DltFlag { get; set; }
    }
}
