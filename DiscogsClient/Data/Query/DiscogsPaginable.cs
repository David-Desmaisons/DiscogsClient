namespace DiscogsClient.Data.Query
{
    public class DiscogsPaginable
    {
        /// <summary>
        ///  number(optional) Example: 3
        ///  The page you want to request
        /// </summary>
        public int? page { get; set;  }

        /// <summary>
        ///  number(optional) Example: 25
        ///  The number of item per page
        /// </summary>
        public int? per_page { get; set; }
    }
}
