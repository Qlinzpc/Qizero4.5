using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qz.Core.Page;
using Qz.GPS.Web.Core.Authorize;

namespace Qz.GPS.Web.Areas.Hr.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Hr/Home/
        [SsoAuthorize]
        public ActionResult Index()
        {
            ViewBag.T = PageBase.GetValue("name");
            return View();
        }

    }
}
