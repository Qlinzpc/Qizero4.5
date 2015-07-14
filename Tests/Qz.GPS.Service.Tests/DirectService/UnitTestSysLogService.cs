
namespace Qz.GPS.Service.Tests.DirectService
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Data.SqlClient;

    using Qz.Core.Entity;
    using Qz.GPS.DirectService;
    using Qz.GPS.Models;
    using Qz.GPS;

    [TestClass]
    public class UnitTestSysLogService
    {
        SysLogService log = new SysLogService();

        [TestMethod]
        public void TestAdd()
        {
            var response = log.Add(new Request<SysLog>()
            {
                Obj = new SysLog()
                {
                    CreateDate = DateTime.Now,
                    Action = "TestAdd",
                    Location = "Test/SysLogService",
                    Message = "测试 SysLogService Add 方法 !",
                    Type = "添加",
                    UserId = 1,
                    UserName = "张鹏程"
                }
            });

            var times = response.Times;

            Assert.AreEqual(response.Data.UserId, 1);

        }

        [TestMethod]
        public void TestList()
        {
            var response = log.Add(new Request<SysLog>()
            {
                Obj = new SysLog()
                {
                    CreateDate = DateTime.Now,
                    Action = "TestList",
                    Location = "Test/SysLogService",
                    Message = "测试 SysLogService List 方法 !",
                    Type = "查询",
                    UserId = 1,
                    UserName = "张鹏程"
                }
            });

            var times = response.Times;

            Assert.AreEqual(response.Data.UserId, 1);

            var res = log.List();

            times = res.Times;

            Assert.AreEqual(res.Data.Count, 2);
        }

        [TestMethod]
        public void TestGetById()
        {
            var response = log.GetById(1);

            var times = response.Times;

            Assert.AreEqual(response.Data.UserId, 1);
        }

    }
}
;