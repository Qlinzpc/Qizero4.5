
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
    public class UnitTestModuleService
    {
        ModuleService service = new ModuleService();

        [TestMethod]
        public void TestAdd()
        {
            var request = new Request<Module>()
            {
                Obj = new Module()
                {
                    Name = "系统管理",
                    ParentId = 0,
                    ApplicationId = 1001,
                    Icon = "sys-manage",
                    IconURL = "",
                    URL = "/GPS/SysManage",
                    CreateDate = DateTime.Now,
                    CreateUserId = 1,
                    CreateUserName = "张鹏程",
                    Remark = ""
                }
            };

            new SysLogService().Add(new Request<SysLog>()
            {
                Obj = new SysLog()
                {
                    CreateDate = DateTime.Now,
                    Location = "Test/ModuleService",
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
            var request = new Request<List<Module>>()
            {
                Obj = new List<Module>()
                {
                    new Module()
                    {
                        Name = "系统应用",
                        ParentId = 1,
                        ApplicationId = 1001,
                        Icon = "sys-app",
                        IconURL = "",
                        URL = "/GPS/SysApplication",
                        CreateDate = DateTime.Now,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        Remark = ""
                    },
                    new Module()
                    {
                        Name = "系统日志",
                        ParentId = 1,
                        ApplicationId = 1001,
                        Icon = "sys-log",
                        IconURL = "",
                        URL = "/GPS/SysLog",
                        CreateDate = DateTime.Now,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        Remark = ""
                    },
                    new Module()
                    {
                        Name = "数据字典",
                        ParentId = 1,
                        ApplicationId = 1001,
                        Icon = "data-code",
                        IconURL = "",
                        URL = "/GPS/DataCode",
                        CreateDate = DateTime.Now,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        Remark = ""
                    },
                    new Module()
                    {
                        Name = "数据库管理",
                        ParentId = 1,
                        ApplicationId = 1001,
                        Icon = "db-manage",
                        IconURL = "",
                        URL = "/GPS/DBManager",
                        CreateDate = DateTime.Now,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        Remark = ""
                    },
                    new Module()
                    {
                        Name = "系统设置",
                        ParentId = 0,
                        ApplicationId = 1001,
                        Icon = "sys-setting",
                        IconURL = "",
                        URL = "/GPS/SysSetting",
                        CreateDate = DateTime.Now,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        Remark = ""
                    },
                    new Module()
                    {
                        Name = "模块管理",
                        ParentId = 6,
                        ApplicationId = 1001,
                        Icon = "module-manage",
                        IconURL = "",
                        URL = "/GPS/ModuleManage",
                        CreateDate = DateTime.Now,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        Remark = ""
                    },
                    new Module()
                    {
                        Name = "公司管理",
                        ParentId = 6,
                        ApplicationId = 1001,
                        Icon = "company-manage",
                        IconURL = "",
                        URL = "/GPS/CompanyManage",
                        CreateDate = DateTime.Now,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        Remark = ""
                    },
                    new Module()
                    {
                        Name = "部门管理",
                        ParentId = 6,
                        ApplicationId = 1001,
                        Icon = "dept-manage",
                        IconURL = "",
                        URL = "/GPS/DeptManage",
                        CreateDate = DateTime.Now,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        Remark = ""
                    },
                    new Module()
                    {
                        Name = "角色管理",
                        ParentId = 6,
                        ApplicationId = 1001,
                        Icon = "role-manage",
                        IconURL = "",
                        URL = "/GPS/SysSetting",
                        CreateDate = DateTime.Now,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        Remark = ""
                    },
                    new Module()
                    {
                        Name = "用户管理",
                        ParentId = 6,
                        ApplicationId = 1001,
                        Icon = "user-manage",
                        IconURL = "",
                        URL = "/GPS/UserManager",
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
                    Location = "Test/ModuleService",
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
            var response = service.GetById(1);

            var entity = response.Data;

            entity.ModifyDate = DateTime.Now;
            entity.ModifyUserId = 1;
            entity.ModifyUserName = "张鹏程";

            entity.Icon = "manage";
            entity.URL = "/GPS/Manage";
            
            var r = service.Modify(new Request<Module>
            {
                Obj = entity
            });

            new SysLogService().Add(new Request<SysLog>()
            {
                Obj = new SysLog()
                {
                    CreateDate = DateTime.Now,
                    Location = "Test/ModuleService",
                    Action = "TestModify",
                    Message = QJsonConvert.Serialize(entity),
                    Type = "修改",
                    UserId = 1,
                    UserName = "张鹏程"
                }
            });

            Assert.AreEqual(r.Data.ModifyUserName, "张鹏程");
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
