using DiscogsClient.Data.Result;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace DiscogsClient.Test
{
    public class DiscogsArtistDeserializationTest
    {
        private const string _JsonResponse = "{\"profile\": \"\", \"realname\": \"Steve Destailleur\", \"releases_url\": \"https://api.discogs.com/artists/20861/releases\", \"name\": \"Tevatron\", \"uri\": \"https://www.discogs.com/artist/20861-Tevatron\", \"images\": [{\"uri\": \"\", \"height\": 113, \"width\": 150, \"resource_url\": \"\", \"type\": \"primary\", \"uri150\": \"\"}], \"resource_url\": \"https://api.discogs.com/artists/20861\", \"aliases\": [{\"resource_url\": \"https://api.discogs.com/artists/575215\", \"id\": 575215, \"name\": \"St\\u00e9phane Destailleur\"}, {\"resource_url\": \"https://api.discogs.com/artists/8324\", \"id\": 8324, \"name\": \"Steve D\"}], \"id\": 20861, \"data_quality\": \"Needs Vote\", \"namevariations\": [\"Tevathron\"]}";
        private readonly DiscogsArtist _Result;

        public DiscogsArtistDeserializationTest()
        {
            _Result = JsonConvert.DeserializeObject<DiscogsArtist>(_JsonResponse);
        }

        [Fact]
        public void DeserializeResult_IsNotNull()
        {
            _Result.Should().NotBeNull();
        }

        [Fact]
        public void DeserializeResult_Has_RealName()
        {
            _Result.realname.Should().Be("Steve Destailleur");
        }
    }
}
