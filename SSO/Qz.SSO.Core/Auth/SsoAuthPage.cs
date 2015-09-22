using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Qz.SSO.Core.Auth
{
    public class SsoAuthPage : Page
    {
        public string Token { get; set; }

        public void AuthInit()
        {
            if (HttpContext.Current.Session != null &&
                (HttpContext.Current.Session.Count > 0 || HttpContext.Current.Request.QueryString.Keys.Count > 0))
            {
                if (HttpContext.Current.Session.Count <= 0)
                {
                    HttpContext.Current.Session["token"] = HttpContext.Current.Request.QueryString["token"];
                }

                Token = HttpContext.Current.Request.QueryString["token"];
            }
            else
            {
                var url = HttpContext.Current.Request.UrlReferrer;
                var returnUrl = HttpContext.Current.Request.Url.ToString();
                if (url != null)
                {
                    returnUrl = url.ToString();
                }

                HttpContext.Current.Response.Redirect("http://localhost:1595/Login/Index?returnUrl=" + HttpUtility.UrlEncode(returnUrl));
            }
        }

        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e);
        }
    }
}
