using System.Web.Mvc;

namespace Qz.GPS.Web.Areas.Es
{
    public class EsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Es";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Es_default",
                "Es/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Qz.GPS.Web.Areas.Es.Controllers" }
            );
        }
    }
}
