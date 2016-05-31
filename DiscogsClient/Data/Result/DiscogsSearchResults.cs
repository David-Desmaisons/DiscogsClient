namespace DiscogsClient.Data.Result
{
    public class DiscogsSearchResults
    {
        public DiscogsPaginedResult pagination { get; set; }

        public DiscogsSearchResult[] results { get; set; }
    }
}
