
namespace Qz.GPS.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Qz.GPS.Web.Core;
    using Qz.Common;

    public class QThemeController : Controller
    {
        //
        // GET: /QTheme/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Win7()
        {
            ViewBag.Module = QJsonConvert.Serialize(SessionModule.ListData.Where(x => x.SubMenu.Equals(0)).ToList());
            ViewBag.ModuleAll = SessionModule.JsonData;

            return View();
        }

        public ActionResult Win8()
        {
            ViewBag.Module = QJsonConvert.Serialize(SessionModule.ListData.Where(x => x.SubMenu.Equals(0)).ToList());
            ViewBag.ModuleAll = SessionModule.JsonData;

            return View();
        }
    }
}
