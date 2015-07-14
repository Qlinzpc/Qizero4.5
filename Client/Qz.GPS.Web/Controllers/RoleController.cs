
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
    public class RoleController : BaseController
    {
        private RoleService service = new RoleService();
        private ModuleService moduleService = new ModuleService();
        //
        // GET: /Role/

        public ActionResult Index()
        {
            var request = new Qz.Core.Entity.Request<Role>();

            var response = service.List(request);

            return View(response);
        }

        /// <summary>
        /// 权限设置 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public ActionResult Permissions(int roleId)
        {

            ViewBag.ModuleTree = moduleService.GetModuleTree(new Qz.Core.Entity.Request<ViewModel.Tree>()
            {
                Obj = new ViewModel.Tree()
                {
                    ParentId = -1
                }
            });

            ViewBag.RoleModule = moduleService.GetModuleByRole(new Qz.Core.Entity.Request<Qz.GPS.DirectService.Parameter.Module.ByRole>()
            {
                Obj = new DirectService.Parameter.Module.ByRole()
                {
                    RoleId = roleId
                }
            });

            ViewBag.Role = roleId;

            return View();
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

        /// <summary>
        /// 根据 角色和模块 获得按钮数据 
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="moduleId">模块Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetButton(int roleId, int moduleId = -1)
        {
            var response = new ButtonService().GetButtonByModuleAndRole(new Qz.Core.Entity.Request<Qz.GPS.DirectService.Parameter.Button.ByModuleAndRole>()
            {
                Obj = new Qz.GPS.DirectService.Parameter.Button.ByModuleAndRole()
                {
                    ModuleId = moduleId,
                    RoleId = roleId
                }
            });

            return Json(response);
        }

        [HttpPost]
        public JsonResult Add(Role role)
        {
            var entity = new Role()
            {
                RoleName = role.RoleName,
                Remark = role.Remark,
                Enabled = role.Enabled,
                SortCode = role.SortCode,
                CreateUserId = SessionUser.Data.Id,
                CreateUserName = SessionUser.Data.UserName,
                CreateDate = DateTime.Now
            };

            var response = service.Add(new Qz.Core.Entity.Request<Role>
            {
                Obj = entity
            });

            return Json(response);
        }

        [HttpPost]
        public JsonResult Modify(Role role)
        {
            var response = new Qz.Core.Entity.Response<Role>();

            if (role == null || role.Id <= 0)
            {
                response.Status = 1;
                response.Message = "角色Id 为 0或不存在 !";
                return Json(response);
            }

            var entity = service.GetById(role.Id);
            if (entity.Status != 0)
            {
                response.Status = entity.Status;
                response.Message = entity.Message;
                return Json(response);
            }

            entity.Data.RoleName = role.RoleName;
            entity.Data.Remark = role.Remark;
            entity.Data.SortCode = role.SortCode;
            entity.Data.Enabled = role.Enabled;
            entity.Data.ModifyDate = DateTime.Now ;
            entity.Data.ModifyUserId = SessionUser.Data.Id;
            entity.Data.ModifyUserName = SessionUser.Data.UserName;

            response = service.Modify(new Qz.Core.Entity.Request<Role>()
            {
                Obj = entity.Data
            });

            return Json(response);
        }

    }
}
