
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
    public class RoleModuleButtonController : BaseController
    {
        private RoleModuleButtonMapService service = new RoleModuleButtonMapService();

        //
        // GET: /RoleModuleButton/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Add(int roleId,int moduleId, string buttonIds)
        {
            var response = service.Add(new Qz.Core.Entity.Request<Qz.GPS.DirectService.Parameter.RoleModuleButton.Add>
            {
                Obj = new DirectService.Parameter.RoleModuleButton.Add()
                {
                    RoleId = roleId,
                    ModuleId = moduleId,
                    ButtonIds = buttonIds
                }
            });

            return Json(response);
        }

        [HttpPost]
        public JsonResult Remove(int roleId,int moduleId, string buttonIds)
        {
            var response = service.Remove(new Qz.Core.Entity.Request<Qz.GPS.DirectService.Parameter.RoleModuleButton.Remove>
            {
                Obj = new DirectService.Parameter.RoleModuleButton.Remove()
                {
                    RoleId = roleId,
                    ModuleId = moduleId,
                    ButtonIds = buttonIds
                }
            });

            return Json(response);
        }

    }
}
