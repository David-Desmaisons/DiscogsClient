using DiscogsClient;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using RestSharpHelper.OAuth1;

namespace DiscogsAuthenticationConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var oAuthConsumerInformation = new OAuthConsumerInformation("", "");
            var discogsClient = new DiscogsAuthentifierClient(oAuthConsumerInformation);

            var aouth = discogsClient.Authorize(s => Task.FromResult(GetToken(s))).Result;

            Console.WriteLine($"{((aouth != null)? "Success": "Fail")}");
            Console.WriteLine($"Token:{aouth?.TokenInformation?.Token}, Token:{aouth?.TokenInformation?.TokenSecret}");
        }

        private static string GetToken(string url)
        {
            Console.WriteLine("Please authourize the application and enter the final key in the console");
            Process.Start(url);
            string tokenKey = Console.ReadLine();
            tokenKey = string.IsNullOrEmpty(tokenKey) ? null : tokenKey;
            return tokenKey;
        }
    }
}
