namespace DiscogsClient.Data.Result
{
    public abstract class DiscogsPaginableResults<T> where T: DiscogsEntity
    {
        public DiscogsPaginedResult pagination { get; set; }

        public abstract T[] GetResults();
    }
}
