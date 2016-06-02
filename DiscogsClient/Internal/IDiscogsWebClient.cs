using RestSharp;
using System.Threading;
using System.Threading.Tasks;

namespace DiscogsClient.Internal
{
    internal interface IDiscogsWebClient
    {
        IRestRequest GetSearchRequest();

        IRestRequest GetReleaseRequest();

        Task<T> Execute<T>(IRestRequest request, CancellationToken cancellationToken);
    }
}