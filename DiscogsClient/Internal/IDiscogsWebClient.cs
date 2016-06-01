using RestSharp;
using System.Threading;
using System.Threading.Tasks;

namespace DiscogsClient.Internal
{
    internal interface IDiscogsWebClient
    {
        IRestRequest GetSearchRequest();

        IRestRequest GetRequest(string url);

        Task<T> Execute<T>(IRestRequest request, CancellationToken cancellationToken);
    }
}