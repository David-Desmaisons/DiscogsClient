namespace DiscogsClient.Data.Result
{
    public class DiscogsReleaseBase : DiscogsEntity
    {
        public DiscogsArtist[] artists { get; set; }
        public DiscogsVideo[] videos { get; set; }
        public DiscogsImage[] images { get; set; }
        public DiscogsTrack[] tracklist { get; set; }
        public string resource_url { get; set; }
        public string data_quality { get; set; }
        public int year { get; set; }
        public string title { get; set; }
        public string uri { get; set; }
        public string[] genres { get; set; }
        public string[] styles { get; set; }
    }
}
