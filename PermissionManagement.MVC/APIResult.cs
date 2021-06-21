using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace PermissionManagement.MVC
{
    public class APIResult
    {
        private class APIResults
        {
            public int status { get; set; }
            public string info { get; set; }
            public string details { get; set; }
        }

        public static bool Checkemail(string mail)
        {
            const string APIURL = "https://api.email-validator.net/api/verify";
            HttpClient client = new HttpClient();
            string Email = mail;
            string APIKey = "ev-ad50bc79a5bcaf082989b83f08e54351";

            var postData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("EmailAddress", Email),
                new KeyValuePair<string, string>("APIKey", APIKey)
            };

            HttpContent content = new FormUrlEncodedContent(postData);

            HttpResponseMessage result = client.PostAsync(APIURL, content).Result;
            string resultContent = result.Content.ReadAsStringAsync().Result;

            APIResults res = new JavaScriptSerializer().
            Deserialize<APIResults>(resultContent);

            switch (res.status)
            {
                // valid addresses have a {200, 207, 215} result code
                // result codes 114 and 118 need a retry
                case 200:
                case 207:
                case 215:
                    // address is valid
                    // 215 - can be retried to update catch-all status
                    return true;
                case 114:
                    // greylisting, wait 5min and retry
                    return false;
                case 118:
                    // api rate limit, wait 5min and retry
                    return false;
                default:
                    // address is invalid
                    // res.info
                    // res.details
                    return false;
            }
        }
    }

}

