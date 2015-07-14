using System;
using System.Collections.Generic;

namespace Qz.Models
{
    public partial class n_customer_import_excel
    {
        public int import_excel_id { get; set; }
        public string import_batch { get; set; }
        public string customer_name { get; set; }
        public string customer_tel { get; set; }
        public string customer_tel_append { get; set; }
        public string customer_remarks { get; set; }
        public int impotr_result { get; set; }
        public string import_result_msg { get; set; }
    }
}
