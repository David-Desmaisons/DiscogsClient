using DiscogsClient.Data.Query;
using DiscogsClient.Data.Result;
using FluentAssertions;
using RestSharpInfra.OAuth1;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DiscogsClient.Test
{
    public class DiscogsClientTest
    {
        private readonly DiscogsClient _DiscogsClient;
        private readonly OAuthCompleteInformation _OAuthCompleteInformation;
        private int _Count;

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
        public async Task Search_Artist()
        {
            var discogsSearch = new DiscogsSearch()
            {
                query = "Ornette Coleman",
                type = DiscogsEntityType.artist
            };

            var res = await _DiscogsClient.Search(discogsSearch);
            res.results.Length.Should().BeGreaterThan(0);
        }

        [Fact(Skip = "Please provide valid token and keys to run the test")]
        public async Task SearchAll_Release()
        {
            var discogsSearch = new DiscogsSearch()
            {
                artist = "Ornette Coleman",
                release_title = "The Shape Of Jazz To Come"
            };

            var observable = _DiscogsClient.SearchAll(discogsSearch);
            await observable.ForEachAsync(OnResult);
        }

        private void OnResult(DiscogsSearchResult result)
        {
            _Count++;
            Trace.WriteLine($"{_Count} - {result.title}");
        }
    }
}
