namespace DiscogsClient.Data.Query
{
    public class DiscogsPaginable
    {
        //number(optional) Example: 3
        //The page you want to request
        public int? page { get; set;  }

        //per_page
        //number(optional) Example: 25
        public int? per_page { get; set; }
    }
}
