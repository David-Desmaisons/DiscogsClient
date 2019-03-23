namespace DiscogsClient.Data.Result 
{
    public class DiscogsMaster : DiscogsReleaseBase 
    {
        public int main_release { get; set; }
        public string main_release_url { get; set; }
        public int most_recent_release { get; set; }
        public string most_recent_release_url { get; set; }
        public int num_for_sale { get; set; }
        public decimal lowest_price { get; set; }
        public string versions_url { get; set; }
    }
}
