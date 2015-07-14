
namespace Qz.GPS.Web.Core
{
    using Qz.GPS.DirectService;
    using Qz.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class BaseController : Controller
    {
        protected int ModuleId { get; set; }
        protected string PageId { get; set; }

        protected override IAsyncResult BeginExecute(System.Web.Routing.RequestContext requestContext, AsyncCallback callback, object state)
        {
            return base.BeginExecute(requestContext, callback, state);
        }

        protected override void EndExecute(IAsyncResult asyncResult)
        {
            base.EndExecute(asyncResult);
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            return base.BeginExecuteCore(callback, state);
        }

        protected override void EndExecuteCore(IAsyncResult asyncResult)
        {
            base.EndExecuteCore(asyncResult);
        }

        protected override void Execute(System.Web.Routing.RequestContext requestContext)
        {
            base.Execute(requestContext);
        }

        protected override void ExecuteCore()
        {
            base.ExecuteCore();
        }

        protected override HttpNotFoundResult HttpNotFound(string statusDescription)
        {
            return base.HttpNotFound(statusDescription);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var action = filterContext.ActionDescriptor.ActionName;
            this.ModuleId = 0;
            var arr = filterContext.Controller.ToString().Split('.');
            var controller = arr[arr.Length - 1].Replace("Controller", "");

            if (action == "Index")
            {
                if (SessionModule.ListData.Count > 0)
                {

                    this.PageId = controller.Substring(0, 1).ToLower() + controller.Substring(1);

                    var module = SessionModule.ListData.Where(x => !string.IsNullOrEmpty(x.URL) && ("/" + controller).Equals(x.URL)).FirstOrDefault() ?? new ViewModel.Module();
                    this.ModuleId = module.Id;

                    // 验证是否具有访问权限 
                    if (this.ModuleId == 0)
                    {
                        this.ModuleId = -1;
                    }

                }
            }
            else
            {
                this.PageId = controller.Substring(0, 1).ToLower() + controller.Substring(1) + action;
            }

            if (this.ModuleId < 0)
            {
                return;
            }

            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // 若没有访问权限 , 则 return 
            if (this.ModuleId < 0)
            {
                return;

            }

            base.OnActionExecuted(filterContext);
        }

        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (this.ModuleId != 0)
            {
                ViewBag.Buttons = new ButtonService().GetButtonByModuleAndRole(new Qz.Core.Entity.Request<DirectService.Parameter.Button.ByModuleAndRole>()
                {
                    Obj = new DirectService.Parameter.Button.ByModuleAndRole()
                    {
                        ModuleId = this.ModuleId,
                        RoleId = SessionUser.Data.RoleId
                    }
                });

            }

            ViewBag.PageId = this.PageId;

            base.OnResultExecuting(filterContext);
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            Log.ErrorAsync(filterContext.Controller.ToString() + " " + filterContext.HttpContext.Request.Url.ToString() + " msg: " + filterContext.Exception.Message);

            new SysLogService().Add(new Qz.Core.Entity.Request<Models.SysLog>()
            {
                Obj = new Models.SysLog()
                {
                    Action = filterContext.HttpContext.Request.Url.ToString(),
                    CreateDate = DateTime.Now,
                    Location = filterContext.Controller.ToString(),
                    Message = filterContext.Exception.Message,
                    Type = "Error",
                    UserId = SessionUser.Data.Id,
                    UserName = SessionUser.Data.UserName
                }
            });

            base.OnException(filterContext);
        }
    }
}
