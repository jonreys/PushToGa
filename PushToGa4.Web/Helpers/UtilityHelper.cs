using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PushToGa.Web.Helpers
{
    public static class UtilityHelper
    {
        public static string GenerateDateOnlyFormat(string date)
        {
            var dateFromInput = DateTime.ParseExact(date, "yyyyMMdd", null, DateTimeStyles.None);
            string dateOnlyFormatForDisplay = dateFromInput.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

            return dateOnlyFormatForDisplay;
        }
    }
}
