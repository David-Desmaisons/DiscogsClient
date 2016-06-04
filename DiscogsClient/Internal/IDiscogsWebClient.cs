using RestSharp;
using System.Threading;
using System.Threading.Tasks;

namespace DiscogsClient.Internal
{
    internal interface IDiscogsWebClient
    {
        IRestRequest GetSearchRequest();

        IRestRequest GetReleaseRequest(int relaseId);

        IRestRequest GetMasterRequest(int masterId);

        IRestRequest GetMasterReleaseVersionRequest(int masterId);

        IRestRequest GetArtistRequest(int artistId);

        IRestRequest GetLabelRequest(int artistId);

        IRestRequest GetArtistReleaseVersionRequest(int artistId);

        IRestRequest GetAllLabelReleasesRequest(int labelId);

        Task<T> Execute<T>(IRestRequest request, CancellationToken cancellationToken);   
    }
}