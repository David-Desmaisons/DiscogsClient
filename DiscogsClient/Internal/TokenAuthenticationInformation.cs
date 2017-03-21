namespace DiscogsClient.Internal
{
    public class TokenAuthenticationInformation
    {
        public string Token { get; set; }
        private string _SecretToken;

        public TokenAuthenticationInformation(string token)
        {
            Token = token;
            _SecretToken = $"Discogs token={token}";
        }

        public string GetDiscogsSecretToken()
        {
            return _SecretToken;
        }
    }   
}
