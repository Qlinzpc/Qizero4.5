using System.Web.Mvc;

namespace Qz.Web.Areas.ES
{
    public class ESAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ES";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ES_default",
                "ES/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
