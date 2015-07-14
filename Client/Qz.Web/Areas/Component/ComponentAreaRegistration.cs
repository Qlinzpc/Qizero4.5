using System.Web.Mvc;

namespace Qz.Web.Areas.Component
{
    public class ComponentAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Component";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Component_default",
                "Component/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
