using DiscogsClient.Data.Query;
using FluentAssertions;
using RestSharpInfra.OAuth1;
using System.Threading.Tasks;
using Xunit;

namespace DiscogsClient.Test
{
    public class DiscogsClientTest
    {
        private readonly DiscogsClient _DiscogsClient;
        private readonly OAuthCompleteInformation _OAuthCompleteInformation;

        public DiscogsClientTest()
        {
            _OAuthCompleteInformation = new OAuthCompleteInformation("", "", "", "");
            _DiscogsClient = new DiscogsClient(_OAuthCompleteInformation);
        }

        [Fact(Skip ="Please provide valid token and keys to run the test")]
        public async Task Search_Release()
        {
            var discogsSearch = new DiscogsSearch()
            {
                artist = "Ornette Coleman",
                release_title = "The Shape Of Jazz To Come"
            };

            var res = await  _DiscogsClient.Search(discogsSearch);
            res.results.Length.Should().BeGreaterThan(0);
        }

        [Fact(Skip = "Please provide valid token and keys to run the test")]
        public async Task Search_Artist() {
            var discogsSearch = new DiscogsSearch() {
                query = "Ornette Coleman",
                type = DiscogsEntityType.artist
            };

            var res = await _DiscogsClient.Search(discogsSearch);
            res.results.Length.Should().BeGreaterThan(0);
        }
    }
}
