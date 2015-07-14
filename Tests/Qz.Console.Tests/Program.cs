
namespace Qz.Console.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Qz.Common;
    using Qz.GPS.Models;

    class Program
    {
        static void Main(string[] args)
        {
            #region 异步
            {
                var num1 = 0.0;
                var num2 = 0.0;
                var begin = DateTime.Now;

                Action action = (() =>
                {
                    Console.WriteLine("begin action async . ");
                    begin = DateTime.Now;
                    for (int i = 0; i < 1000000; i++)
                    {
                        num1 = num1 / num2;
                    }

                    Console.WriteLine("end action async . " + begin.Interval());
                });

                Func<double> func = (() =>
                {
                    Console.WriteLine("begin func async . ");
                    begin = DateTime.Now;

                    for (int i = 0; i < 100000000; i++)
                    {
                        num1 = num1 / num2;
                    }

                    Console.WriteLine("end func async . " + begin.Interval());

                    return num1;
                });

                action.RunAsync(() =>
                {

                    Console.WriteLine("begin callback . ");
                });

                func.RunAsync((rs) =>
                {
                    Console.WriteLine("Result: " + rs);
                });

                Console.WriteLine("begin sync . ");
            }
            #endregion

            #region 加密 解密
            {
                Guid guid = Guid.NewGuid();

                Console.WriteLine(guid.ToString());

                var s = guid.ToString();
                Console.WriteLine(s.Substring(26, 8));
                var encypt = QSecurity.DESEncrypt("Qz110", "security");
                Console.WriteLine(encypt);
                // Console.WriteLine(s.Substring(26, 8));
                try
                {
                    Console.WriteLine("解密: " + QSecurity.DESDecrypt(encypt, "security"));
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("不正确的数据"))
                    {
                        Console.WriteLine("解密密钥错误 !");
                    }
                }

            }
            #endregion

            #region JSON
            {
                var dept = new Department()
                {
                    Code = "1000",
                    CompanyId = 100,
                    FullName = "信息技术部",
                    ParentId = 100,
                    CreateUserId = 1,
                    CreateUserName = "张鹏程",
                    CreateDate = DateTime.Now
                };

                var serialize = QJsonConvert.Serialize(dept);

                var deserialize = QJsonConvert.Deserialize<Department>(serialize);

                Console.WriteLine("\n====================");
                Console.WriteLine(serialize);

                Console.WriteLine("\n====================");
                Console.WriteLine(deserialize.CreateDate);
            }
            #endregion

            Console.Read();
        }

    }
}
