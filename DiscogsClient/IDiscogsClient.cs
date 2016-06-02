using DiscogsClient.Data.Query;
using DiscogsClient.Data.Result;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiscogsClient
{
    public interface IDiscogsClient
    {
        Task<DiscogsSearchResults> Search(DiscogsSearch search);

        Task<DiscogsSearchResults> Search(DiscogsSearch search, CancellationToken token);

        IEnumerable<DiscogsSearchResult> SearchAllEnumerable(DiscogsSearch search, int? maxElement = null);

        IObservable<DiscogsSearchResult> SearchAll(DiscogsSearch search, int? maxElement = null);

        Task<DiscogsRelease> GetRelease(int releaseId);

        Task<DiscogsRelease> GetRelease(int releaseId, CancellationToken token);

        //Task<DiscogsRelease> GetRelease(DiscogsSearchResult result, CancellationToken token);
    }
}