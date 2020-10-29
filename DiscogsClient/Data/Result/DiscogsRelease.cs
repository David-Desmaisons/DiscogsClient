using System;

namespace DiscogsClient.Data.Result
{
    public class DiscogsRelease : DiscogsReleaseBase
    {
        public DiscogsReleaseArtist[] extraartists { get; set; }
        public DiscogsReleaseLabel[] labels { get; set; }
        public DiscogsReleaseLabel[] companies { get; set; }
        public DiscogsFormat[] formats { get; set; }
        public DiscogsIdentifier[] identifiers { get; set; }
        public DiscogsCommunity community { get; set; }
        public DiscogsReleaseLabel[] series { get; set; }
        public string artists_sort { get; set; }
        public string catno { get; set; }
        public string country { get; set; }
        public DateTime date_added { get; set; }
        public DateTime date_changed { get; set; }
        public int estimated_weight { get; set; }
        public int format_quantity { get; set; }
        public decimal? lowest_price { get; set; }
        public int master_id { get; set; }
        public string master_url { get; set; }
        public string notes { get; set; }
        public int num_for_sale { get; set; }
        public string released { get; set; }
        public string released_formatted { get; set; }
        public string status { get; set; }
        public string thumb { get; set; }
    }
}
