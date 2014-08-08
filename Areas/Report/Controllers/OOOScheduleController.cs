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
    public class OOOScheduleController : Controller
    {
        //
        // GET: /OOOSchedule/

        public ActionResult Index()
        {
            var ret = new ReportViewModel();

            System.Xml.Serialization.XmlSerializer reader =
                   new System.Xml.Serialization.XmlSerializer(typeof(OOOList));
            System.IO.StreamReader file = new System.IO.StreamReader(
                @"\\hpsjvlfs02\Data\SalesPortalPRODfiles\atlasNOC\TeamOOOSchedule.xml");


            XmlSerializer serializer = new XmlSerializer(typeof(OOOList));
            ret.OOOList = (OOOList)serializer.Deserialize(file);
            file.Close();

            return PartialView("~/Areas/Report/Views/OOOSchedule.cshtml", ret);
        }

    }
}
