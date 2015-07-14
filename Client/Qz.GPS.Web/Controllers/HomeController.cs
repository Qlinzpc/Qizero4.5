
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
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            ViewBag.Module = SessionModule.JsonData;

            return View();
        }

    }
}
