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
        #region MD5����
        /// <summary>
        /// MD5����,�����ã�using System.Security.Cryptography;
        /// </summary>
        /// <param name="str">ԭʼ�ַ���</param>
        /// <returns>MD5���</returns>
        public static string MD5(string str)
        {
            //΢��md5�����ο�return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5");
            byte[] b = Encoding.Default.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');
            return ret;
        }
        #endregion

        #region ʹ��ɢ�з�ʽ���� MD5����

        /// <summary>
        /// ʹ��MD5���ַ������м���
        /// </summary>
        /// <param name="str">��Ҫ���ܵ��ַ���</param>
        /// <returns>���ܺ���ַ���</returns>
        public static string MD5Encrypts(string str)
        {
            string result = string.Empty;
            //�Ƚ�Ҫ���ܵ��ַ���ת����byte����
            byte[] inputData = System.Text.Encoding.Default.GetBytes(str);
            //��ͨ��MD5����ܣ����õ����ܺ��byte[]����
            byte[] data = System.Security.Cryptography.MD5.Create().ComputeHash(inputData);

            StringBuilder strBuild = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                //��ÿ��byte����ת����16���ơ�"X":��ʾ��д16���ƣ�"X2"����ʾ��д16���Ʊ���2λ��"x"����ʾСд16����
                strBuild.Append(data[i].ToString("X2"));
            }
            result = strBuild.ToString();
            return result;
        }

        #endregion
    }
}
