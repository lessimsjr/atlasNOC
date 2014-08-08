using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using atlasNOC.Models;

namespace atlasNOC.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var ret = new atlasNOC.Models.HomeIndex();
            //ret.urls = new List<urlList>();
            XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(urlList));
            using (System.IO.StreamReader file = new System.IO.StreamReader(
                @"\\hpsjvlfs02\Data\SalesPortalPRODfiles\atlasNOC\urls.xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(urlList));
                ret.urlList = (urlList)serializer.Deserialize(file);
                return PartialView("~/Views/Home/Index.cshtml", ret);
            }
        }
    }
}
