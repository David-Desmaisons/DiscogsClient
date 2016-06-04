namespace DiscogsClient.Data.Result
{
    public class DiscogsIdentity : DiscogsEntity
    {
        public string username { get; set; }
        public string resource_url { get; set; }
        public string consumer_name { get; set; }
    }
}
