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

        [Fact(Skip = "Please provide valid token and keys to run the test")]
        public async Task SearchAll_Artist()
        {
            var discogsSearch = new DiscogsSearch()
            {
                query = "Ornette Coleman",
                type = DiscogsEntityType.artist
            };

            var observable = _DiscogsClient.SearchAll(discogsSearch);
            await observable.ForEachAsync(OnResult);
        }

        private void OnResult(DiscogsSearchResult result)
        {
            _Count++;
            Trace.WriteLine($"{_Count} - {result.title}");
        }

        [Fact(Skip = "Please provide valid token and keys to run the test")]
        public async Task GetMasterReleaseVersion()
        {
            var observable = _DiscogsClient.GetMasterReleaseVersion(47813);
            await observable.ForEachAsync(OnResult);
        }

        private void OnResult(DiscogsReleaseVersion result)
        {
            _Count++;
            Trace.WriteLine($"{_Count} - {result.title}");
        }

        [Fact(Skip = "Please provide valid token and keys to run the test")]
        public async Task GetRelease()
        {     
            var res = await _DiscogsClient.GetRelease(1704673);
            res.Should().NotBeNull();
        }

        [Fact(Skip = "Please provide valid token and keys to run the test")]
        public async Task GetMaster() 
        {
            var res = await _DiscogsClient.GetMaster(47813);
            res.Should().NotBeNull();
        }
    }
}
