using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Qz.Common;

namespace Qz.GPS.Web.Core.Authorize
{
    public class SsoAuthorize : AuthorizeAttribute
    {
        /// <summary>
        /// 验证授权
        /// </summary>
        /// <param name="httpContext">HTTP 上下文，它封装有关单个 HTTP 请求的所有 HTTP 特定的信息</param>
        /// <returns>如果用户已经过授权，则为 true；否则为 false</returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null) return false;
            if (httpContext.Session != null && (httpContext.Session.Count > 0 || httpContext.Request.QueryString.Keys.Count > 0))
            {
                httpContext.Session["token"] = httpContext.Request.QueryString["token"];
                return true;

                //var user = SessionUser.Data;
                //if (user != null && user.Id != 0)
                //{
                //    return true;
                //}
            }

            httpContext.Response.StatusCode = 403;
            return false;
        }

        /// <summary>
        /// 请求授权时调用 重写验证
        /// </summary>
        /// <param name="filterContext">验证信息上下文。</param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.HttpContext.Response.StatusCode == 403)
            {
                var url = filterContext.HttpContext.Request.UrlReferrer;
                var returnUrl = QConst.HOME_URL;
                if (url != null)
                {
                    returnUrl = url.ToString();
                }

                filterContext.Result = new RedirectResult(QConst.LOGIN_URL + "?returnUrl=" + HttpUtility.UrlEncode(returnUrl));
            }
        }
    }
}