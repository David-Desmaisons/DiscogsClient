namespace DiscogsClient.Data.Result 
{
    public class DiscogsTrack : DiscogsSubtrack
    {
        public DiscogsSubtrack[] sub_tracks { get; set; }
    }
}