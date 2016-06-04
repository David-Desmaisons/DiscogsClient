namespace DiscogsClient.Data.Result
{
    public class DiscogsLabel : DiscogsEntity
    {
        public DiscogsImage[] images { get; set; }
        public DiscogsSimplifiedLabel[] sublabels { get; set; }
        public DiscogsSimplifiedLabel parent_label { get; set; }
        public string[] urls { get; set; }
        public string profile { get; set; }
        public string releases_url { get; set; }
        public string name { get; set; }
        public string contact_info { get; set; }
        public string uri { get; set; }
        public string resource_url { get; set; }
        public string data_quality { get; set; }
    }
}