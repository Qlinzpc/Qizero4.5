
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
    public class SysModuleButtonController : BaseController
    {
        private ModuleButtonMapService service = new ModuleButtonMapService();

        //
        // GET: /SysModuleButton/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Add(int moduleId, string buttonIds)
        {
            var response = service.Add(new Qz.Core.Entity.Request<Qz.GPS.DirectService.Parameter.ModuleButton.Add>
            {
                Obj = new DirectService.Parameter.ModuleButton.Add()
                {
                    ModuleId = moduleId,
                    ButtonIds = buttonIds
                }
            });

            return Json(response);
        }

        [HttpPost]
        public JsonResult Remove(int moduleId, string buttonIds)
        {
            var response = service.Remove(new Qz.Core.Entity.Request<Qz.GPS.DirectService.Parameter.ModuleButton.Remove>
            {
                Obj = new DirectService.Parameter.ModuleButton.Remove()
                {
                    ModuleId = moduleId,
                    ButtonIds = buttonIds
                }
            });

            return Json(response);
        }
    }
}
