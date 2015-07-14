using System;
using System.Collections.Generic;

namespace Qz.Models
{
    public partial class Order
    {
        public int Orderid { get; set; }
        public Nullable<int> custid { get; set; }
    }
}
