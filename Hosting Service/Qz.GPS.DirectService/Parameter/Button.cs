using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qz.GPS.DirectService.Parameter
{
    public class Button
    {
        /// <summary>
        /// 根据模块 ( Module ) 和 角色 ( Role ) 获得授权模块按钮 
        /// </summary>
        public class ByModuleAndRole
        {
            /// <summary>
            /// 角色Id
            /// </summary>
            public int RoleId { get; set; }
            /// <summary>
            /// 模块Id 
            /// </summary>
            public int ModuleId { get; set; }
        }

        /// <summary>
        /// 根据模块 ( Module ) 获得授权模块按钮 
        /// </summary>
        public class ByModule
        {
            /// <summary>
            /// 模块Id 
            /// </summary>
            public int ModuleId { get; set; }
        }
    }
}
