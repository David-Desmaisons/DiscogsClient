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
        IEnumerable<DiscogsSearchResult> SearchAllAsEnumerable(DiscogsSearch search, int? max = null);

        IObservable<DiscogsSearchResult> SearchAll(DiscogsSearch search, int? max = null);

        IEnumerable<DiscogsReleaseVersion> GetMasterReleaseVersionAsEnumerable(int masterId, int? max=null);

        IObservable<DiscogsReleaseVersion> GetMasterReleaseVersion(int masterId, int? max = null);

        Task<DiscogsRelease> GetRelease(int releaseId);

        Task<DiscogsRelease> GetRelease(int releaseId, CancellationToken token);

        Task<DiscogsMaster> GetMaster(int masterId);

        Task<DiscogsMaster> GetMaster(int masterId, CancellationToken token);
    }
}