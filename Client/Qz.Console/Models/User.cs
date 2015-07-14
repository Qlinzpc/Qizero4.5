using System;
using System.Collections.Generic;

namespace Qz.Console.Models
{
    public partial class User
    {
        public User()
        {
            this.UserPermissions = new List<UserPermission>();
            this.UserRoles = new List<UserRole>();
        }

        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public int InnerUser { get; set; }
        public string Code { get; set; }

        public string Account { get; set; }
        public string Password { get; set; }
        public string Secretkey { get; set; }
        public string UserName { get; set; }
        public string Spell { get; set; }
        public string Gender { get; set; }
        public string Birthday { get; set; }
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        public Nullable<System.DateTime> ChangePasswordDate { get; set; }
        public int OpenId { get; set; }
        public int LoginCount { get; set; }
        public Nullable<System.DateTime> FirstVisit { get; set; }
        public Nullable<System.DateTime> PreviousVisit { get; set; }
        public Nullable<System.DateTime> LastVisit { get; set; }
        public int Online { get; set; }
        public string Config { get; set; }
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
        public virtual Department Department { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
