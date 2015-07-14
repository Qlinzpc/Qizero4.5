using System;
using System.Collections.Generic;

namespace Qz.Console.Models
{
    public partial class SysLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Location { get; set; }
        public string Action { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public System.DateTime CreateDate { get; set; }
    }
}
