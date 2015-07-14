
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
    public class UnitTestButtonService
    {
        ButtonService service = new ButtonService();

        [TestMethod]
        public void TestAdd()
        {
            var request = new Request<Button>()
            {
                Obj = new Button()
                {
                    Name = "新增",
                    Value = "add",
                    Icon = "icon-add",
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
                    Location = "Test/ButtonService",
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
            var request = new Request<List<Button>>()
            {
                Obj = new List<Button>()
                {
                    new Button()
                    {
                        Name = "删除",
                        Value = "remove",
                        Icon = "icon-remove",
                        CreateDate = DateTime.Now,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        Remark = ""
                    },
                    new Button()
                    {
                        Name = "修改",
                        Value = "modify",
                        Icon = "icon-modify",
                        CreateDate = DateTime.Now,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        Remark = ""
                    },
                    new Button()
                    {
                        Name = "编辑",
                        Value = "edit",
                        Icon = "icon-edit",
                        CreateDate = DateTime.Now,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        Remark = ""
                    }
                    ,
                    new Button()
                    {
                        Name = "刷新",
                        Value = "reload",
                        Icon = "icon-reload",
                        CreateDate = DateTime.Now,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        Remark = ""
                    },
                    new Button()
                    {
                        Name = "保存",
                        Value = "save",
                        Icon = "icon-save",
                        CreateDate = DateTime.Now,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        Remark = ""
                    },
                    new Button()
                    {
                        Name = "查询",
                        Value = "search",
                        Icon = "icon-search",
                        CreateDate = DateTime.Now,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        Remark = ""
                    },
                    new Button()
                    {
                        Name = "导入",
                        Value = "import",
                        Icon = "icon-import",
                        CreateDate = DateTime.Now,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        Remark = ""
                    },
                    new Button()
                    {
                        Name = "导出",
                        Value = "export",
                        Icon = "icon-export",
                        CreateDate = DateTime.Now,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        Remark = ""
                    },
                    new Button()
                    {
                        Name = "打印",
                        Value = "print",
                        Icon = "icon-print",
                        CreateDate = DateTime.Now,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        Remark = ""
                    },
                    new Button()
                    {
                        Name = "复制",
                        Value = "copy",
                        Icon = "icon-copy",
                        CreateDate = DateTime.Now,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        Remark = ""
                    },
                    new Button()
                    {
                        Name = "下载",
                        Value = "download",
                        Icon = "icon-download",
                        CreateDate = DateTime.Now,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        Remark = ""
                    },
                    new Button()
                    {
                        Name = "预览",
                        Value = "preview",
                        Icon = "icon-preview",
                        CreateDate = DateTime.Now,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        Remark = ""
                    },
                    new Button()
                    {
                        Name = "浏览",
                        Value = "browser",
                        Icon = "icon-browser",
                        CreateDate = DateTime.Now,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        Remark = ""
                    },
                    new Button()
                    {
                        Name = "审核",
                        Value = "audit",
                        Icon = "icon-audit",
                        CreateDate = DateTime.Now,
                        CreateUserId = 1,
                        CreateUserName = "张鹏程",
                        Remark = ""
                    },
                    new Button()
                    {
                        Name = "授权",
                        Value = "CC",
                        Icon = "icon-CC",
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
                    Location = "Test/ButtonService",
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
            var r = service.Modify(new Request<Button>
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
