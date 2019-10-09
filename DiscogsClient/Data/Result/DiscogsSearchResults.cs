namespace DiscogsClient.Data.Result
{
    public sealed class DiscogsSearchResults : DiscogsPaginableResults<DiscogsSearchResult>
    {
        public DiscogsSearchResult[] results { get; set; }

        public override DiscogsSearchResult[] GetResults()
        {
            return results;
        }
    }
}
