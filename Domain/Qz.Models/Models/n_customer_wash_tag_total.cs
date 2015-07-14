using System;
using System.Collections.Generic;

namespace Qz.Models
{
    public partial class n_customer_wash_tag_total
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int NPowerOff { get; set; }
        public int NOccupy { get; set; }
        public int NStop { get; set; }
        public int NMissedCall { get; set; }
        public int NRestrictedPhone { get; set; }
        public int NInvalidPhone { get; set; }
        public int NOutOfReach { get; set; }
        public int NOther { get; set; }
        public int YToSelf { get; set; }
        public int YLeasedSold { get; set; }
        public int YNotRentSell { get; set; }
        public int YPeers { get; set; }
        public int YNotOneself { get; set; }
        public int YHangUp { get; set; }
        public int YOther { get; set; }
    }
}
