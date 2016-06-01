using DiscogsClient.Data.Query;
using DiscogsClient.Data.Result;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using Xunit;

namespace DiscogsClient.Test
{
    public class TrackDeserializationTest
    {
        private const string _Track = "{\"duration\": \"7:13\",\"position\": \"2\",\"type_\": \"track\",\"extraartists\": [  {\"join\": \"\",\"name\": \"DJ Sangeet\",\"anv\": \"\",\"tracks\": \"\",\"role\": \"Written-By, Producer\",\"resource_url\": \"https://api.discogs.com/artists/25460\",\"id\": 25460  }],\"title\": \"From The Heart\"}";

        private readonly DiscogsTrack _Result;
 
        public TrackDeserializationTest()
        {
            _Result = JsonConvert.DeserializeObject<DiscogsTrack>(_Track);
        }

        [Fact]
        public void DeserializeResult_IsNotNull()
        {
            _Result.Should().NotBeNull();
        }

        [Fact]
        public void DeserializeDuration_IsOK()
        {
            _Result.duration.Should().Be(new TimeSpan(0,7,13));
        }

        [Fact]
        public void DeserializePosition_IsCorrect()
        {
            _Result.position.Should().Be(2);
        }

        [Fact]
        public void DeserializeTitle_IsCorrect()
        {
            _Result.title.Should().Be("From The Heart");
        }
    }
}
