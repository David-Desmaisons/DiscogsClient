using RateLimiter;
using RestSharp;
using System;
using RestSharpHelper;
using RestSharpHelper.OAuth1;

namespace DiscogsClient.Internal
{
    internal class DiscogsWebClient : RestSharpWebClient, IDiscogsWebClient
    {
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
        private readonly TokenAuthenticationInformation _tokenAuthenticationInformation;

        protected override string UrlBase => "https://api.discogs.com";
        protected override string UserAgentFallBack => @"DiscogsClient https://github.com/David-Desmaisons/DiscogsClient";
        protected override TimeLimiter TimeLimiter => SharedTimeLimiter;

        private static TimeLimiter SharedTimeLimiter { get; }

        public DiscogsWebClient(OAuthCompleteInformation oAuthCompleteInformation, string userAgent, int timeOut = 10000):
            base(userAgent, timeOut)
        {
            _OAuthCompleteInformation = oAuthCompleteInformation;
        }

        public DiscogsWebClient(TokenAuthenticationInformation tokenAuthenticationInformation, string userAgent, int timeOut = 10000)
            : base(userAgent, timeOut)
        {
            _tokenAuthenticationInformation = tokenAuthenticationInformation;
        }

        static DiscogsWebClient()
        {
            SharedTimeLimiter = TimeLimiter.GetFromMaxCountByInterval(240, TimeSpan.FromMinutes(1));
        }

        protected override IRestClient Mature(IRestClient client) 
        {
            client.Authenticator = _OAuthCompleteInformation?.GetAuthenticatorForProtectedResource();
            return client;
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
            var request = new RestRequest(url, method).AddHeader("Accept-Encoding", "gzip");
            if (_tokenAuthenticationInformation != null)
            {
                request.AddHeader("Authorization", _tokenAuthenticationInformation.GetDiscogsSecretToken());
            }
            return request;
        }
    }
}
