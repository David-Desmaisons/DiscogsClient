namespace DiscogsClient.Data.Result 
{
    public class DiscogsReleaseArtist : DiscogsEntity
    {
        public string name { get; set; }
        public string anv { get; set; }
        public string join { get; set; }
        public string resource_url { get; set; }
        public string role { get; set; }
        public string tracks { get; set; }
    }
}
