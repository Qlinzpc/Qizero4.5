
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
    public class DataCodeController : BaseController
    {

        private DataCodeService dataCodeService = new DataCodeService();
        //
        // GET: /DataCode/

        public ActionResult Index()
        {
            var request = new Qz.Core.Entity.Request<DataCode>();

            var response = dataCodeService.List(request);

            return View(response);
        }

    }
}
