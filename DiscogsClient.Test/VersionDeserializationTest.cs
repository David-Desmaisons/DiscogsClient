using DiscogsClient.Data.Result;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;

namespace DiscogsClient.Test
{
    public class VersionDeserializationTest
    {
        private const string _Version = "{ \"catno\": \"2078-2\", \"country\": \"Israel\", \"format\": \"CD, Album, Mixed\", \"id\": 528753, \"label\": \"Phonokol\", \"released\": \"1997\", \"resource_url\": \"http://api.discogs.com/releases/528753\",        \"status\": \"Accepted\",        \"thumb\": \"https://api-img.discogs.com/53aTwkmfs6fwWZhq5ma7-vbU7TY=/fit-in/150x150/filters:strip_icc():format(jpeg):mode_rgb()/discogs-images/R-528753-1127942810.jpeg.jpg\", \"title\": \"Stardiver\" }";
        private const string _Version2 = " { \"catno\": \"SPIRIT ZONE 027\", \"country\": \"Germany\", \"format\": \"CD, Album, Mixed\", \"id\": 66785, \"label\": \"Spirit Zone Recordings\", \"released\": \"1997-03-14\",        \"resource_url\": \"http://api.discogs.com/releases/66785\", \"status\": \"Accepted\", \"thumb\": \"https://api-img.discogs.com/sSWjXKczZseDjX2QohG1Lc77F-w=/fit-in/150x150/filters:strip_icc():format(jpeg):mode_rgb()/discogs-images/R-66785-1213949871.jpeg.jpg\", \"title\": \"Stardiver\" }";
        private const string _Version3 = " { \"catno\": \"SPIRIT ZONE 027\", \"country\": \"Germany\", \"format\": \"CD, Album, Mixed\", \"id\": 66785, \"label\": \"Spirit Zone Recordings\", \"released\": \"1997-03-0\",        \"resource_url\": \"http://api.discogs.com/releases/66785\", \"status\": \"Accepted\", \"thumb\": \"https://api-img.discogs.com/sSWjXKczZseDjX2QohG1Lc77F-w=/fit-in/150x150/filters:strip_icc():format(jpeg):mode_rgb()/discogs-images/R-66785-1213949871.jpeg.jpg\", \"title\": \"Stardiver\" }";
        private const string _Version4 = " { \"catno\": \"SPIRIT ZONE 027\", \"country\": \"Germany\", \"format\": \"CD, Album, Mixed\", \"id\": 66785, \"label\": \"Spirit Zone Recordings\", \"resource_url\": \"http://api.discogs.com/releases/66785\", \"status\": \"Accepted\", \"thumb\": \"https://api-img.discogs.com/sSWjXKczZseDjX2QohG1Lc77F-w=/fit-in/150x150/filters:strip_icc():format(jpeg):mode_rgb()/discogs-images/R-66785-1213949871.jpeg.jpg\", \"title\": \"Stardiver\" }";

        public static IEnumerable<object[]> BasicData
        {
            get
            {
                return new[]
                {
                    new object[] { _Version },
                    new object[] { _Version2 }
                };
            }
        }

        [Theory, MemberData(nameof(BasicData))]
        public void DeserializeResult_IsNotNull(string version)
        {
            var result = JsonConvert.DeserializeObject<DiscogsReleaseVersion>(version);
            result.Should().NotBeNull();
        }

        public static IEnumerable<object[]> Data
        {
            get
            {
                return new[]
                {
                    new object[] { _Version,  new DateTime(1997, 1, 1) },
                    new object[] { _Version2, new DateTime(1997, 3, 14) },
                    new object[] { _Version3, new DateTime(1997, 3, 1) },
                    new object[] { _Version4, null }
                };
            }
        }

        [Theory, MemberData(nameof(Data))]
        public void DeserializeResult_DateTimeIsCorrect(string version, DateTime? expected)
        {
            var result = JsonConvert.DeserializeObject<DiscogsReleaseVersion>(version);
            result.Should().NotBeNull();
        }
    }
}
