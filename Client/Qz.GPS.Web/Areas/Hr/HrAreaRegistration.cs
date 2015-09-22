using System.Web.Mvc;

namespace Qz.GPS.Web.Areas.Hr
{
    public class HrAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Hr";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Hr_default",
                "Hr/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
