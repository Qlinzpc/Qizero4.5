using System;
using System.Collections.Generic;

namespace Qz.GPS.Models
{
    public partial class LoginLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string HostName { get; set; }
        public string HostIP { get; set; }
        public string LoginMsg { get; set; }
        public System.DateTime LoginDate { get; set; }
    }
}
