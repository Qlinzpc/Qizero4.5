using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
using System.Text;

namespace Common
{
    public class Log
    {
        public static void Debug(string msg)
        {
            Write(" [Debug] " + msg);
        }

        public static void Error(string msg)
        {
            Write(" [Error] " + msg);
        }

        public static void Info(string msg)
        {
            Write(" [Info] " + msg);
        }

        private static void Write(string msg)
        {
            var root = AppDomain.CurrentDomain.BaseDirectory;
            var fileName = DateTime.Now.ToString("yyyyMMdd");
            var filePath = root + "log\\" + fileName + ".txt";

            CreateDirectory(root + "log\\");  // 检查文件目录是否存在 

            using (StreamWriter w = new StreamWriter(filePath, true, Encoding.Default))
            {
                w.WriteLine(DateTime.Now.ToString() + msg + "\r\n");
                w.Flush();
            }
        }

        /// <summary>
        /// 文件目录是否存在，不存在则新建
        /// </summary>
        /// <returns></returns>
        private static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}