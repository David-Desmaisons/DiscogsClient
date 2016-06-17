using System;
using Newtonsoft.Json;
using RestSharpHelper;

namespace DiscogsClient.Data.Result 
{
    public class DiscogsReleaseVersion : DiscogsEntity
    {
        public string catno { get; set; }
        public string country { get; set; }
        public string format { get; set; }
        public string label { get; set; }
        [JsonConverter(typeof(BasicDateTimeConverter))]
        public DateTime? released { get; set; }
        public string resource_url { get; set; }
        public string status { get; set; }
        public string thumb { get; set; }
        public string title { get; set; }
    }
}
