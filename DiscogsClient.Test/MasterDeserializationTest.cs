using DiscogsClient.Data.Result;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace DiscogsClient.Test
{
    public class MasterDeserializationTest 
    {
        private const string _Master = "{\"styles\": [\"Techno\", \"Deep Techno\", \"Deep House\", \"Disco\"], \"genres\": [\"Electronic\"], \"num_for_sale\": 7, \"title\": \"Conceptions Inspirations b/w Sweet Love \", \"most_recent_release\": 13111150, \"main_release\": 13111150, \"main_release_url\": \"https://api.discogs.com/releases/13111150\", \"uri\": \"https://www.discogs.com/Vintage-Future-ft-Vann-Johnson-and-Syndicate-Of-Swing-Conceptions-Inspirations-bw-Sweet-Love-/master/1489926\", \"artists\": [{\"join\": \"ft.\", \"name\": \"Vintage Future\", \"anv\": \"\", \"tracks\": \"\", \"role\": \"\", \"resource_url\": \"https://api.discogs.com/artists/65478\", \"id\": 65478}, {\"join\": \"and\", \"name\": \"Vann Johnson\", \"anv\": \"\", \"tracks\": \"\", \"role\": \"\", \"resource_url\": \"https://api.discogs.com/artists/603935\", \"id\": 603935}, {\"join\": \"\", \"name\": \"Syndicate Of Swing\", \"anv\": \"\", \"tracks\": \"\", \"role\": \"\", \"resource_url\": \"https://api.discogs.com/artists/1185799\", \"id\": 1185799}], \"versions_url\": \"https://api.discogs.com/masters/1489926/versions\", \"data_quality\": \"Correct\", \"most_recent_release_url\": \"https://api.discogs.com/releases/13111150\", \"year\": 2019, \"images\": [{\"uri\": \"\", \"height\": 600, \"width\": 600, \"resource_url\": \"\", \"type\": \"primary\", \"uri150\": \"\"}, {\"uri\": \"\", \"height\": 600, \"width\": 600, \"resource_url\": \"\", \"type\": \"secondary\", \"uri150\": \"\"}, {\"uri\": \"\", \"height\": 600, \"width\": 600, \"resource_url\": \"\", \"type\": \"secondary\", \"uri150\": \"\"}], \"resource_url\": \"https://api.discogs.com/masters/1489926\", \"lowest_price\": 13.51, \"id\": 1489926, \"tracklist\": [{\"duration\": \"08:05\", \"position\": \"A\", \"type_\": \"track\", \"title\": \"Conceptions Inspirations\"}, {\"duration\": \"13:18\", \"position\": \"B\", \"type_\": \"track\", \"title\": \"Sweet Love\"}]}";
        private readonly DiscogsMaster _Result;
        public MasterDeserializationTest()
        {
            _Result = JsonConvert.DeserializeObject<DiscogsMaster>(_Master);
        }

        [Fact]
        public void DeserializeResult_IsNotNull()
        {
            _Result.Should().NotBeNull();
        }

        [Fact]
        public void DeserializeResult_Deserialize_most_recent_release()
        {
            _Result.most_recent_release.Should().Be(13111150);
        }

        [Fact]
        public void DeserializeResult_Deserialize_most_recent_release_url()
        {
            _Result.most_recent_release_url.Should().Be("https://api.discogs.com/releases/13111150");
        }

        [Fact]
        public void DeserializeResult_Deserialize_num_for_sale()
        {
            _Result.num_for_sale.Should().Be(7);
        }

        [Fact]
        public void DeserializeResult_Deserialize_lowest_price()
        {
            _Result.lowest_price.Should().Be(13.51m);
        }
    }
}
