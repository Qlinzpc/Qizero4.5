
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
    public class SysLogController : BaseController
    {
        private SysLogService sysLogService = new SysLogService();
        //
        // GET: /SysLog/

        public ActionResult Index()
        {
            var request =new Qz.Core.Entity.Request<SysLog>();

            var response = sysLogService.List(request);
            
            var responseNew = sysLogService.ListNew(request);

            return View(response);
        }

    }
}
