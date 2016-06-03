using DiscogsClient.Data.Result;

namespace DiscogsClient.Data.Query 
{
    public class DiscogsSortInformation 
    {
        /// <summary>
        ///  Sort items by this field
        /// </summary>
        public DiscogsArtistSortType sort { get; set; }

        /// <summary>
        ///  Sort items in a particular order (one of asc, desc)
        /// </summary>
        public DiscogsSortOrderType sort_order { get; set; }
    }
}
