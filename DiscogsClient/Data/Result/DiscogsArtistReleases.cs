namespace DiscogsClient.Data.Result 
{
    public sealed class DiscogsArtistReleases : DiscogsPaginableResults<DiscogsArtistRelease> 
    {
        public DiscogsArtistRelease[] releases { get; set; }

        public override DiscogsArtistRelease[] GetResults() 
        {
            return releases;
        }
    }
}