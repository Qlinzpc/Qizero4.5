using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Security;
using System.IO;
using System.Security.Cryptography;

namespace Zephyr.Utils
{
    public partial class ZEncypt
    {
        #region MD5函数
        /// <summary>
        /// MD5函数,需引用：using System.Security.Cryptography;
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>MD5结果</returns>
        public static string MD5(string str)
        {
            //微软md5方法参考return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5");
            byte[] b = Encoding.Default.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');
            return ret;
        }
        #endregion

        #region 使用散列方式加密 MD5加密

        /// <summary>
        /// 使用MD5对字符串进行加密
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5Encrypts(string str)
        {
            string result = string.Empty;
            //先将要加密的字符串转换成byte数组
            byte[] inputData = System.Text.Encoding.Default.GetBytes(str);
            //在通过MD5类加密，并得到加密后的byte[]类型
            byte[] data = System.Security.Cryptography.MD5.Create().ComputeHash(inputData);

            StringBuilder strBuild = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                //将每个byte数据转换成16进制。"X":表示大写16进制；"X2"：表示大写16进制保留2位；"x"：表示小写16进制
                strBuild.Append(data[i].ToString("X2"));
            }
            result = strBuild.ToString();
            return result;
        }

        #endregion
    }
}
