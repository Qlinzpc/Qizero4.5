
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
    public class UnitTestApplicationService
    {
        ApplicationService service = new ApplicationService();

        [TestMethod]
        public void TestAdd()
        {
            var req = new Request<Application>()
            {
                Obj = new Application()
                {
                    CreateDate = DateTime.Now,
                    Name = "通用权限系统",
                    Code = "GPS",
                    CreateUserId = 1,
                    CreateUserName = "张鹏程",
                    Remark = "通用权限系统 ( General Permissions System )"
                }
            };

            var res = service.Add(req);

            Assert.AreEqual(res.Status, 0);

        }

        [TestMethod]
        public void TestRemove()
        {

            var response = service.Remove(1005);

            Assert.AreEqual(response.Message, QConst.DELETE_SUCCESS);
        }

        [TestMethod]
        public void TestModify()
        {
            var response = service.GetById(1001);

            var app = response.Data;

            app.ModifyDate = DateTime.Now;
            app.ModifyUserId = 1;
            app.ModifyUserName = "zpc";
            var r = service.Modify(new Request<Application>
            {
                Obj = app
            });

            Assert.AreEqual(r.Data.ModifyUserName, "zpc");
        }

        [TestMethod]
        public void TestList()
        {
            var response = service.List();

            var list = response.Data;

            Assert.AreEqual(list.Count, 2);
        }
    }

}
