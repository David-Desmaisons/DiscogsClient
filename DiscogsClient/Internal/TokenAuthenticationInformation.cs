using System;

namespace DiscogsClient.Internal
{
    public class TokenAuthenticationInformation
    {
        public string Token { get; set; }
        private string _secretToken;

        public TokenAuthenticationInformation(string token)
        {
            this.Token = token;
            _secretToken = $"Discogs token={token}";
        }

        public string GetDiscogsSecretToken()
        {
            return _secretToken;
        }
    }   
}
