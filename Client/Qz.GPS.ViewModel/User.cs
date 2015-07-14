using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qz.GPS.ViewModel
{
    [Serializable]
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public string Code { get; set; }
        public string Mobile { get; set; }

        public string Account { get; set; }
        public string Secretkey { get; set; }
        public string Telephone { get; set; }
        public int OpenId { get; set; }
        public int LoginCount { get; set; }
        public DateTime? FirstVisit { get; set; }
        public DateTime? PreviousVisit { get; set; }
        public DateTime? LastVisit { get; set; }
        public string Config { get; set; }
        public string Remark { get; set; }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
