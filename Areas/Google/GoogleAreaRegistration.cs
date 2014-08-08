using System.Web.Mvc;

namespace atlasNOC.Areas.Google
{
    public class GoogleAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Google";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Google_default",
                "Google/{controller}/{action}",
                new { action = "Index" }
            );
        }
    }
}
