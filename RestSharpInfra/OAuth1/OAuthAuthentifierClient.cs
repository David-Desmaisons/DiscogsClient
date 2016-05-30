using System.Net;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Extensions.MonoHttp;
using System;

namespace RestSharpInfra.OAuth1
{
    public abstract class OAuthAuthentifierClient : IOAuthAuthentifierClient
    {
        private const string _RequestTokenUrl = "oauth/request_token";
        private const string _AccessTokenUrl = "oauth/access_token";
        private const string _AuthorizeUrl = "oauth/authorize";
        private const string _Token = "oauth_token";
        private const string _TokenSecret = "oauth_token_secret";

        private readonly OAuthConsumerInformation _ConsumerInformation;
        private OAuthTokenInformation _TokenInformation;
        private OAuthCompleteInformation _CompleteInformation;

        protected abstract string RequestUrl { get; }
        protected abstract string AuthorizeUrl { get; }

        private bool TokenIsPartialOrValid
        {
            get { return ((_TokenInformation != null) && (_TokenInformation.PartialOrValid)); }
        }

        private bool TokenIsValid
        {
            get { return ((_TokenInformation != null) && (_TokenInformation.Valid)); }
        }

        protected OAuthAuthentifierClient(OAuthConsumerInformation consumerInformation) 
        {
            _ConsumerInformation = consumerInformation;
        }

        public async Task<OAuthCompleteInformation> AuthorizeImplicitelly() 
        {
            if (_CompleteInformation != null)
                return _CompleteInformation;

            await RequestToken();

            return _CompleteInformation;
        }

        private async Task RequestToken() 
        {
            if (_TokenInformation.PartialOrValid)
                return;

            var requestTokenClient = GetRequestTokenClient(RequestUrl);
            _TokenInformation = await GetTokenInformationFromRequest(requestTokenClient, _RequestTokenUrl);

            if (!TokenIsValid)
                return;

            _CompleteInformation = new OAuthCompleteInformation(_ConsumerInformation, _TokenInformation);
        }

        public async Task<OAuthCompleteInformation> AuthorizeExplicitelly(Func<string, Task<string>> extractVerifier) 
        {
            var res = await AuthorizeImplicitelly();
            if (res != null)
                return res;

            if (!TokenIsPartialOrValid)
                return null;

            var url = GetAuthorizeUrl();
            var verifier = await extractVerifier(url);

            return _CompleteInformation = await Access(verifier);
        }

        private string GetAuthorizeUrl()
        {
            var authorizeTokenClient = GetRequestTokenClient(AuthorizeUrl);
            var request = new RestRequest(_AuthorizeUrl);
            request.AddParameter(_Token, _TokenInformation.Token);
            return authorizeTokenClient.BuildUri(request).ToString();
        }

        private async Task<OAuthCompleteInformation> Access(string verifier) 
        {
            if (verifier == null)
                return null;

            var accessTokenClient = GetAccessTokenClient(RequestUrl, verifier);
            var tokenInformation = await GetTokenInformationFromRequest(accessTokenClient, _AccessTokenUrl);

            if ((tokenInformation== null) || (!tokenInformation.Valid))
                return null;

            return new OAuthCompleteInformation(_ConsumerInformation, tokenInformation);
        }

        public async Task<OAuthTokenInformation> GetTokenInformationFromRequest(IRestClient client, string relativeUrl) 
        {
            var request = new RestRequest(relativeUrl, Method.POST);
            var response = await client.ExecuteTaskAsync(request);

            return !CheckResponse(response) ? null : GetTokenInformationFromBodyResponse(response);
        }

        private OAuthTokenInformation GetTokenInformationFromBodyResponse(IRestResponse response) 
        {
            var qs = HttpUtility.ParseQueryString(response.Content);
            return new OAuthTokenInformation(qs[_Token], qs[_TokenSecret]);
        }

        private bool CheckResponse(IRestResponse response) 
        {
            return ((response != null) && (response.StatusCode == HttpStatusCode.OK));
        }

        private IRestClient GetRequestTokenClient(string baseUrl) 
        {
            var client = ClientBuilder.Build(baseUrl);
            client.Authenticator = _ConsumerInformation.GetAuthenticatorForRequestToken();
            return client;
        }

        private IRestClient GetAccessTokenClient(string baseUrl, string verifier) 
        {
            var client = ClientBuilder.Build(baseUrl);
            client.Authenticator = _CompleteInformation.GetAuthenticatorForAccessToken(verifier);
            return client;
        }
    }
}
