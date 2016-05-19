using System.ComponentModel;

namespace DiscogsClient.Data.Query 
{
    public class DiscogsSearch : DiscogsPaginable
    {
        //Your search query. Example: nirvana
        [Description("q")]
        public string query { get; set; }

        //Example: release
        public DiscogsReleaseType? type { get; set; }

        //Search by combined “Artist Name - Release Title” title field.
        //Example: nirvana - nevermind
        public string title { get; set; }

        //Search release titles. Example: nevermind
        public string  release_title { get; set; }

        //Search release credits.Example: kurt
        public string credit { get; set; }

        //Search artist names. Example: nirvana
        public string artist { get; set; }

        //Search artist ANV. Example: nirvana
        public string anv { get; set; }

        //Search label names. Example: dgc
        public string label { get; set; }

        //Search genres. Example: rock
        public string genre { get; set; }

        //Search style. Example: grunge
        public string style { get; set; }

        //Search country. Example: canada
        public string country { get; set; }

        //Search release year.  Example: 1991
        public int? year { get; set; }

        //Search formats.  Example: album
        public string format { get; set; }

        //Search catalog number.  Example: DGCD-24425
        public string catno { get; set; }

        //Search barcodes. Example: 7 2064-24425-2 4
        public string barcode { get; set; }

        //Search track titles. Example: smells like teen spirit
        public string track { get; set; }

        //Search submitter username. Example: milKt
        public string submitter { get; set; }

        //Search contributor usernames. Example: jerome99
        public string contributor { get; set; }
    }
}
