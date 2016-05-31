using DiscogsClient.Data.Query;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace DiscogsClient.Data.Result 
{
    public class DiscogsSearchResult : DiscogsEntity
    {
        public List<string> genre { get; set; }
        public List<string> style { get; set; }
        public List<string> label { get; set; }
        public List<string> format { get; set; }
        public int? year { get; set; }
        public string title { get; set; }
        public string catno { get; set; }
        public string resource_url { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public DiscogsReleaseType type { get; set; }
    }
}