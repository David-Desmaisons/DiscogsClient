namespace DiscogsClient.Data.Result
{
    public sealed class DiscogsLabelReleases: DiscogsPaginableResults<DiscogsLabelRelease>
    {
        public DiscogsLabelRelease[] releases { get; set; }

        public override DiscogsLabelRelease[] GetResults()
        {
            return releases;
        }
    }
}