using System;
using System.Collections.Generic;

namespace Qz.Console.Models
{
    public partial class Department
    {
        public Department()
        {
            this.DepartmentRoles = new List<DepartmentRole>();
            this.Users = new List<User>();
        }

        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int ParentId { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string Nature { get; set; }
        public string Manager { get; set; }
        public string Phone { get; set; }
        public string Remark { get; set; }
        public int Enabled { get; set; }
        public int SortCode { get; set; }
        public int IsDelete { get; set; }
        public int CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<int> ModifyUserId { get; set; }
        public string ModifyUserName { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<DepartmentRole> DepartmentRoles { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
