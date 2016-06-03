using Newtonsoft.Json;
using RateLimiter;
using RestSharp;
using RestSharpInfra.OAuth1;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DiscogsClient.Internal
{
    internal class DiscogsWebClient : IDiscogsWebClient
    {
        private const string _ErrorMessage = "";
        private const string _UserAgentFallBack = @"DiscogsClient https://github.com/David-Desmaisons/DiscogsClient";
        private const string _SearchUrl = "database/search";
        private const string _ReleaseUrl = "releases/{releaseId}";
        private const string _MasterUrl = "masters/{masterId}";
        private const string _MasterReleaseVersionUrl = "masters/{masterId}/versions";
        private const string _ArtistUrl = "artists/{artistId}";
        private const string _ArtistReleaseUrl = "artists/{artistId}/releases";
        private readonly TimeLimiter _TimeLimiter;
        private readonly OAuthCompleteInformation _OAuthCompleteInformation;
        private readonly RestClient _Client;

        private string UrlBase => "https://api.discogs.com";

        private string _UserAgent;
        public string UserAgent
        {
            get { return _UserAgent ?? _UserAgentFallBack; }
            set { _UserAgent = value; }
        }

        public DiscogsWebClient(OAuthCompleteInformation oAuthCompleteInformation, int timeOut = 10000)
        {
            _OAuthCompleteInformation = oAuthCompleteInformation;
            _TimeLimiter = TimeLimiter.GetFromMaxCountByInterval(240, TimeSpan.FromMinutes(1));
            _Client = new RestClient(UrlBase)
            {
                UserAgent = _UserAgent,
                Timeout = timeOut,
                Authenticator = _OAuthCompleteInformation?.GetAuthenticatorForProtectedResource()
            };
        }

        public IRestRequest GetSearchRequest()
        {
            return GetRequest(_SearchUrl);
        }

        public IRestRequest GetReleaseRequest(int releaseId)
        {       
            var request = GetRequest(_ReleaseUrl);
            request.AddUrlSegment(nameof(releaseId), releaseId.ToString());
            return request;
        }

        public IRestRequest GetMasterRequest(int masterId) 
        {
            var request = GetRequest(_MasterUrl);
            request.AddUrlSegment(nameof(masterId), masterId.ToString());
            return request;
        }

        public IRestRequest GetMasterReleaseVersion(int masterId)
        {
            var request = GetRequest(_MasterReleaseVersionUrl);
            request.AddUrlSegment(nameof(masterId), masterId.ToString());
            return request;
        }

        public IRestRequest GetArtistRequest(int artistId) 
        {
            var request = GetRequest(_ArtistUrl);
            request.AddUrlSegment(nameof(artistId), artistId.ToString());
            return request;
        }

        public IRestRequest GetArtistReleaseVersion(int artistId) 
        {
            var request = GetRequest(_ArtistReleaseUrl);
            request.AddUrlSegment(nameof(artistId), artistId.ToString());
            return request;
        }

        private IRestRequest GetRequest(string url)
        {
            var request = new RestRequest(url);
            return Finalize(request);
        }

        private IRestRequest Finalize(IRestRequest request)
        {
            request.AddHeader("Accept-Encoding", "gzip");
            return request;
        }

        public async Task<T> Execute<T>(IRestRequest request, CancellationToken cancellationToken)
        {
            var response = await _TimeLimiter.Perform(async () => await ExecuteBasic(request, cancellationToken), cancellationToken);

            if (response.ErrorException != null)
                throw new DiscogsException(_ErrorMessage, response.ErrorException);

            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        private Task<IRestResponse> ExecuteBasic(IRestRequest request, CancellationToken cancellationToken)
        {
            return _Client.ExecuteTaskAsync(request, cancellationToken);
        }
    }
}
