using System.Web.Mvc;

namespace Qz.GPS.Web.Areas.Qz
{
    public class QzAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Qz";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Qz_default",
                "Qz/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
