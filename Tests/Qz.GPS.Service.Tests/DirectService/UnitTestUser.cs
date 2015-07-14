
namespace Qz.GPS.Service.Tests.DirectService
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Data.SqlClient;

    using Qz.Core.Entity;
    using Qz.GPS.DirectService;
    using Qz.GPS.Models;
    using Qz.GPS;
    using Qz.Common;

    [TestClass]
    public class UnitTestUser
    {
        private UserService user = new UserService();

        [TestMethod]
        public void TestAdd()
        {
            var request = new Request<User>()
            {
                Obj = new User()
                {
                    Account = "Qz",
                    Birthday = "1993-07-17",
                    Code = "10002",
                    CompanyId = 100,
                    CreateDate = DateTime.Now,
                    CreateUserId = 1,
                    CreateUserName = "张鹏程",
                    DepartmentId = 1001,
                    Gender = "男",
                    Mobile = "15012940312",
                    Password = "Qz100",
                    Secretkey = Guid.NewGuid().ToString(),
                    Spell = "qz",
                    Telephone = "",
                    UserName = "Qz"
                }
            };

            new SysLogService().Add(new Request<SysLog>()
            {
                Obj = new SysLog()
                {
                    CreateDate = DateTime.Now,
                    Location = "Test/UserService",
                    Action = "TestAdd",
                    Message = QJsonConvert.Serialize(request),
                    Type = "添加",
                    UserId = 1,
                    UserName = "张鹏程"
                }
            });

            var response = user.Add(request);

            var timers = response.Times;

            Assert.AreEqual(response.Data.CreateUserId, 1);
        }

    }
}
