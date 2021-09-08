using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushToGa.Web.Models
{
    public class ReportResultModel
    {
        public string EventCategory { get; set; }
        public string EventAction { get; set; }
        public string EventLabel { get; set; }
        public string NumberUser { get; set; }
        public string Date { get; set; }
        public int Sessions { get; set; }
    }
}
