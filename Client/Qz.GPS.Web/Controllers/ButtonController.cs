
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
    using Qz.Core.Entity;

    [SysAuthorize]
    public class ButtonController : BaseController
    {
        private ButtonService buttonService = new ButtonService();

        //
        // GET: /Button/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int roleId = 0, int moduleId = -1)
        {
            ViewBag.Role = roleId;
            ViewBag.Module = moduleId;

            if (roleId == 0) moduleId = -1;

            ViewBag.Action = ((moduleId == -1 && roleId == 0) ? "SysModuleButton" : "RoleModuleButton");

            var response = buttonService.GetButtonByModule(new Request<Qz.GPS.DirectService.Parameter.Button.ByModule>()
            {
                Obj = new Qz.GPS.DirectService.Parameter.Button.ByModule()
                {
                    ModuleId = moduleId
                }
            });

            ViewBag.Buttons = response;
            
            return View();
        }

        [HttpPost]
        public JsonResult GetButton(int id = -1)
        {

            var response = buttonService.GetButtonByModule(new Request<Qz.GPS.DirectService.Parameter.Button.ByModule>()
            {
                Obj = new Qz.GPS.DirectService.Parameter.Button.ByModule()
                {
                    ModuleId = id
                }
            });

            return Json(response);
        }

    }
}
