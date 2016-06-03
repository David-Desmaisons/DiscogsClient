using DiscogsClient.Data.Query;
using DiscogsClient.Data.Result;
using DiscogsClient.Internal;
using RestSharp;
using RestSharpInfra;
using RestSharpInfra.OAuth1;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiscogsClient
{
    public class DiscogsClient : IDiscogsClient
    {
        private readonly IDiscogsWebClient _Client;

        public DiscogsClient(OAuthCompleteInformation oAuthCompleteInformation, int timeOut = 5000)
        {
            _Client = new DiscogsWebClient(oAuthCompleteInformation, timeOut);
        }

        public Task<DiscogsRelease> GetRelease(int releaseId)
        {
            return GetRelease(releaseId, CancellationToken.None);
        }

        public async Task<DiscogsRelease> GetRelease(int releaseId, CancellationToken token)
        {
            var request = _Client.GetReleaseRequest(releaseId);
            return await _Client.Execute<DiscogsRelease>(request, token);
        }

        public Task<DiscogsMaster> GetMaster(int masterId)
        {
            return GetMaster(masterId, CancellationToken.None);
        }

        public async Task<DiscogsMaster> GetMaster(int masterId, CancellationToken token)
        {
            var request = _Client.GetMasterRequest(masterId);
            return await _Client.Execute<DiscogsMaster>(request, token);
        }

        public IEnumerable<DiscogsSearchResult> SearchAllAsEnumerable(DiscogsSearch search, int? max = null)
        {
            return SearchAll(search, max).ToEnumerable();
        }

        public IObservable<DiscogsSearchResult> SearchAll(DiscogsSearch search, int? max = null)
        {
            var observable = RawSearchAll(search, max);
            return max.HasValue? observable.Take(max.Value) : observable;
        }

        private IObservable<DiscogsSearchResult> RawSearchAll(DiscogsSearch search, int? max = null)
        {
            Func<IRestRequest> requestBuilder = () =>  _Client.GetSearchRequest().AddAsParameter(search);
            return GenerateFromPaginable<DiscogsSearchResult, DiscogsSearchResults>(requestBuilder, max);
        }

        public IEnumerable<DiscogsReleaseVersion> GetMasterReleaseVersionAsEnumerable(int masterId, int? max = default(int?))
        {
            return GetMasterReleaseVersion(masterId, max).ToEnumerable();        
        }

        public IObservable<DiscogsReleaseVersion> GetMasterReleaseVersion(int masterId, int? max = default(int?))
        {
            Func<IRestRequest> requestBuilder = () => _Client.GetMasterReleaseVersion(masterId);
            return GenerateFromPaginable<DiscogsReleaseVersion, DiscogsReleaseVersions>(requestBuilder, max);
        }

        private IObservable<T> GenerateFromPaginable<T, TRes>(Func<IRestRequest> requestBuilder, int? max = null) where T : DiscogsEntity 
                        where TRes : DiscogsPaginableResults<T>
        {
            var paginable = new DiscogsPaginable()
            {
                per_page = (max > 0) ? Math.Min(50, max.Value) : new int?(),
                page = 1
            };

            return Observable.Create<T>(async (observer, cancel) =>
            {
                DiscogsPaginedResult pagination;
                do
                {
                    cancel.ThrowIfCancellationRequested();
                    var request = requestBuilder().AddAsParameter(paginable);
                    paginable.page++;

                    var res = await _Client.Execute<TRes>(request, cancel);
                    var elements = res?.GetResults();
                    if (elements == null)
                        return;

                    foreach (var result in elements)
                    {
                        cancel.ThrowIfCancellationRequested();
                        observer.OnNext(result);
                    }

                    pagination = res.pagination;
                }
                while (pagination.page != pagination.pages);
            });
        }
    }
}
