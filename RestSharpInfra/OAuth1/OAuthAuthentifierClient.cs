using System.Net;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Extensions.MonoHttp;

namespace RestSharpInfra.OAuth1
{
    public abstract class OAuthAuthentifierClient 
    {
        private const string _RequestToken = "oauth/request_token";
        private const string _AccessToken = "oauth/access_token";
        private const string _Authorize = "oauth/authorize";
        private const string _Token = "oauth_token";
        private const string _TokenSecret = "oauth_token_secret";

        private readonly OAuthConsumerInformation _OAuthConsumerInformation;
        protected abstract string RequestUrl { get; }
        protected abstract string AuthorizeUrl { get; }

        public OAuthCompleteInformation OAuthCompleteInformation { get; private set; }
   
        protected OAuthAuthentifierClient(OAuthConsumerInformation oAuthConsumerInformation) 
        {
            _OAuthConsumerInformation = oAuthConsumerInformation;
        }

        public async Task<string> GetAuthorizeUrl() 
        {
            var completeInformation = await RequestToken();
            if (completeInformation == null)
                return null;

            var authorizeTokenClient = GetRequestTokenClient(AuthorizeUrl);
            var request = new RestRequest(_Authorize);
            request.AddParameter(_Token, OAuthCompleteInformation.Token);

            return authorizeTokenClient.BuildUri(request).ToString();
        }

        private async Task<OAuthCompleteInformation> RequestToken() 
        {
            if (OAuthCompleteInformation != null)
                return OAuthCompleteInformation;

            var requestTokenClient = GetRequestTokenClient(RequestUrl);
            var request = new RestRequest(_RequestToken, Method.POST);
            var response = await requestTokenClient.ExecuteTaskAsync(request);

            if ((response == null) || (response.StatusCode != HttpStatusCode.OK))
                return null;

            var qs = HttpUtility.ParseQueryString(response.Content);
            var oauthToken = qs[_Token];
            var oauthTokenSecret = qs[_TokenSecret];

            if ((oauthToken == null) || (oauthTokenSecret ==  null))
                return null;

            return OAuthCompleteInformation = new OAuthCompleteInformation(_OAuthConsumerInformation, oauthToken, oauthTokenSecret);
        }

        public async Task<bool> Access(string verifier) 
        {
            if (OAuthCompleteInformation == null)
                return false;

            var accessTokenClient = GetAccessTokenClient(RequestUrl, verifier);      
            var request = new RestRequest(_AccessToken, Method.POST);
            var response = await accessTokenClient.ExecuteTaskAsync(request);

            ////qs = HttpUtility.ParseQueryString(response.Content);
            //    //oauthToken = qs["oauth_token"];
            //    //oauthTokenSecret = qs["oauth_token_secret"];

            return (response != null) && (response.StatusCode == HttpStatusCode.OK);
        }

        private IRestClient GetRequestTokenClient(string baseUrl) 
        {
            return new RestClient(baseUrl) 
            {
                Authenticator = OAuth1Authenticator.ForRequestToken(_OAuthConsumerInformation.ConsumerKey, _OAuthConsumerInformation.ConsumerSecret)
            };
        }

        private IRestClient GetAccessTokenClient(string baseUrl, string verifier) 
        {
            var info = OAuthCompleteInformation.ConsumerInformation;
            return new RestClient(baseUrl)
            {
                Authenticator = OAuth1Authenticator.ForAccessToken(info.ConsumerKey, info.ConsumerSecret, OAuthCompleteInformation.Token, OAuthCompleteInformation.TokenSecret, verifier)
            };
        }
    }
}
