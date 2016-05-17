using RestSharp.Authenticators;

namespace RestSharpInfra.OAuth1 
{
    public class OAuthCompleteInformation 
    {
        public OAuthConsumerInformation ConsumerInformation { get; private set; }
        public OAuthTokenInformation TokenInformation { get; private set; }

        public OAuthCompleteInformation(string consumerKey, string consumerSecret, string token, string tokenSecret): 
            this(new OAuthConsumerInformation(consumerKey, consumerSecret), token, tokenSecret)
        {
        }

        public OAuthCompleteInformation(OAuthConsumerInformation consumerInformation, string token, string tokenSecret):
            this(consumerInformation, new OAuthTokenInformation(token, tokenSecret))
        {
        }

        public OAuthCompleteInformation(OAuthConsumerInformation consumerInformation, OAuthTokenInformation oAuthTokenInformation) 
        {
            ConsumerInformation = consumerInformation;
            TokenInformation = oAuthTokenInformation;
        }

        public OAuth1Authenticator GetAuthenticatorForProtectedResource() 
        {
            return OAuth1Authenticator.ForProtectedResource(ConsumerInformation.ConsumerKey, ConsumerInformation.ConsumerSecret, 
                TokenInformation.Token, TokenInformation.TokenSecret);
        }

        public OAuth1Authenticator GetAuthenticatorForAccessToken(string verifier) 
        {
            return OAuth1Authenticator.ForAccessToken(ConsumerInformation.ConsumerKey, ConsumerInformation.ConsumerSecret, TokenInformation.Token, TokenInformation.TokenSecret, verifier);
        }
    }
}
