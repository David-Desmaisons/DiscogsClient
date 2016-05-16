using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscogsClient
{
    public class DiscogsClient
    {
        private const string _UrlBase = "http://api.discogs.com/";
        private readonly string _OauthToken;
        private readonly string _OauthTokenSecret;
        //private readonly string
        //private readonly string

        public DiscogsClient(string oauthToken, string oauthTokenSecret) 
        {
            _OauthToken = oauthToken;
            _OauthTokenSecret = oauthTokenSecret;
        }

    }
}
