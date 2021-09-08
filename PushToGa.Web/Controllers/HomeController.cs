using Google.Apis.AnalyticsReporting.v4.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PushToGa.Web.Helpers;
using PushToGa.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PushToGa.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            var result = new List<ReportResultModel>();
            try
            {
                #region Prepare Report Request object 
                var dateRange = new DateRange
                {
                    StartDate = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd"),
                    EndDate = DateTime.UtcNow.ToString("yyyy-MM-dd")
                };              
                var metrics = new List<Metric> { new Metric { Expression = "ga:sessions", Alias = "Sessions" } };
                var dimensions = new List<Dimension> { new Dimension { Name = "ga:eventCategory" }
                                                     , new Dimension { Name = "ga:eventAction" }
                                                     , new Dimension { Name = "ga:eventLabel" }
                                                     , new Dimension { Name = "ga:date"} };

                var ViewId = configuration.GetSection("ViewId").Value;

                var reportRequest = new ReportRequest
                {
                    DateRanges = new List<DateRange> { dateRange },
                    Metrics = metrics,
                    Dimensions = dimensions,                    
                    ViewId = ViewId
                };
                var getReportsRequest = new GetReportsRequest();
                getReportsRequest.ReportRequests = new List<ReportRequest> { reportRequest };
                #endregion

                var response = new ReportHelper(configuration).GetReport(getReportsRequest);

                result = PrintReport(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View(result);
        }

        private List<ReportResultModel> PrintReport(GetReportsResponse response)
        {            
            var results = new List<ReportResultModel>();
            var reportModel = new ReportResultModel();
            foreach (var report in response.Reports)
            {
                var rows = report.Data.Rows;
                ColumnHeader header = report.ColumnHeader;
                var dimensionHeaders = header.Dimensions;
                var metricHeaders = header.MetricHeader.MetricHeaderEntries;
                
                if (!rows.Any())
                {
                    Console.WriteLine("No data found!");
                    return null;
                }
                else
                {
                    foreach (var row in rows)
                    {
                        reportModel = new ReportResultModel();                        
                        var dimensions = row.Dimensions;
                        var metrics = row.Metrics;
                        for (int i = 0; i < dimensionHeaders.Count && i < dimensions.Count; i++)
                        {
                            if (dimensionHeaders[i].Contains("eventCategory"))
                            {
                                reportModel.EventCategory = dimensions[i];
                            }
                            if (dimensionHeaders[i].Contains("eventAction"))
                            {
                                reportModel.EventAction = dimensions[i];
                            }
                            if (dimensionHeaders[i].Contains("eventLabel"))
                            {
                                reportModel.EventLabel = dimensions[i];
                            }
                            if (dimensionHeaders[i].Contains("date"))
                            {
                                reportModel.Date = UtilityHelper.GenerateDateOnlyFormat(dimensions[i]);
                            }
                        }
                        for (int j = 0; j < metrics.Count; j++)
                        {
                            DateRangeValues values = metrics[j];
                            for (int k = 0; k < values.Values.Count && k < metricHeaders.Count; k++)
                            {
                                reportModel.Sessions = Convert.ToInt32(values.Values[k]);
                            }
                        }
                        
                        results.Add(reportModel);

                    }
                }
            }

            return results;
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
