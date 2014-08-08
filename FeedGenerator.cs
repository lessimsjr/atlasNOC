using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using GoogleAnalytics.Json;
using Google.GData.Analytics;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.Apis.Analytics.v3;

namespace GoogleAnalytics
{
    public class FeedGenerator
    {
        private string _baseUrl = "https://www.googleapis.com/analytics/v2.4/data";
        private atlasNOC.GoogleAnalyticsAPI gaAPI;

        //private DataFeed queryResults;
        private atlasNOC.GoogleAnalyticsAPI.AnalyticDataPoint queryResults;

        public Google.Apis.Analytics.v3.AnalyticsService AnalyticsService;
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public string GaProfileId { set; get; }

        /// <summary>
        /// Initializes a new instance of the FeedGenerator
        /// </summary>
        public FeedGenerator()
        {
            //AnalyticsService = new AnalyticsService(AnalyticsConfig.AppName);
            //AnalyticsService.setUserCredentials(AnalyticsConfig.UserName, AnalyticsConfig.AppKey);
            gaAPI = new atlasNOC.GoogleAnalyticsAPI(@"C:\Projects\Development\atlasNOC\atlasNOC\atlasNOC\atlasNOC-b881d3cac99b.p12", "304758295118-dtke240l48i3v8jjqa06tihsedra2qbr@developer.gserviceaccount.com");
            AnalyticsService = gaAPI.Service;
            GaProfileId = AnalyticsConfig.ProfileId;
        }

        /// <summary>
        /// Sets the start and end dates for all queries to run against
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public void SetGenericQueryProperties(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        private string DateAsGAString(DateTime value)
        {
            return value.Year + "-" + value.Month.ToString("00") + "-" + value.Day.ToString("00");
        }

        /// <summary>
        /// Runs a Query against the Google Analytics provider - AnalyticsService
        /// </summary>
        /// <param name="dimensions">multiple dimensions separated by a comma: ga:dimensionName, ga:dimensionName</param>
        /// <param name="metrics">multiple metrics separated by a comma: ga:metricName, ga:metricName</param>
        /// <param name="numberToRetrieve">the max number of entries to return</param>
        public void RunQuery(string[] dimensions, string[] metrics, string sort, string filters, int? numberToRetrieve)
        {
            try
            {
                // GA Data Feed query uri.
                //string dataFeedUrl = _baseUrl;
                //string accountFeedUrl = "https://www.googleapis.com/analytics/v2.4/management/accounts";
                DataQuery query = new DataQuery(_baseUrl + "?key=");  // + AnalyticsConfig.APIKey);
                query.Ids = GaProfileId;
                //query.Dimensions = dimensions; //"ga:date";
                //query.Metrics = metrics; //"ga:visitors, ga:newVisits, ga:bounces";
                //query.Segment = "gaid::-11";
                //query.Filters = "ga:medium==referral";
                
                query.Sort = sort;//query.Sort = "-ga:visits";
                query.Filters = filters;
                //query.NumberToRetrieve = 5;
                //if (numberToRetrieve != default(int))
                //{
                //    query.NumberToRetrieve = numberToRetrieve;
                //}
                query.GAStartDate = DateAsGAString(StartDate);
                query.GAEndDate = DateAsGAString(EndDate);
                Uri url = query.Uri;

                // Send our request to the Analytics API and wait for the results to
                // come back.
                //queryResults = AnalyticsService.Query(query);
                queryResults = gaAPI.GetAnalyticsData(GaProfileId, dimensions, metrics, StartDate, EndDate, sort, filters, numberToRetrieve);
            }
            catch (AuthenticationException ex)
            {
                throw new Exception("Authentication failed : " + ex.Message);
            }
            catch (Google.GData.Client.GDataRequestException ex)
            {
                throw new Exception("Authentication failed : " + ex.Message);
            }
        }

        /// <summary>
        /// Converts Google.Analytics.DataFeed into a JsonDataTable
        /// </summary>
        /// <param name="jsKey">a unique key for the javascript variable.  variable is created as var {jsKey}_JsonTable = {};</param>
        /// <returns></returns>
        public string ResultsAsJsonTable(string jsKey)
        {
            string x = queryResults.ToString();

            if (queryResults.Rows.Count() == 0)
            {
                return "{}";
            }

            JsonTable tab = new JsonTable(jsKey);
            int idx = 0;
            int hdx = 0;

            foreach (var header in queryResults.ColumnHeaders)
            {
                tab.Columns.Add(new JsonColumn() { ID = header.Name, Label = getLabelFriendlyName(header.Name), Type = header.ColumnType == "DIMENSION" ? JsonType.STRING : JsonType.NUMBER });

                hdx++;
            }

            foreach (var entry in queryResults.Rows)
            {
                JsonRow row = new JsonRow();

                // Dimensions
                JsonCell Dimcell = new JsonCell();
                Dimcell.Type = JsonType.STRING;
                int valAsNumDate;
                if ((int.TryParse(entry[0], out valAsNumDate)) && (entry[0].Length == 8))
                {
                    string year = entry[0].Substring(0, 4);
                    string month = entry[0].Substring(4, 2);
                    string day = entry[0].Substring(6);
                    Dimcell.Value = month + '/' + day + '/' + year;
                }
                else
                {
                    Dimcell.Value = entry[0];
                }
                row.Cells.Add(Dimcell);
                
                // Metrics
                JsonCell Metcell = new JsonCell();
                Metcell.Type = JsonType.NUMBER;
                Metcell.Value = entry[1];
                row.Cells.Add(Metcell);



                idx++;
                tab.Rows.Add(row);
            }

            return tab.ToString();
        }

        private string getLabelFriendlyName(string value)
        {
            string retVal = value.Substring(3);

            if (string.IsNullOrWhiteSpace(retVal))
                return string.Empty;
            StringBuilder newText = new StringBuilder(retVal.Length * 2);
            newText.Append(retVal[0]);
            for (int i = 1; i < retVal.Length; i++)
            {
                if (char.IsUpper(retVal[i]) && retVal[i - 1] != ' ')
                    newText.Append(' ');
                newText.Append(retVal[i]);
            }
            return newText.ToString().ToLower();
        }
    }
}