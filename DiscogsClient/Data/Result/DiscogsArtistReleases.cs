namespace DiscogsClient.Data.Result 
{
    public class DiscogsArtistReleases : DiscogsPaginableResults<DiscogsArtistRelease> 
    {
        public DiscogsArtistRelease[] releases { get; set; }

        public override DiscogsArtistRelease[] GetResults() 
        {
            return releases;
        }
    }
}