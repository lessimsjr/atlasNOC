using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace atlasNOC.Areas.Report.Models
{
    public class ReportViewModel
    {
        public atlasNOC.Models.releaseList releaseList { get; set; }
        public atlasNOC.Models.OOOList OOOList { get; set; }
    }
}