namespace DiscogsClient.Data.Result
{
    public class DiscogsPaginedResult
    {
        public int per_page { get; set; }
        public int pages { get; set; }
        public int page { get; set; }
        public int items { get; set; }
        public DiscogsPaginedUrls pagination { get; set; }
    }
}
