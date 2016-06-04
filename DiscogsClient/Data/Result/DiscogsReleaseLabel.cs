namespace DiscogsClient.Data.Result
{
    public class DiscogsReleaseLabel : DiscogsEntity
    {
        public string catno { get; set; }
        public string entity_type { get; set; }
        public string entity_type_name { get; set; }
        public string name { get; set; }
        public string resource_url { get; set; }
    }
}