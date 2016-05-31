namespace DiscogsClient.Data.Result
{
    public class DiscogsRelease : DiscogsEntity
    {
        public DiscogsArtist[] artists { get; set; }
        public DiscogsImage[] images { get; set; }
        public DiscogsTrack[] tracklist { get; set; }
        public string[] genre { get; set; }
        public string[] style { get; set; }
        public string[] label { get; set; }
        public string[] format { get; set; }
        public int year { get; set; }
        public string title { get; set; }
        public string catno { get; set; }
        public string resource_url { get; set; }
        public string type { get; set; }
    }
}
