
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
    using System.Collections;
    using System.Collections.Generic;

    [TestClass]
    public class UnitTestDepartmentService
    {
        DepartmentService service = new DepartmentService();

        [TestMethod]
        public void TestAdd()
        {
            var request = new Request<Department>()
            {
                Obj = new Department()
                {
                    Code = "1001",
                    CompanyId = 100,
                    FullName = "营业东部",
                    ParentId = 1000,
                    CreateUserId = 1,
                    CreateUserName = "张鹏程",
                    CreateDate = DateTime.Now
                }
            };

            new SysLogService().Add(new Request<SysLog>()
            {
                Obj = new SysLog()
                {
                    CreateDate = DateTime.Now,
                    Location = "Test/DepartmentService",
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
        public void TestAddRange()
        {
            var request = new Request<List<Department>>()
            {
                Obj = new List<Department>()
                {
                    new Department()
                    {
                        Code = "1002",
                        CompanyId = 100,
                        FullName = "营业中部",
                        ParentId = 1000,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        CreateDate = DateTime.Now
                    },  
                    new Department()
                    {
                        Code = "1003",
                        CompanyId = 100,
                        FullName = "营业西部",
                        ParentId = 1000,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        CreateDate = DateTime.Now
                    }
                }
            };

            new SysLogService().Add(new Request<SysLog>()
            {
                Obj = new SysLog()
                {
                    CreateDate = DateTime.Now,
                    Location = "Test/DepartmentService",
                    Action = "TestAddRange",
                    Message = QJsonConvert.Serialize(request),
                    Type = "批量添加",
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
            var r = service.Modify(new Request<Department>
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
