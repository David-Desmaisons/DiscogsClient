namespace DiscogsClient.Data.Result 
{
    public class DiscogsArtist : DiscogsEntity 
    {
        public DiscogsImage[] images { get; set; }
        public DiscogsBandMember[] members { get; set; }
        public string[] urls { get; set; }
        public string[] namevariations { get; set; }
        public string profile { get; set; }
        public string releases_url { get; set; }
        public string resource_url { get; set; }
        public string uri { get; set; }
        public string data_quality { get; set; }
    }
}
