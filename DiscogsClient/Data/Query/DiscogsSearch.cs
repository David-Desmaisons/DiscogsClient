using System.ComponentModel;

namespace DiscogsClient.Data.Query 
{
    public class DiscogsSearch : DiscogsPaginable
    {
        /// <summary>
        ///  Your search query. Example: nirvana
        /// </summary>
        [Description("q")]
        public string query { get; set; }

        /// <summary>
        ///  Example: release
        /// </summary>
        public DiscogsReleaseType? type { get; set; }

        /// <summary>
        ///  Search by combined “Artist Name - Release Title” title field.
        ///  Example: nirvana - nevermind
        /// </summary>
        public string title { get; set; }

        /// <summary>
        ///  Search release titles. Example: nevermind
        /// </summary>
        public string  release_title { get; set; }

        /// <summary>
        ///  Search release credits.Example: kurt
        /// </summary>
        public string credit { get; set; }

        /// <summary>
        ///  Search artist names. Example: nirvana
        /// </summary>
        public string artist { get; set; }

        /// <summary>
        ///  Search artist ANV. Example: nirvana
        /// </summary>
        public string anv { get; set; }

        /// <summary>
        ///  Search label names. Example: dgc
        /// </summary>
        public string label { get; set; }

        /// <summary>
        ///  Search genres. Example: rock
        /// </summary>
        public string genre { get; set; }

        /// <summary>
        /// Search style. Example: grunge
        /// </summary>
        public string style { get; set; }

        /// <summary>
        ///  Search country. Example: canada
        /// </summary>
        public string country { get; set; }

        /// <summary>
        ///  Search release year.  Example: 1991
        /// </summary>
        public int? year { get; set; }

        /// <summary>
        ///  Search formats.  Example: album
        /// </summary>
        public string format { get; set; }

        /// <summary>
        ///  Search catalog number.  Example: DGCD-24425
        /// </summary>
        public string catno { get; set; }

        /// <summary>
        ///  Search barcodes. Example: 7 2064-24425-2 4
        /// </summary>
        public string barcode { get; set; }

        /// <summary>
        ///  Search track titles. Example: smells like teen spirit
        /// </summary>
        public string track { get; set; }

        /// <summary>
        ///  Search submitter username. Example: milKt
        /// </summary>
        public string submitter { get; set; }

        /// <summary>
        ///  Search contributor usernames. Example: jerome99
        /// </summary>
        public string contributor { get; set; }
    }
}
