namespace DiscogsClient.Data.Result
{
    public class DiscogsLabelRelease : DiscogsEntity
    {
        public string artist { get; set; }
        public string catno { get; set; }
        public string format { get; set; }
        public string resource_url { get; set; }
        public string status { get; set; }
        public string thumb { get; set; }
        public string title { get; set; }
        public int year { get; set; }
    }
}
