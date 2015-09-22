using System.Web.Mvc;

namespace Qz.GPS.Web.Areas.Pms
{
    public class PmsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Pms";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Pms_default",
                "Pms/{controller}/{action}/{id}",
                new { controller="Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
