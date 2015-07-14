using System;
using System.Collections.Generic;

namespace Qz.Models
{
    public partial class HouseDetail
    {
        public int Id { get; set; }
        public string HouseNo { get; set; }
        public string HouseType { get; set; }
        public string DevelopersOutcry { get; set; }
        public string HouseUse { get; set; }
        public string CoveredArea { get; set; }
        public string IndoorArea { get; set; }
        public string AssessmentArea { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
    }
}
