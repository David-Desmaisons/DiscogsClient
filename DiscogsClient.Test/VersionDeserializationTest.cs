using DiscogsClient.Data.Result;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace DiscogsClient.Test
{
    public class VersionDeserializationTest 
    {
        private const string _Version = "{ \"catno\": \"2078-2\", \"country\": \"Israel\", \"format\": \"CD, Album, Mixed\", \"id\": 528753, \"label\": \"Phonokol\", \"released\": \"1997\", \"resource_url\": \"http://api.discogs.com/releases/528753\",        \"status\": \"Accepted\",        \"thumb\": \"https://api-img.discogs.com/53aTwkmfs6fwWZhq5ma7-vbU7TY=/fit-in/150x150/filters:strip_icc():format(jpeg):mode_rgb()/discogs-images/R-528753-1127942810.jpeg.jpg\", \"title\": \"Stardiver\" }";
        private const string _Version2 = " { \"catno\": \"SPIRIT ZONE 027\", \"country\": \"Germany\", \"format\": \"CD, Album, Mixed\", \"id\": 66785, \"label\": \"Spirit Zone Recordings\", \"released\": \"1997-03-14\",        \"resource_url\": \"http://api.discogs.com/releases/66785\", \"status\": \"Accepted\", \"thumb\": \"https://api-img.discogs.com/sSWjXKczZseDjX2QohG1Lc77F-w=/fit-in/150x150/filters:strip_icc():format(jpeg):mode_rgb()/discogs-images/R-66785-1213949871.jpeg.jpg\", \"title\": \"Stardiver\" }";
        private readonly DiscogsReleaseVersion _Result;
        private readonly DiscogsReleaseVersion _Result2;

        public VersionDeserializationTest()
        {
            _Result = JsonConvert.DeserializeObject<DiscogsReleaseVersion>(_Version);
            _Result2 = JsonConvert.DeserializeObject<DiscogsReleaseVersion>(_Version2);
        }

        [Fact]
        public void DeserializeResult_IsNotNull()
        {
            _Result.Should().NotBeNull();
        }

        [Fact]
        public void DeserializeResultWithComplexDate_IsNotNull() 
        {
            _Result2.Should().NotBeNull();
        }
    }
}
