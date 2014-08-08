using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoogleAnalytics;
using atlasNOC.Google.Controls;

namespace atlasNOC.Google
{
    public partial class AnalyticsHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                loadReports(Request.QueryString["v"] == null ? "monthly" : Request.QueryString["v"].ToString().ToLower());
            }

        }
        private void loadReports(string version)
        {
            var baseOptions = new Dictionary<string, object>();
            baseOptions.Add("height", 305);
            baseOptions.Add("width", 340);
            baseOptions.Add("is3D", true);

            DateTime dateStart;
            DateTime dateEnd;

            if (version == "prevday")
            {
                dateStart = (DateTime.Now.AddDays(-1));
            }
            else
            {
                dateStart = (DateTime.Now.AddMonths(-1));
            }
            dateEnd = (DateTime.Now.AddDays(-1));

            var basePagesOptions = new Dictionary<string, object>();
            basePagesOptions.Add("height", 305);
            basePagesOptions.Add("width", 428);
            basePagesOptions.Add("is3D", true);

            var baseMapOptions = new Dictionary<string, object>();
            baseMapOptions.Add("height", 330);
            baseMapOptions.Add("width", 780 );
            baseMapOptions.Add("is3D", true);

            var baseRegionOptions = new Dictionary<string, object>();
            baseRegionOptions.Add("height", 330);
            baseRegionOptions.Add("width", 292);
            baseRegionOptions.Add("is3D", true);


            report_BrowserOS.QueryName = "BrowserOS";
            report_BrowserOS.StartDate = dateStart;
            report_BrowserOS.EndDate = dateEnd;
            //report_BrowserOS.Dimensions = "ga:operatingSystem,ga:operatingSystemVersion,ga:browser,ga:browserVersion";
            report_BrowserOS.Dimensions = new string[1] { "ga:browser" };
            report_BrowserOS.Metrics = new string[1] { "ga:sessions" };
            report_BrowserOS.Sort = "-ga:sessions";
            report_BrowserOS.ChartType = ChartType.PieChart;
            report_BrowserOS.ChartOptions = new GoogleAnalytics.Json.ChartOptions();
            report_BrowserOS.ChartOptions.Options.Add("title", "Browser & OS");
            report_BrowserOS.ChartOptions.Options.Add("page", "enable");
            //report_BrowserOS.ChartOptions.Options.Add("pageSize", 10);
            report_BrowserOS.PageSize = 8;
            report_BrowserOS.ChartOptions.Extend(baseOptions);
            report_BrowserOS.DataBind();

            report_Device.QueryName = "Device";
            report_Device.StartDate = dateStart;
            report_Device.EndDate = dateEnd;
            report_Device.Dimensions = new string[1] { "ga:devicecategory" };
            report_Device.Metrics = new string[1] { "ga:sessions" };
            report_Device.Sort = "-ga:sessions";
            report_Device.ChartType = ChartType.PieChart;
            report_Device.ChartOptions = new GoogleAnalytics.Json.ChartOptions();
            report_Device.ChartOptions.Options.Add("title", "Devices");
            report_Device.ChartOptions.Options.Add("page", "enable");
            report_Device.ChartOptions.Options.Add("keepAspectRatio", "false");
            //report_Device.ChartOptions.Options.Add("pageSize", 10);
            report_Device.PageSize = 8;
            report_Device.ChartOptions.Extend(baseOptions);
            report_Device.DataBind();

            report_Pages.QueryName = "Pages";
            report_Pages.StartDate = dateStart;
            report_Pages.EndDate = dateEnd;
            report_Pages.Dimensions = new string[1] { "ga:pagePath" };
            report_Pages.Metrics = new string[1] { "ga:pageviews" };
            report_Pages.Sort = "-ga:pageviews";
            report_Pages.ChartType = ChartType.Table;
            report_Pages.ChartOptions = new GoogleAnalytics.Json.ChartOptions();
            report_Pages.ChartOptions.Options.Add("title", "Pages");
            report_Pages.ChartOptions.Options.Add("page", "enable");
            report_Pages.ChartOptions.Options.Add("pageSize", 12);
            report_Pages.MaxResults = 12;
            //report_Pages.PageSize = 11;
            report_Pages.ChartOptions.Extend(basePagesOptions);
            report_Pages.DataBind();

            report_Region.QueryName = "Region";
            report_Region.StartDate = dateStart;
            report_Region.EndDate = dateEnd;
            report_Region.Dimensions = new string[1] { "ga:region" };
            report_Region.Metrics = new string[1] { "ga:sessions" };
            report_Region.Sort = "-ga:sessions";
            report_Region.ChartType = ChartType.Table;
            report_Region.ChartOptions = new GoogleAnalytics.Json.ChartOptions();
            report_Region.ChartOptions.Options.Add("title", "Regions");
            report_Region.ChartOptions.Options.Add("page", "enable");
            report_Region.ChartOptions.Options.Add("pageSize", 13);
            report_Region.MaxResults = 13;
            //report_Region.PageSize = 12;
            report_Region.ChartOptions.Extend(baseRegionOptions);
            report_Region.DataBind();

            report_RegionMap.QueryName = "RegionMap";
            report_RegionMap.StartDate = dateStart;
            report_RegionMap.EndDate = dateEnd;
            report_RegionMap.Dimensions = new string[1] { "ga:region" };//,ga:city,ga:latitude,ga:longitude";
            report_RegionMap.Metrics = new string[1] { "ga:sessions" };
            report_RegionMap.Filter = "ga:country==United States";
            report_RegionMap.ChartType = ChartType.GeoChart;
            report_RegionMap.ChartOptions = new GoogleAnalytics.Json.ChartOptions();
            report_RegionMap.ChartOptions.Options.Add("title", "Region Map");
            report_RegionMap.ChartOptions.Options.Add("region", "US");
            report_RegionMap.ChartOptions.Options.Add("displayMode", "regions");
            report_RegionMap.ChartOptions.Options.Add("resolution", "provinces");
            //report_RegionMap.ChartOptions.Options.Add("page", "enable");
            report_RegionMap.ChartOptions.Extend(baseMapOptions);
            report_RegionMap.DataBind();
        }
    }
}