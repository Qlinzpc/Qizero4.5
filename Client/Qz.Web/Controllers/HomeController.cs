using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qz.Web.Core.Authorize;

namespace Qz.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [SsoAuthorize]
        public ActionResult Index()
        {

            return View();
        }

    }
}
