using System;
using System.Collections.Generic;

namespace Qz.Models
{
    public partial class NLog_Error
    {
        public int Id { get; set; }
        public System.DateTime time_stamp { get; set; }
        public string host { get; set; }
        public string type { get; set; }
        public string source { get; set; }
        public string message { get; set; }
        public string level { get; set; }
        public string logger { get; set; }
        public string stacktrace { get; set; }
        public string allxml { get; set; }
        public string detail { get; set; }
    }
}
