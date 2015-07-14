
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

    [SysAuthorize]
    public class SysModuleController : BaseController
    {
        private ModuleService moduleService = new ModuleService();

        //
        // GET: /Module/
        public ActionResult Index()
        {
            var request = new Qz.Core.Entity.Request<Module>();

            var response = moduleService.List(request);

            return View(response);
        }

        public ActionResult Setting()
        {
            ViewBag.ModuleTree = moduleService.GetModuleTree(new Qz.Core.Entity.Request<ViewModel.Tree>()
            {
                Obj = new ViewModel.Tree()
                {
                    ParentId = -1
                }
            });

            return View();
        }

    }
}
