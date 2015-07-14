using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qz.Common
{
    public class QConst
    {
        /// <summary>
        /// 添加成功
        /// </summary>
        public const string INSERT_SUCCESS = "添加成功 !";
        /// <summary>
        /// 添加失败
        /// </summary>
        public const string INSERT_FAIL = "添加失败 !";

        /// <summary>
        /// 删除成功
        /// </summary>
        public const string DELETE_SUCCESS = "删除成功 !";
        /// <summary>
        /// 删除失败
        /// </summary>
        public const string DELETE_FAIL = "删除失败 !";

        /// <summary>
        /// 修改成功
        /// </summary>
        public const string UPDATE_SUCCESS = "修改成功 !";
        /// <summary>
        /// 修改失败
        /// </summary>
        public const string UPDATE_FAIL = "修改失败 !";

        /// <summary>
        /// 加密密钥错误 
        /// </summary>
        public const string ENCRYPT_KEY_ERROR = "加密密钥错误 !";
        /// <summary>
        /// 加密成功 
        /// </summary>
        public const string ENCRYPT_SUCCESS = "加密成功 !";
        /// <summary>
        /// 加密失败 
        /// </summary>
        public const string ENCRYPT_ERROR = "加密失败 !";

        /// <summary>
        /// 解密密钥错误 
        /// </summary>
        public const string DECRYPT_KEY_ERROR = "解密密钥错误 !";
        /// <summary>
        /// 解密成功 
        /// </summary>
        public const string DECRYPT_SUCCESS = "解密成功 !";
        /// <summary>
        /// 解密失败 
        /// </summary>
        public const string DECRYPT_ERROR = "解密失败 !";

        /// <summary>
        /// 加密, 解密 KEY 
        /// </summary>
        public const string SECURITY_KEY = "security";

        /// <summary>
        /// User Session Name 
        /// </summary>
        public const string SESSION_USER_NAME = "QzUser";
        /// <summary>
        /// Module List Session Name 
        /// </summary>
        public const string SESSION_MODULE_LIST_NAME = "QzModuleList";
        /// <summary>
        /// Module Json Session Name 
        /// </summary>
        public const string SESSION_MODULE_JSON_NAME = "QzModuleJson";

        /// <summary>
        /// Login 成功 
        /// </summary>
        public const string LOGIN_SUCCESS = "登录成功 !";
        /// <summary>
        /// Login 失败  
        /// </summary>
        public const string LOGIN_FAIL = "登录失败 !";
        /// <summary>
        /// Login 用户名错误 
        /// </summary>
        public const string LOGIN_USERNAME_ERROR = "用户名错误 !";
        /// <summary>
        /// Login 密码错误 
        /// </summary>
        public const string LOGIN_PASSWORD_ERROR = "密码错误 !";

        /// <summary>
        /// 系统内暂无数据 
        /// </summary>
        public const string SYS_DATA_NULL = "Sorry, 系统内暂无您查询的数据 !";

        /// <summary>
        /// 登录 URL 
        /// </summary>
        public const string LOGIN_URL = "/Login/Index";

        /// <summary>
        /// GPS 应用程序 Id 
        /// </summary>
        public const int GPS_APPLICATION_ID = 1001;

    }
}
