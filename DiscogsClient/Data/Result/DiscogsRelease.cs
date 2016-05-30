using System.Collections.Generic;

namespace DiscogsClient.Data.Result 
{
    public class DiscogsRelease : DiscogsEntity
    {
        public List<DiscogsArtist> artists { get; set; }
        public List<DiscogsImage> images { get; set; }
        public List<DiscogsTrack> tracklist { get; set; }
        public List<string> genre { get; set; }
        public List<string> style { get; set; }
        public List<string> label { get; set; }
        public List<string> format { get; set; }
        public int year { get; set; }
        public string title { get; set; }
        public string catno { get; set; }
        public string resource_url { get; set; }
        public string type { get; set; }
    }
}
