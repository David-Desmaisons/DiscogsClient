namespace DiscogsClient.Data.Result
{
    public class DiscogsResults<T>
    {
        public DiscogsPaginedResult pagination { get; set; }

        public T[] results { get; set; }
    }
}
