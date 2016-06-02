namespace DiscogsClient.Data.Result 
{
    public class DiscogsMaster : DiscogsReleaseBase 
    {
        public int main_release { get; set; }
        public string main_release_url { get; set; }
        public string versions_url { get; set; }
    }
}
