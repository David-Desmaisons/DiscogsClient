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

        IEnumerable<DiscogsSearchResult> SearchAllEnumerable(DiscogsSearch search);

        IObservable<DiscogsSearchResult> SearchAll(DiscogsSearch search);

        Task<DiscogsRelease> GetRelease(int releaseId);

        Task<DiscogsRelease> GetRelease(int releaseId, CancellationToken token);

        Task<DiscogsMaster> GetMaster(int masterId);

        Task<DiscogsMaster> GetMaster(int masterId, CancellationToken token);
    }
}