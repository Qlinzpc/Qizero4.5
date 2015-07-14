
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
    using System.Collections.Generic;

    [TestClass]
    public class UnitTestRoleService
    {
        RoleService service = new RoleService();

        [TestMethod]
        public void TestAdd()
        {
            var request = new Request<Role>()
            {
                Obj = new Role()
                {
                    RoleName = "超级管理员",
                    CreateDate = DateTime.Now,
                    CreateUserId = 1,
                    CreateUserName = "张鹏程",
                    Remark = "超级管理员, 拥有系统所有权限"
                }
            };

            new SysLogService().Add(new Request<SysLog>()
            {
                Obj = new SysLog()
                {
                    CreateDate = DateTime.Now,
                    Location = "Test/RoleService",
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
            //var request = new Request<List<Role>>()
            //{
            //    Obj = new List<Role>()
            //    {
            //        new Role()
            //        {
            //            RoleName = "营运总监",
            //            CreateDate = DateTime.Now,
            //            CreateUserId = 1,
            //            CreateUserName = "张鹏程",
            //            Remark = ""
            //        },
            //        new Role()
            //        {
            //            RoleName = "分行经理",
            //            CreateDate = DateTime.Now,
            //            CreateUserId = 1,
            //            CreateUserName = "张鹏程",
            //            Remark = ""
            //        },
            //        new Role()
            //        {
            //            RoleName = "置业顾问",
            //            CreateDate = DateTime.Now,
            //            CreateUserId = 1,
            //            CreateUserName = "张鹏程",
            //            Remark = ""
            //        },
            //        new Role()
            //        {
            //            RoleName = "项目经理",
            //            CreateDate = DateTime.Now,
            //            CreateUserId = 1,
            //            CreateUserName = "张鹏程",
            //            Remark = ""
            //        },
            //        new Role()
            //        {
            //            RoleName = "技术经理",
            //            CreateDate = DateTime.Now,
            //            CreateUserId = 1,
            //            CreateUserName = "张鹏程",
            //            Remark = ""
            //        }
            //    }
            //};

            var request = new Request<List<Role>>()
            {
                Obj = new List<Role>()
                {
                    new Role()
                    {
                        RoleName = "测试角色",
                        CreateDate = DateTime.Now,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        Remark = ""
                    },
                    new Role()
                    {
                        RoleName = "访客角色",
                        CreateDate = DateTime.Now,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        Remark = ""
                    }
                }
            };

            new SysLogService().Add(new Request<SysLog>()
            {
                Obj = new SysLog()
                {
                    CreateDate = DateTime.Now,
                    Location = "Test/RoleService",
                    Action = "TestAddRange",
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
            var r = service.Modify(new Request<Role>
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
