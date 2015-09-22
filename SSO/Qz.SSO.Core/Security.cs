using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Qz.SSO.Core.Entity;
using Qz.SSO.Core.Token;

namespace Qz.SSO.Core
{
    public class Security
    {
        public static List<SsoToken> SsoTokens = new List<SsoToken>();

        public static SsoUser Login(string name, string pwd)
        {
            // 1. 参数验证 

            var user = new SsoUser();

            // 2. 数据操作 
            var sql = "select top 1 * from dbo.t_user where name like '%" + name + "%'";
            var reader = SQLHelper.ExecuteReader(sql, CommandType.Text);

            // 3. 获取数据 
            while (reader.Read())
            {
                user.Id = QConvert.ConvertTo<Int32>(reader["id"]);
                user.Usr = QConvert.ConvertTo<string>(reader["usr"]);
                user.Tel = QConvert.ConvertTo<string>(reader["tel"]);
                user.Name = QConvert.ConvertTo<string>(reader["name"]);
                user.No = QConvert.ConvertTo<string>(reader["no"]);
                user.Dept_id = QConvert.ConvertTo<Int32>(reader["dept_id"]);
                user.Role_id = QConvert.ConvertTo<Int32>(reader["role_id"]);
                user.Station = QConvert.ConvertTo<string>(reader["station"]);
                user.DeptName = QConvert.ConvertTo<string>(reader["deptName"]);
            }

            return user;
        }

    }
}
