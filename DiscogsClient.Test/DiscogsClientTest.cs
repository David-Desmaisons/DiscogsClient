using DiscogsClient.Data.Query;
using DiscogsClient.Data.Result;
using FluentAssertions;
using System.Diagnostics;
using System.IO;
using System.Reactive.Linq;
using System.Threading.Tasks;
using RestSharpHelper.OAuth1;
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
            _OAuthCompleteInformation = null;
                //new OAuthCompleteInformation("", "", "", "");
            _DiscogsClient = new DiscogsClient(_OAuthCompleteInformation);
        }

        [Fact(Skip = "Need internet access and valid token and keys.")]
        public async Task Search_Release()
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
        public async Task GetUserIdentity()
        {
            var res = await _DiscogsClient.GetUserIdentity();
            res.Should().NotBeNull();
        }

        [Fact(Skip = "Need internet access and valid token and keys.")]
        public async Task SetUserReleaseRating()
        {
            var res = await _DiscogsClient.SetUserReleaseRating(488973, 5);
            res.Should().NotBeNull();
        }

        [Fact(Skip = "Need internet access and valid token and keys.")]
        public async Task DeleteUserReleaseRating()
        {
            var res = await _DiscogsClient.DeleteUserReleaseRating(488973);
            res.Should().BeTrue();
        }

        [Fact(Skip = "Need internet access and valid token and keys.")]
        public async Task Search_Artist()
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
        public async Task GetMasterReleaseVersion()
        {
            var observable = _DiscogsClient.GetMasterReleaseVersions(47813);
            await observable.ForEachAsync(OnResult);
        }

        private void OnResult(DiscogsReleaseVersion result)
        {
            _Count++;
            Trace.WriteLine($"{_Count} - {result.title}");
        }

        [Fact(Skip = "Need internet access.")]
        public async Task GetRelease()
        {     
            var res = await _DiscogsClient.GetRelease(1704673);
            res.Should().NotBeNull();
        }

        [Fact(Skip = "Need internet access.")]
        public async Task GetMaster() 
        {
            var res = await _DiscogsClient.GetMaster(47813);
            res.Should().NotBeNull();
        }

        [Fact (Skip = "Need internet access.")]
        public async Task GetArtist() 
        { 
            var res = await _DiscogsClient.GetArtist(224506);
            res.Should().NotBeNull();
        }

        [Fact(Skip = "Need internet access.")]
        public async Task GetArtistRelease() 
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
        public async Task GetLabel()
        {
            var res = await _DiscogsClient.GetLabel(125);
            res.Should().NotBeNull();
        }

        [Fact(Skip = "Need internet access.")]
        public async Task GetAllLabelReleases()
        {
            var observable = _DiscogsClient.GetAllLabelReleases(26557);
            await observable.ForEachAsync(OnResult);
        }

        private void OnResult(DiscogsLabelRelease result) 
        {
            _Count++;
            Trace.WriteLine($"{_Count} - {result.title}");
        }

        [Fact(Skip = "Need internet access.")]
        public async Task GetUserReleaseRating()
        {
            var res = await _DiscogsClient.GetUserReleaseRating("andersinstockholm", 488973);
            res.Should().NotBeNull();
        }

        [Fact(Skip = "Need internet access.")]
        public async Task GetCommunityReleaseRating()
        {
            var res = await _DiscogsClient.GetCommunityReleaseRating(488973);
            res.Should().NotBeNull();
        }

        [Fact(Skip = "Need internet access and valid token and keys.")]
        public async Task SaveImage()
        {
            var res = await _DiscogsClient.GetMaster(47813);
            res.Should().NotBeNull();

            await _DiscogsClient.SaveImage(res.images[0], Path.GetTempPath(), "Ornette-TSOAJTC");
        }
    }
}
