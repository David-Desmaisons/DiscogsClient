using RestSharp;
using System;

namespace RestSharpInfra
{
    public class ClientBuilder
    {
        public static Func<string, IRestClient> Builder
        {
            get; set;
        }

        static ClientBuilder()
        {
            Builder = (url) => new RestClient(url);
        }  
    }
}
