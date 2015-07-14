
namespace Qz.Logging
{
    using Qz.Common;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Log
    {
        public static void Debug(string msg)
        {
            if (Config.IsDebug) Write(" [Debug] " + msg);
        }
        public static async Task DebugAsync(string msg)
        {
            if (Config.IsDebug) await WriteAsync(" [Debug] " + msg);
        }

        public static void Error(string msg)
        {
            Write(" [Error] " + msg);
        }
        public static async Task ErrorAsync(string msg)
        {
            await WriteAsync(" [Error] " + msg);
        }
        public static void Info(string msg)
        {
            Write(" [Info] " + msg);
        }

        public static async Task InfoAsync(string msg)
        {
            await WriteAsync(" [Info] " + msg);
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

        private static Task WriteAsync(string msg)
        {
            return Task.Run(() =>
            {
                Write(msg);
            });
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
