namespace DiscogsClient.Data.Result
{
    public sealed class DiscogsReleaseVersions : DiscogsPaginableResults<DiscogsReleaseVersion>
    {
        public DiscogsReleaseVersion[] versions { get; set; }

        public override DiscogsReleaseVersion[] GetResults()
        {
            return versions;
        }
    }
}