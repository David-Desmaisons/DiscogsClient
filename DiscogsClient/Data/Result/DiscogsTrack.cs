using DiscogsClient.Internal;
using Newtonsoft.Json;
using System;

namespace DiscogsClient.Data.Result 
{
    public class DiscogsTrack 
    {
        public string title { get; set; }
        public string type_ { get; set; }
        [JsonConverter(typeof(DiscogsTimeSpanConverter))]
        public TimeSpan duration { get; set; }
        public int position { get; set; }
    }
}
