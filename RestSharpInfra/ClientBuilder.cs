using RestSharp;
using System;

namespace RestSharpInfra
{
    public class ClientBuilder
    {
        public static Func<string, IRestClient> Build
        {
            get; set;
        }

        static ClientBuilder()
        {
            Build = (url) => new RestClient(url);
        }  
    }
}
