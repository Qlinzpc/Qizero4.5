
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
    public class UnitTestCompanyService
    {
        CompanyService service = new CompanyService();

        [TestMethod]
        public void TestAdd()
        {
            var request = new Request<Company>()
            {
                Obj = new Company()
                {
                    Category = "",
                    Code = "100",
                    Contact = "Qz",
                    CreateDate = DateTime.Now,
                    CreateUserId = 1,
                    CreateUserName = "张鹏程",
                    FullName = "Qz",
                    Manager = "Qz",
                    Nature = "",
                    ParentId = 0,
                    Phone = "15012940312",
                    Remark = "",
                    ShortName = "Qz"
                }
            };

            new SysLogService().Add(new Request<SysLog>()
            {
                Obj = new SysLog()
                {
                    CreateDate = DateTime.Now,
                    Location = "Test/CompanyService",
                    Action = "TestAdd",
                    Message = QJsonConvert.Serialize(request),
                    Type = "添加",
                    UserId = 1,
                    UserName = "张鹏程"
                }
            });

            var response = service.Add(request);

            Assert.AreEqual(response.Status, 0);

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
            var r = service.Modify(new Request<Company>
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
