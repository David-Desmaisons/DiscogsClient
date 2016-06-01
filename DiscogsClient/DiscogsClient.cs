using DiscogsClient.Data.Query;
using DiscogsClient.Data.Result;
using DiscogsClient.Internal;
using RestSharp;
using RestSharpInfra;
using RestSharpInfra.OAuth1;
using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiscogsClient
{
    public class DiscogsClient
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

        public IObservable<DiscogsSearchResult> SearchAll(DiscogsSearch search, int? maxElement=null)
        {
            var perPage = 50;
            var page = 0;
            if ((maxElement != null) && (maxElement.Value > 0) && (maxElement.Value < 50))
                perPage = maxElement.Value;

            DiscogsPaginedResult pagination = null;

            return Observable.Create<DiscogsSearchResult>(async (observer, cancel) =>
            {
                do
                {
                    var request = GetSearchRequest(search, perPage, ++page);

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
    }
}
