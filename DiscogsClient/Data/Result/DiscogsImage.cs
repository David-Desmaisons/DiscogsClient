namespace DiscogsClient.Data.Result 
{
    public class DiscogsImage 
    {
        public string uri { get; set; }
        public string uri150 { get; set; }
        public DiscogsImageType type { get; set; }
        public int height { get; set; }
        public int width { get; set; }
    }
}
