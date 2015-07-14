using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qz.GPS.DirectService.Parameter
{
    public class Module
    {
        /// <summary>
        /// 根据用户 ( User ) 获得模块, 参数 ( Parameter )  
        /// </summary>
        public class ByUser
        {
            /// <summary>
            /// 用户Id 
            /// </summary>
            public int UserId { get; set; }
            /// <summary>
            /// 应用Id
            /// </summary>
            public int ApplicationId { get; set; }
            /// <summary>
            /// 父Id 
            /// </summary>
            public int ParentId { get; set; }
        }

        /// <summary>
        /// 根据角色 ( Role ) 获得模块, 参数 ( Parameter )  
        /// </summary>
        public class ByRole
        {
            /// <summary>
            /// 角色Id 
            /// </summary>
            public int RoleId { get; set; }
        }
    }
}
