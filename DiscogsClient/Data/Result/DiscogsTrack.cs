namespace DiscogsClient.Data.Result 
{
    public class DiscogsTrack : DiscogsSubtrack
    {
        public DiscogsReleaseArtist[] extraartists { get; set; }
        public DiscogsReleaseArtist[] artists { get; set; }
        public DiscogsSubtrack[] sub_tracks { get; set; }
    }
}