using Newtonsoft.Json;
using RateLimiter;
using RestSharp;
using RestSharpInfra.OAuth1;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace DiscogsClient.Internal
{
    internal class DiscogsWebClient : IDiscogsWebClient
    {
        private const string _ErrorMessage = "Error During Request Processing";
        private const string _UserAgentFallBack = @"DiscogsClient https://github.com/David-Desmaisons/DiscogsClient";
        private const string _SearchUrl = "database/search";
        private const string _ReleaseUrl = "releases/{releaseId}";
        private const string _MasterUrl = "masters/{masterId}";
        private const string _MasterReleaseVersionUrl = "masters/{masterId}/versions";
        private const string _ArtistUrl = "artists/{artistId}";
        private const string _ArtistReleaseUrl = "artists/{artistId}/releases";
        private const string _AllLabelReleasesUrl = "labels/{labelId}/releases";
        private const string _LabeltUrl = "labels/{labelId}";
        private const string _ReleaseRatingByUserUrl = "releases/{releaseId}/rating/{userName}";
        private const string _CommunityReleaseRatingUrl = "releases/{releaseId}/rating";
        private const string _IdendityUrl = "oauth/identity";
        private readonly OAuthCompleteInformation _OAuthCompleteInformation;
        private readonly IRestClient _Client;

        private static TimeLimiter TimeLimiter { get; set; }

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
            _Client = GetClient(UrlBase, timeOut);
        }

        static DiscogsWebClient()
        {
            TimeLimiter = TimeLimiter.GetFromMaxCountByInterval(240, TimeSpan.FromMinutes(1));
        }

        private IRestClient GetClient(string UrlBase, int timeOut=10000)
        {
            return new RestClient(UrlBase)
            {
                UserAgent = _UserAgent,
                Timeout = timeOut,
                Authenticator = _OAuthCompleteInformation?.GetAuthenticatorForProtectedResource()
            };
        }

        public IRestRequest GetUserIdentityRequest()
        {
            return GetRequest(_IdendityUrl);
        }

        public IRestRequest GetSearchRequest()
        {
            return GetRequest(_SearchUrl);
        }

        public IRestRequest GetReleaseRequest(int releaseId)
        {       
            return GetRequest(_ReleaseUrl).AddUrlSegment(nameof(releaseId), releaseId.ToString());
        }

        public IRestRequest GetMasterRequest(int masterId) 
        {
            return GetRequest(_MasterUrl).AddUrlSegment(nameof(masterId), masterId.ToString());
        }

        public IRestRequest GetMasterReleaseVersionRequest(int masterId)
        {
            return GetRequest(_MasterReleaseVersionUrl).AddUrlSegment(nameof(masterId), masterId.ToString());
        }

        public IRestRequest GetArtistRequest(int artistId) 
        {
            return GetRequest(_ArtistUrl).AddUrlSegment(nameof(artistId), artistId.ToString());
        }

        public IRestRequest GetLabelRequest(int labelId) 
        {
            return GetRequest(_LabeltUrl).AddUrlSegment(nameof(labelId), labelId.ToString());
        }

        public IRestRequest GetArtistReleaseVersionRequest(int artistId) 
        {
            return GetRequest(_ArtistReleaseUrl).AddUrlSegment(nameof(artistId), artistId.ToString());
        }

        public IRestRequest GetAllLabelReleasesRequest(int labelId)
        {
            return GetRequest(_AllLabelReleasesUrl).AddUrlSegment(nameof(labelId), labelId.ToString());
        }

        public IRestRequest GetGetUserReleaseRatingRequest(string userName, int releaseId)
        {
            return GetUserReleaseRatingRequestRaw(userName, releaseId, Method.GET);
        }

        public IRestRequest GetPutUserReleaseRatingRequest(string userName, int releaseId)
        {
            return GetUserReleaseRatingRequestRaw(userName, releaseId, Method.PUT);
        }

        public IRestRequest GetDeleteUserReleaseRatingRequest(string userName, int releaseId)
        {
            return GetUserReleaseRatingRequestRaw(userName, releaseId, Method.DELETE);
        }

        private IRestRequest GetUserReleaseRatingRequestRaw(string userName, int releaseId, Method method)
        {
            return GetRequest(_ReleaseRatingByUserUrl, method)
                       .AddUrlSegment(nameof(userName), userName)
                       .AddUrlSegment(nameof(releaseId), releaseId.ToString());
        }

        public IRestRequest GetCommunityReleaseRatingRequest(int releaseId)
        {
            return GetRequest(_CommunityReleaseRatingUrl).AddUrlSegment(nameof(releaseId), releaseId.ToString());
        }

        private IRestRequest GetRequest(string url, Method method = Method.GET)
        {
            return new RestRequest(url, method).AddHeader("Accept-Encoding", "gzip");
        }

        public async Task<T> Execute<T>(IRestRequest request, CancellationToken cancellationToken)
        {
            var response = await GetResponse(request, cancellationToken);
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public async Task<HttpStatusCode> Execute(IRestRequest request, CancellationToken cancellationToken)
        {
            var response = await GetResponse(request, cancellationToken);
            return response.StatusCode;
        }

        public async Task Download(string url, Stream copyStream, CancellationToken cancellationToken)
        {
            var client = GetClient(url, 15000);
            var request = new RestRequest(Method.GET) 
            {
                ResponseWriter = (stream) => stream.CopyTo(copyStream)
            };
            await GetResponse(request, cancellationToken, client);
        }

        private async Task<IRestResponse> GetResponse(IRestRequest request, CancellationToken cancellationToken, IRestClient client = null)
        {
            client = client ?? _Client;
            var response = await TimeLimiter.Perform(async () => await ExecuteBasic(client, request, cancellationToken), cancellationToken);

            if (response.ErrorException != null)
                throw new DiscogsException(_ErrorMessage, response.ErrorException);

            return response;
        }

        private static Task<IRestResponse> ExecuteBasic(IRestClient client, IRestRequest request, CancellationToken cancellationToken)
        {
            return client.ExecuteTaskAsync(request, cancellationToken);
        }
    }
}
