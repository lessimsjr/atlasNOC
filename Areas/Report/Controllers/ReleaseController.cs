using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using atlasNOC.Models;
using atlasNOC.Areas.Report.Models;

namespace atlasNOC.Areas.Report.Controllers
{
    public class ReleaseController : Controller
    {
        //
        // GET: /Release/

        public ActionResult Index()
        {
            var ret = new ReportViewModel();

            System.Xml.Serialization.XmlSerializer reader =
                   new System.Xml.Serialization.XmlSerializer(typeof(releaseList));
            System.IO.StreamReader file = new System.IO.StreamReader(
                @"\\hpsjvlfs02\Data\SalesPortalPRODfiles\atlasNOC\upcomingreleases.xml");
                //@"c:\Projects\Development\atlasNOC\atlasNOC\atlasNOC\urls.xml");
            

            XmlSerializer serializer = new XmlSerializer(typeof(releaseList));
            ret.releaseList = (releaseList)serializer.Deserialize(file);
            file.Close();

            return PartialView("~/Areas/Report/Views/Release.cshtml", ret);
        }

    }
}
