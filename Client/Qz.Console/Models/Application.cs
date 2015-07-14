using System;
using System.Collections.Generic;

namespace Qz.Console.Models
{
    public partial class Application
    {
        public Application()
        {
            this.Modules = new List<Module>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Remark { get; set; }
        public int Enabled { get; set; }
        public int SortCode { get; set; }
        public int IsDelete { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public Nullable<int> ModifyUserId { get; set; }
        public string ModifyUserName { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public virtual ICollection<Module> Modules { get; set; }
    }
}
