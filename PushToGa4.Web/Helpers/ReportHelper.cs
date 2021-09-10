using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.AnalyticsReporting.v4;
using Google.Apis.AnalyticsReporting.v4.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System.Configuration;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace PushToGa.Web.Helpers
{
    public class ReportHelper
    {
        private readonly IConfiguration configuration;

        public ReportHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        private AnalyticsReportingService GetAnalyticsReportingServiceInstance()
        {
            string[] scopes = { AnalyticsReportingService.Scope.AnalyticsReadonly }; //Read-only access to Google Analytics
            GoogleCredential credential;
            using (var stream = new FileStream(configuration.GetSection("KeyFileName").Value, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(scopes);
            }
            // Create the  Analytics service.
            return new AnalyticsReportingService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Push to Ga Web",
            });
        }

        public GetReportsResponse GetReport(GetReportsRequest getReportsRequest)
        {            
            var analyticsService = GetAnalyticsReportingServiceInstance();
            return analyticsService.Reports.BatchGet(getReportsRequest).Execute();
        }
    }
}
