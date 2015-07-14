
namespace Qz.GPS.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Qz.GPS.Web.Core;
    using Qz.GPS.DirectService;
    using Qz.GPS.Models;

    using Qz.Common;
    using System.Threading.Tasks;

    public class UserController : BaseController
    {
        private UserService userService = new UserService();

        //
        // GET: /User/
        [SysAuthorize]
        public ActionResult Index()
        {
            var request = new Qz.Core.Entity.Request<User>();

            var response = userService.List(request);

            return View(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult Login(Qz.GPS.DirectService.Parameter.User.Login user)
        {
            UserService service = new UserService();

            var response = service.Login(new Qz.Core.Entity.Request<Qz.GPS.DirectService.Parameter.User.Login>()
            {
                Obj = user
            }, new LoginLog()
            {
                HostName = Request.UserHostName,
                HostIP = Request.UserHostAddress,
                LoginMsg = Request.UserAgent + " " + Request.UrlReferrer
            });

            if (response.Status == 0)
            {
                // 保存 Session 状态 
                SessionUser.Value(response.Data);

                var moduleResponse = new ModuleService().GetModuleByUser(new Qz.Core.Entity.Request<DirectService.Parameter.Module.ByUser>()
                {
                    Obj = new DirectService.Parameter.Module.ByUser()
                    {
                        ApplicationId = QConst.GPS_APPLICATION_ID,
                        ParentId = 0,
                        UserId = response.Data.Id
                    }
                });

                SessionModule.ListValue(moduleResponse.Data);
                SessionModule.JsonValue(Qz.Common.QJsonConvert.Serialize(moduleResponse.Data));
            }

            return Json(
                new Qz.Core.Entity.Response()
                {
                    Message = response.Message,
                    Times = response.Times,
                    Status = response.Status
                }
            );
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult Signout()
        {

            Session.Abandon();

            return Json(
               new Qz.Core.Entity.Response()
               {
                   Message = "已安全退出"
               }
           );
        }
    }
}
