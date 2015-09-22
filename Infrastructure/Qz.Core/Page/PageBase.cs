using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qz.Caching;

namespace Qz.Core.Page
{
    public class PageBase
    {
        /// <summary>
        /// 登陆 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void Login(string name, object value)
        {
            QCache.Set(name, value, 3600);
        }

        public static object GetValue(string name)
        {
            return QCache.Get(name);
        }

        /// <summary>
        /// 注销 
        /// </summary>
        /// <param name="name"></param>
        public static void Logout(string name)
        {
            QCache.Remove(name);
        }

    }
}
