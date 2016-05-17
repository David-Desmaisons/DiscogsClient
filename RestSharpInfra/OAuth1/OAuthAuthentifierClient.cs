using System.Net;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Extensions.MonoHttp;
using System;

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
        private OAuthCompleteInformation _OAuthCompleteInformation;

        protected abstract string RequestUrl { get; }
        protected abstract string AuthorizeUrl { get; }
   
        protected OAuthAuthentifierClient(OAuthConsumerInformation oAuthConsumerInformation) 
        {
            _OAuthConsumerInformation = oAuthConsumerInformation;
        }

        public async Task<OAuthCompleteInformation> Authorize(Func<string, Task<string>> ExtractVerifier)
        {
            if (_OAuthCompleteInformation != null)
                return _OAuthCompleteInformation;

            var token = await RequestToken();
            if (token == null)
                return null;

            if (_OAuthCompleteInformation!=null)
                return _OAuthCompleteInformation;

            var url = GetAuthorizeUrl(token);

            var verifier = await ExtractVerifier(url);         
            return _OAuthCompleteInformation = await Access(verifier, token);
        }

        private async Task<string> RequestToken()
        {
            var requestTokenClient = GetRequestTokenClient(RequestUrl);
            var request = new RestRequest(_RequestToken, Method.POST);
            var response = await requestTokenClient.ExecuteTaskAsync(request);

            if ((response == null) || (response.StatusCode != HttpStatusCode.OK))
                return null;

            var qs = HttpUtility.ParseQueryString(response.Content);
            var oauthToken = qs[_Token];
            var oauthTokenSecret = qs[_TokenSecret];

            if ((oauthToken != null) && (oauthTokenSecret != null))
                _OAuthCompleteInformation = new OAuthCompleteInformation(_OAuthConsumerInformation, oauthToken, oauthTokenSecret);

            return oauthToken;
        }

        private string GetAuthorizeUrl(string token)
        {
            var authorizeTokenClient = GetRequestTokenClient(AuthorizeUrl);
            var request = new RestRequest(_Authorize);
            request.AddParameter(_Token, token);
            return authorizeTokenClient.BuildUri(request).ToString();
        }

        private async Task<OAuthCompleteInformation> Access(string verifier, string token) 
        {
            var accessTokenClient = GetAccessTokenClient(RequestUrl, token, verifier);      
            var request = new RestRequest(_AccessToken, Method.POST);
            var response = await accessTokenClient.ExecuteTaskAsync(request);

            if ((response == null) || (response.StatusCode != HttpStatusCode.OK))
                return null;

            var qs = HttpUtility.ParseQueryString(response.Content);
            var oauthToken = qs[_Token];
            var oauthTokenSecret = qs[_TokenSecret];

            if ((oauthToken == null) || (oauthTokenSecret == null))
                return null;

            return new OAuthCompleteInformation(_OAuthConsumerInformation, oauthToken, oauthTokenSecret);
        }

        private IRestClient GetRequestTokenClient(string baseUrl) 
        {
            var client = ClientBuilder.Builder(baseUrl);
            client.Authenticator = OAuth1Authenticator.ForRequestToken(_OAuthConsumerInformation.ConsumerKey, _OAuthConsumerInformation.ConsumerSecret);
            return client;
        }

        private IRestClient GetAccessTokenClient(string baseUrl, string token, string verifier) 
        {
            var info = _OAuthCompleteInformation.ConsumerInformation;
            var client = ClientBuilder.Builder(baseUrl);
            client.Authenticator = OAuth1Authenticator.ForAccessToken(info.ConsumerKey, info.ConsumerSecret, token, null, verifier);
            return client;
        }
    }
}
