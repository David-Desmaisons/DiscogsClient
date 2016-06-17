namespace RestSharpInfra.OAuth1 
{
    public class OAuthTokenInformation 
    {
        public string Token { get; }
        public string TokenSecret { get; }

        public OAuthTokenInformation(string token, string tokenSecret) 
        {
            Token = token;
            TokenSecret = tokenSecret;
        }

        public bool PartialOrValid 
        {
            get { return Token != null; }
        }

        public bool Valid 
        {
            get { return ((Token != null) && (TokenSecret != null)); }
        }
    }
}
