using RestSharp;
using RestSharpInfra.OAuth1;

namespace DiscogsClient
{
   public class DiscogsClient 
   {
        private readonly OAuthCompleteInformation _OAuthCompleteInformation;
        private const string _UrlBase = "https://api.discogs.com/";
        private readonly RestClient _Client;

        public DiscogsClient(OAuthCompleteInformation oAuthCompleteInformation) 
        {
            _OAuthCompleteInformation = oAuthCompleteInformation;
            _Client = new RestClient(_UrlBase);
        }



        //request.Headers["Accept-Encoding"] = "gzip";
        //    if (string.IsNullOrEmpty(request.UserAgent))
        //        request.UserAgent = _UA;
        //    if (_TimeOut!=null)
        //        request.Timeout = _TimeOut.Value;
    }
}
