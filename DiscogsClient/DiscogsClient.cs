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

        public Task<DiscogsSearchResults> Search(DiscogsSearch search)
        {
            return Search(search, CancellationToken.None);
        }

        public async Task<DiscogsSearchResults> Search(DiscogsSearch search, CancellationToken token)
        {
            var request = GetSearchRequest(search);
            return  await _Client.Execute<DiscogsSearchResults>(request, token);
        }

        public IEnumerable<DiscogsSearchResult> SearchAllEnumerable(DiscogsSearch search)
        {
            return SearchAll(search).ToEnumerable();
        }

        public IObservable<DiscogsSearchResult> SearchAll(DiscogsSearch search)
        {
            var searchPerPage = search.per_page;
            var perPage = (searchPerPage > 0) ? Math.Min(100, searchPerPage.Value) : 50;

            var searchPage = search.page;
            var page = searchPage ?? 1;

            return Observable.Create<DiscogsSearchResult>(async (observer, cancel) =>
            {
                DiscogsPaginedResult pagination;
                do
                {             
                    cancel.ThrowIfCancellationRequested();
                    var request = GetSearchRequest(search, perPage, page++);

                    var res = await _Client.Execute<DiscogsSearchResults>(request, cancel);
                    if (res?.results == null)
                        return;

                    foreach (var result in res.results)
                    {
                        cancel.ThrowIfCancellationRequested();
                        observer.OnNext(result);
                    }

                    pagination = res.pagination;
                }
                while (pagination.page != pagination.pages);
            });
        }

        private IRestRequest GetSearchRequest(DiscogsSearch search, int? perPage=null, int page=1)
        {
            search.per_page = perPage ?? search.per_page;
            search.page = (page != 1) ? page : search.page;
            var request = _Client.GetSearchRequest();
            request.AddAsParameter(search);
            return request;
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
    }
}
