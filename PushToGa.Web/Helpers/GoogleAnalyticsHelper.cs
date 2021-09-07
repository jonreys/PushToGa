using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PushToGa.Web.Helpers
{
    public class GoogleAnalyticsHelper
    {
        private readonly string endpoint = "http://www.google-analytics.com/collect";
        private readonly string googleVersion = "1";
        private readonly string googleTrackingId; // UA-XXXXXXXXX-XX
        private readonly string googleClientId; // 555 - any user identifier

        public GoogleAnalyticsHelper(string trackingId, string clientId)
        {
            this.googleTrackingId = trackingId;
            this.googleClientId = clientId;
        }

        public async Task<HttpResponseMessage> TrackEvent(string category, string action, string label, int? value = null)
        {
            if (string.IsNullOrEmpty(category))
                throw new ArgumentNullException(nameof(category));

            if (string.IsNullOrEmpty(action))
                throw new ArgumentNullException(nameof(action));

            using (var httpClient = new HttpClient())
            {
                var postData = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("v", googleVersion),
                    new KeyValuePair<string, string>("tid", googleTrackingId),
                    new KeyValuePair<string, string>("cid", googleClientId),
                    new KeyValuePair<string, string>("t", "event"),
                    new KeyValuePair<string, string>("ec", category),
                    new KeyValuePair<string, string>("ea", action)
                };

                if (label != null)
                {
                    postData.Add(new KeyValuePair<string, string>("el", label));
                }

                if (value != null)
                {
                    postData.Add(new KeyValuePair<string, string>("ev", value.ToString()));
                }


                return await httpClient.PostAsync(endpoint, new FormUrlEncodedContent(postData)).ConfigureAwait(false);
            }
        }
    }
}
