using System.Collections.Generic;

namespace DiscogsClient.Data.Result 
{
    public class DiscogsRelease 
    {
        public List<DiscogsArtist> artists { get; set; }
        public List<DiscogsImage> images { get; set; }
        public List<dynamic> tracklist { get; set; }
        public List<string> genre { get; set; }
        public List<string> label { get; set; }
        public int year { get; set; }
        public int id { get; set; }
        public string title { get; set; }
    }
}
