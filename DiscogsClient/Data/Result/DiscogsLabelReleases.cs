namespace DiscogsClient.Data.Result
{
    public class DiscogsLabelReleases: DiscogsPaginableResults<DiscogsLabelRelease>
    {
        public DiscogsLabelRelease[] releases { get; set; }

        public override DiscogsLabelRelease[] GetResults()
        {
            return releases;
        }
    }
}