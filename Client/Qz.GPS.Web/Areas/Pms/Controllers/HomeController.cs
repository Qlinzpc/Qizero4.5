using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qz.Core.Page;
using Qz.GPS.Web.Core.Authorize;

namespace Qz.GPS.Web.Areas.Pms.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Pms/Home/
        [SsoAuthorize]
        public ActionResult Index()
        {
            PageBase.Login("name", Guid.NewGuid().ToString());

            ViewBag.T = PageBase.GetValue("name");
            
            return View();
        }

    }
}
