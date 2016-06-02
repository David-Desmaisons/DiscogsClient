namespace DiscogsClient.Data.Result
{
    public class DiscogsCommunity : DiscogsCommunityInfo
    {
        public DiscogsUser[] contributors { get; set; }
        public string data_quality { get; set; }
        public DiscogsRating rating { get; set; }
        public string status { get; set; }
        public DiscogsUser submitter { get; set; }
    }
}
