using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qz.Core.Page;
using Qz.GPS.Web.Core.Authorize;

namespace Qz.GPS.Web.Areas.Qz.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Qz/Home/
        [SsoAuthorize]
        public ActionResult Index()
        {
            PageBase.Login("name", Guid.NewGuid().ToString());
            return View();
        }

    }
}
