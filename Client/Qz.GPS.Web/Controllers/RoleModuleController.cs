
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
    public class RoleModuleController : BaseController
    {
        private RoleModuleMapService service = new RoleModuleMapService();
        //
        // GET: /RoleModule/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Add(int roleId, string moduleIds)
        {
            var response = service.Add(new Qz.Core.Entity.Request<Qz.GPS.DirectService.Parameter.RoleModule.Add>
            {
                Obj = new DirectService.Parameter.RoleModule.Add()
                {
                    RoleId = roleId,
                    ModuleIds = moduleIds
                }
            });

            return Json(response);
        }

        [HttpPost]
        public JsonResult Remove(int roleId, string moduleIds)
        {
            var response = service.Remove(new Qz.Core.Entity.Request<Qz.GPS.DirectService.Parameter.RoleModule.Remove>
            {
                Obj = new DirectService.Parameter.RoleModule.Remove()
                {
                    RoleId = roleId,
                    ModuleIds = moduleIds
                }
            });

            return Json(response);
        }

    }
}
