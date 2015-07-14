
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
    using Qz.GPS.ViewModel;

    using Qz.Common;
    using System.Threading.Tasks;

    [SysAuthorize]
    public class ApplicationController : BaseController
    {

        private ApplicationService service = new ApplicationService();

        //
        // GET: /Application/

        public ActionResult Index()
        {
            var request = new Qz.Core.Entity.Request<Application>();

            var response = service.List(request);

            return View(response);
        }

        /// <summary>
        /// 编辑 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            if (id > 0)
            {
                ViewBag.Data = service.GetById(id);
            }

            return View();
        }


        [HttpPost]
        public JsonResult Add(Application app)
        {
            var entity = new Application()
            {
                Name = app.Name,
                Enabled = app.Enabled,
                Remark = app.Remark,
                SortCode = app.SortCode,
                Code = app.Code,
                CreateUserId = SessionUser.Data.Id,
                CreateUserName = SessionUser.Data.UserName,
                CreateDate = DateTime.Now
            };

            var response = service.Add(new Qz.Core.Entity.Request<Application>
            {
                Obj = entity
            });

            return Json(response);
        }

        [HttpPost]
        public JsonResult Modify(Application app)
        {
            var response = new Qz.Core.Entity.Response<Application>();

            if (app == null || app.Id <= 0)
            {
                response.Status = 1;
                response.Message = "系统应用Id 为 0或不存在 !";
                return Json(response);
            }

            var entity = service.GetById(app.Id);
            if (entity.Status != 0)
            {
                response.Status = entity.Status;
                response.Message = entity.Message;
                return Json(response);
            }

            entity.Data.Name = app.Name;
            entity.Data.Remark = app.Remark;
            entity.Data.SortCode = app.SortCode;
            entity.Data.Enabled = app.Enabled;
            entity.Data.ModifyDate = DateTime.Now;
            entity.Data.ModifyUserId = SessionUser.Data.Id;
            entity.Data.ModifyUserName = SessionUser.Data.UserName;

            response = service.Modify(new Qz.Core.Entity.Request<Application>()
            {
                Obj = entity.Data
            });

            return Json(response);
        }


    }
}
