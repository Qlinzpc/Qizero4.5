using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qz.SSO.Core.Entity
{
    public class SsoUser
    {
        #region 公共属性
        private int _Id;
        /// <summary>
        /// 
        /// </summary>
        public int Id { get { return _Id; } set { _Id = value; } }

        private string _usr;
        /// <summary>
        /// 
        /// </summary>
        public string Usr { get { return _usr; } set { _usr = value; } }

        private string _tel;
        /// <summary>
        /// 
        /// </summary>
        public string Tel { get { return _tel; } set { _tel = value; } }

        private string _name;
        /// <summary>
        /// 
        /// </summary>
        public string Name { get { return _name; } set { _name = value; } }

        private string _NamePy;
        /// <summary>
        /// 
        /// </summary>
        public string NamePy { get { return _NamePy; } set { _NamePy = value; } }

        private string _no;
        /// <summary>
        /// 
        /// </summary>
        public string No { get { return _no; } set { _no = value; } }

        private string _brief;
        /// <summary>
        /// 
        /// </summary>
        public string Brief { get { return _brief; } set { _brief = value; } }

        private int _dept_id;
        /// <summary>
        /// 
        /// </summary>
        public int Dept_id { get { return _dept_id; } set { _dept_id = value; } }

        private int _role_id;
        /// <summary>
        /// 
        /// </summary>
        public int Role_id { get { return _role_id; } set { _role_id = value; } }

        private string _pwd;
        /// <summary>
        /// 
        /// </summary>
        public string Pwd { get { return _pwd; } set { _pwd = value; } }

        private bool _CanDeal;
        /// <summary>
        /// 
        /// </summary>
        public bool CanDeal { get { return _CanDeal; } set { _CanDeal = value; } }

        private DateTime _LoginDate;
        /// <summary>
        /// 
        /// </summary>
        public DateTime LoginDate { get { return _LoginDate; } set { _LoginDate = value; } }

        private int _QryTimes;
        /// <summary>
        /// 
        /// </summary>
        public int QryTimes { get { return _QryTimes; } set { _QryTimes = value; } }

        private bool _DltFlag;
        /// <summary>
        /// 
        /// </summary>
        public bool DltFlag { get { return _DltFlag; } set { _DltFlag = value; } }

        private string _guid;
        /// <summary>
        /// 
        /// </summary>
        public string Guid { get { return _guid; } set { _guid = value; } }

        private int _tmp_id;
        /// <summary>
        /// 
        /// </summary>
        public int Tmp_id { get { return _tmp_id; } set { _tmp_id = value; } }

        private DateTime _create_date;
        /// <summary>
        /// 
        /// </summary>
        public DateTime Create_date { get { return _create_date; } set { _create_date = value; } }

        private bool _luxurious;
        /// <summary>
        /// 
        /// </summary>
        public bool Luxurious { get { return _luxurious; } set { _luxurious = value; } }

        private string _identityNo;
        /// <summary>
        /// 
        /// </summary>
        public string IdentityNo { get { return _identityNo; } set { _identityNo = value; } }

        private string _curtmobile;
        /// <summary>
        /// 
        /// </summary>
        public string Curtmobile { get { return _curtmobile; } set { _curtmobile = value; } }

        private string _station;
        /// <summary>
        /// 
        /// </summary>
        public string Station { get { return _station; } set { _station = value; } }

        private string _deptName;
        /// <summary>
        /// 
        /// </summary>
        public string DeptName { get { return _deptName; } set { _deptName = value; } }

        #endregion
    }
}
