using System;
using System.Collections.Generic;

namespace Qz.Models
{
    public partial class Building
    {
        public int Id { get; set; }
        public string BuildNo { get; set; }
        public string ProjectNo { get; set; }
        public string BuildName { get; set; }
        public string ParentNo { get; set; }
        public string PlanningLicence { get; set; }
        public string ConstrucLicence { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
    }
}
