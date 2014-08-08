using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using atlasNOC.Google.Controls;

namespace atlasNOC.Areas.Google.Controllers
{
    public class AnalyticsController : Controller
    {
        //
        // GET: /Google/Analytics/

        public ActionResult Index()
        {
            var gaReport = new report();
            return View();
        }

    }
}
