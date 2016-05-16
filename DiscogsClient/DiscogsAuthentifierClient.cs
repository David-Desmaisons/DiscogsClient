namespace DiscogsClient
{
    public class DiscogsAuthentifierClient 
    {
        private readonly OAuthConsumerInformation _OAuthConsumerInformation;

        public DiscogsAuthentifierClient(OAuthConsumerInformation oAuthConsumerInformation) 
        {
            _OAuthConsumerInformation = oAuthConsumerInformation;
        }
    }
}
