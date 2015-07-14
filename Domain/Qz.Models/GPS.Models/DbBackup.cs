using System;
using System.Collections.Generic;

namespace Qz.GPS.Models
{
    public partial class DbBackup
    {
        public int Id { get; set; }
        public string ServerName { get; set; }
        public string DbName { get; set; }
        public string JobName { get; set; }
        public string Mode { get; set; }
        public System.DateTime StartTime { get; set; }
        public string FilePath { get; set; }
        public string Remark { get; set; }
        public int Enabled { get; set; }
        public int CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public System.DateTime CreateDate { get; set; }
    }
}
