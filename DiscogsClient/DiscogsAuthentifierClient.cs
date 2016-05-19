using RestSharpInfra.OAuth1;

namespace DiscogsClient
{
    public class DiscogsAuthentifierClient : OAuthAuthentifierClient
    {
        protected override string RequestUrl 
        {
            get { return @"https://api.discogs.com/"; }
        }

        protected override string AuthorizeUrl
        {
            get { return @"https://www.discogs.com/"; }
        }

        public DiscogsAuthentifierClient(OAuthConsumerInformation consumerInformation) : base(consumerInformation)
        {
        }
    }
}
