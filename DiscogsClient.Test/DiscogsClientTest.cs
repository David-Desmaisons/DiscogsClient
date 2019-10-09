using DiscogsClient.Data.Query;
using DiscogsClient.Data.Result;
using FluentAssertions;
using System.Diagnostics;
using System.IO;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DiscogsClient.Internal;
using RestSharpHelper.OAuth1;
using Xunit;
using Xunit.Abstractions;

namespace DiscogsClient.Test
{
    public class DiscogsClientTest
    {
        private readonly DiscogsClient _DiscogsClient;
        private readonly ITestOutputHelper _TestOutputHelper;
        private int _Count;
        private const string Token = "";

        public DiscogsClientTest(ITestOutputHelper testOutputHelper) 
        {
            _TestOutputHelper = testOutputHelper;
            var tokenInformation = new TokenAuthenticationInformation(Token);
            _DiscogsClient = new DiscogsClient(tokenInformation);
        }

        [Fact(Skip = "Need internet access and valid token and keys.")]
        public async Task Search()
        {
            var discogsSearch = new DiscogsSearch()
            {
                artist = "Ornette Coleman",
                release_title = "The Shape Of Jazz To Come"
            };

            var observable = _DiscogsClient.Search(discogsSearch);
            await observable.ForEachAsync(OnResult);
        }

        [Fact(Skip = "Need internet access and valid token and keys.")]
        public async Task SearchAsync()
        {
            var discogsSearch = new DiscogsSearch()
            {
                artist = "Ornette Coleman",
                release_title = "The Shape Of Jazz To Come"
            };

            var res = await _DiscogsClient.SearchAsync(discogsSearch);
            foreach (var searchResult in res.GetResults())
            {
                OnResult(searchResult);
            }
        }

        [Fact(Skip = "Need internet access and valid token and keys.")]
        public async Task GetUserIdentityAsync()
        {
            var res = await _DiscogsClient.GetUserIdentityAsync();
            res.Should().NotBeNull();
        }

        [Fact(Skip = "Need internet access and valid token and keys.")]
        public async Task SetUserReleaseRatingAsync()
        {
            var res = await _DiscogsClient.SetUserReleaseRatingAsync(488973, 5);
            res.Should().NotBeNull();
        }

        [Fact(Skip = "Need internet access and valid token and keys.")]
        public async Task DeleteUserReleaseRatingAsync()
        {
            var res = await _DiscogsClient.DeleteUserReleaseRatingAsync(488973);
            res.Should().BeTrue();
        }

        [Fact(Skip = "Need internet access and valid token and keys.")]
        public async Task Search_ArtistAsync()
        {
            var discogsSearch = new DiscogsSearch()
            {
                query = "Ornette Coleman",
                type = DiscogsEntityType.artist
            };

            var observable = _DiscogsClient.Search(discogsSearch);
            await observable.ForEachAsync(OnResult);
        }

        private void OnResult(DiscogsSearchResult result)
        {
            _Count++;
            Trace.WriteLine($"{_Count} - {result.title}");
        }

        [Fact(Skip = "Need internet access.")]
        public async Task GetMasterReleaseVersions()
        {
            var observable = _DiscogsClient.GetMasterReleaseVersions(47813);
            await observable.ForEachAsync(OnResult);
        }

        [Fact(Skip = "Need internet access.")]
        public async Task GetMasterReleaseVersionAsync()
        {
            var res = await _DiscogsClient.GetMasterReleaseVersionsAsync(47813);
            foreach (var searchResult in res.GetResults())
            {
                OnResult(searchResult);
            }
        }

        private void OnResult(DiscogsReleaseVersion result)
        {
            _Count++;
            Trace.WriteLine($"{_Count} - {result.title}");
        }

        [Fact(Skip = "Need internet access.")]
        public async Task GetReleaseAsync()
        {     
            var res = await _DiscogsClient.GetReleaseAsync(1704673);
            res.Should().NotBeNull();
        }

        [Fact(Skip = "Need internet access.")]
        public async Task GetMasterAsync() 
        {
            var res = await _DiscogsClient.GetMasterAsync(47813);
            res.Should().NotBeNull();
        }

        [Fact (Skip = "Need internet access.")]
        public async Task GetArtistAsync() 
        { 
            var res = await _DiscogsClient.GetArtistAsync(224506);
            res.Should().NotBeNull();
        }

        [Fact(Skip = "Need internet access.")]
        public async Task GetArtistReleaseAsync() 
        {
            var observable = _DiscogsClient.GetArtistRelease(200818);
            await observable.ForEachAsync(OnResult);
        }

        private void OnResult(DiscogsArtistRelease result)
        {
            _Count++;
            Trace.WriteLine($"{_Count} - {result.title}");
        }

        [Fact(Skip = "Need internet access.")]
        public async Task GetLabelAsync()
        {
            var res = await _DiscogsClient.GetLabelAsync(125);
            res.Should().NotBeNull();
        }

        [Fact(Skip = "Need internet access.")]
        public async Task GetAllLabelReleases()
        {
            var observable = _DiscogsClient.GetAllLabelReleases(26557);
            await observable.ForEachAsync(OnResult);
        }

        [Fact(Skip = "Need internet access.")]
        public async Task GetAllLabelReleasesAsync()
        {
            var res = await _DiscogsClient.GetAllLabelReleasesAsync(26557);
            foreach (var searchResult in res.GetResults())
            {
                OnResult(searchResult);
            }
        }

        private void OnResult(DiscogsLabelRelease result) 
        {
            _Count++;
            _TestOutputHelper.WriteLine($"{_Count} - {result.title}");
        }

        [Fact(Skip = "Need internet access.")]
        public async Task GetUserReleaseRatingAsync()
        {
            var res = await _DiscogsClient.GetUserReleaseRatingAsync("andersinstockholm", 488973);
            res.Should().NotBeNull();
        }

        [Fact(Skip = "Need internet access.")]
        public async Task GetCommunityReleaseRatingAsync()
        {
            var res = await _DiscogsClient.GetCommunityReleaseRatingAsync(488973);
            res.Should().NotBeNull();
        }

        [Fact(Skip = "Need internet access and valid token and keys.")]
        public async Task SaveImageAsync()
        {
            var res = await _DiscogsClient.GetMasterAsync(47813);
            res.Should().NotBeNull();

            await _DiscogsClient.SaveImageAsync(res.images[0], Path.GetTempPath(), "Ornette-TSOAJTC");
        }
    }
}
