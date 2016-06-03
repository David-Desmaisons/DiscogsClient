namespace DiscogsClient.Data.Result
{
    public class DiscogsSearchResults : DiscogsPaginableResults<DiscogsSearchResult>
    {
        public DiscogsSearchResult[] results { get; set; }

        public override DiscogsSearchResult[] GetResults()
        {
            return results;
        }
    }
}
