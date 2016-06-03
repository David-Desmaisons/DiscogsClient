using DiscogsClient.Data.Query;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DiscogsClient.Data.Result 
{
    public class DiscogsLabelRelease : DiscogsEntity
    {
        public string artist { get; set; }
        public int main_release { get; set; }
        public string resource_url { get; set; }
        public string role { get; set; }
        public string thumb { get; set; }
        public string title { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public DiscogsEntityType? type { get; set; }
        public int year { get; set; }
        public string format { get; set; }
        public string label { get; set; }
        public string status { get; set; }
    }
}
