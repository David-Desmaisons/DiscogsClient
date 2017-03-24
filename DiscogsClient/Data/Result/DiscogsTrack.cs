using Newtonsoft.Json;
using System;
using RestSharpHelper;

namespace DiscogsClient.Data.Result 
{
    public class DiscogsTrack 
    {
        public string title { get; set; }
        public string type_ { get; set; }
        [JsonConverter(typeof(BasicTimeSpanConverter))]
        public TimeSpan? duration { get; set; }
        public string position { get; set; }
        public DiscogsReleaseArtist[] extraartists { get; set; }
        public DiscogsReleaseArtist[] artists { get; set; }
    }
}
