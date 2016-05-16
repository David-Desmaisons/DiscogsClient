namespace RestSharpInfra.OAuth1 
{
    public class OAuthConsumerInformation 
    {
        public string ConsumerKey { get; private set; }
        public string ConsumerSecret { get; private set; }
        public OAuthConsumerInformation(string consumerKey, string consumerSecret) 
        {
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
        }
    }
}
