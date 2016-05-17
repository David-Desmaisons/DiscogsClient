using RestSharp;
using RestSharpInfra.OAuth1;

namespace DiscogsClient
{
   public class DiscogsClient 
   {
        private const string _UserAgent = @"DiscogsClient https://github.com/David-Desmaisons/DiscogsClient";
        private const string _UrlBase = "https://api.discogs.com/";
        private const string _SearchUrl = "/database/search";

        private readonly OAuthCompleteInformation _OAuthCompleteInformation;
        private readonly RestClient _Client;

        public DiscogsClient(OAuthCompleteInformation oAuthCompleteInformation, int timeOut=5000) 
        {
            _OAuthCompleteInformation = oAuthCompleteInformation;
            _Client = new RestClient(_UrlBase)
            {
                UserAgent = _UserAgent,
                Timeout = timeOut,
                Authenticator = _OAuthCompleteInformation.GetAuthenticatorForProtectedResource()
            };
        }

        private IRestRequest Finalize(IRestRequest request)
        {
            request.AddHeader("Accept-Encoding", "gzip");
            return request;
        }
    }
}
