namespace DiscogsClient.Data.Result
 {
    public class DiscogsGroupOrBandMember : DiscogsEntity 
    {
        public bool active { get; set; }
        public string name { get; set; }
        public string resource_url { get; set; }
    }
}