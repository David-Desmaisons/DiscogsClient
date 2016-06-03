namespace DiscogsClient.Data.Result
{
    public class DiscogsReleaseVersions : DiscogsPaginableResults<DiscogsReleaseVersion>
    {
        public DiscogsReleaseVersion[] versions { get; set; }

        public override DiscogsReleaseVersion[] GetResults()
        {
            return versions;
        }
    }
}