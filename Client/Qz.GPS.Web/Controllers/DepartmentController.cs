
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
    public class DepartmentController : BaseController
    {
        private DepartmentService departmentService = new DepartmentService();
        //
        // GET: /Department/

        public ActionResult Index()
        {
            var request = new Qz.Core.Entity.Request<Department>();

            var response = departmentService.List(request);

            return View(response);
        }
    }
}
