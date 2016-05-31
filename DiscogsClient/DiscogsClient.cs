using DiscogsClient.Data.Query;
using DiscogsClient.Data.Result;
using DiscogsClient.Internal;
using RestSharpInfra;
using RestSharpInfra.OAuth1;
using System.Threading;
using System.Threading.Tasks;

namespace DiscogsClient
{
    public class DiscogsClient
    {
        private readonly DiscogsWebClient _Client;

        public DiscogsClient(OAuthCompleteInformation oAuthCompleteInformation, int timeOut = 5000)
        {
            _Client = new DiscogsWebClient(oAuthCompleteInformation, timeOut);
        }

        public Task<DiscogsSearchResults> Search<T>(DiscogsSearch search) where T : DiscogsEntity
        {
            return Search<T>(search, CancellationToken.None);
        }

        public async Task<DiscogsSearchResults> Search<T>(DiscogsSearch search, CancellationToken token) where T : DiscogsEntity
        {
            var request = _Client.GetSearchRequest();
            request.AddAsParameter(search);
            return  await _Client.Execute<DiscogsSearchResults>(request, token);
        }
    }
}
