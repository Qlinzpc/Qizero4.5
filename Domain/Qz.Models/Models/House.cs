using System;
using System.Collections.Generic;

namespace Qz.Models
{
    public class House
    {
        public int Id { get; set; }
        public string HouseNo { get; set; }
        public string BuildNo { get; set; }
        public string HouseName { get; set; }
        public string HouseFloor { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
    }
}
