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
    public class DiscogsClient : IDiscogsDataBaseClient
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

        public Task<DiscogsArtist> GetArtist(int artistId) 
        {
            return GetArtist(artistId, CancellationToken.None);
        }

        public async Task<DiscogsArtist> GetArtist(int artistId, CancellationToken token) 
        {
            var request = _Client.GetArtistRequest(artistId);
            return await _Client.Execute<DiscogsArtist>(request, token);
        }

        public Task<DiscogsLabel> GetLabel(int labelId) 
        {
            return GetLabel(labelId, CancellationToken.None);
        }

        public async Task<DiscogsLabel> GetLabel(int labelId, CancellationToken token) 
        {
            var request = _Client.GetLabelRequest(labelId);
            return await _Client.Execute<DiscogsLabel>(request, token);
        }

        public Task<DiscogsReleaseRating> GetUserReleaseRating(string userName, int releaseId)
        {
            return GetUserReleaseRating(userName, releaseId, CancellationToken.None);
        }

        public async Task<DiscogsReleaseRating> GetUserReleaseRating(string userName, int releaseId, CancellationToken token)
        {
            var request = _Client.GetUserReleaseRatingRequest(userName, releaseId);
            return await _Client.Execute<DiscogsReleaseRating>(request, token);          
        }

        public Task<DiscogsCommunityReleaseRating> GetCommunityReleaseRating(int releaseId)
        {
            return GetCommunityReleaseRating(releaseId, CancellationToken.None);
        }

        public async Task<DiscogsCommunityReleaseRating> GetCommunityReleaseRating(int releaseId, CancellationToken token)
        {
            var request = _Client.GetCommunityReleaseRatingRequest(releaseId);
            return await _Client.Execute<DiscogsCommunityReleaseRating>(request, token);
        }

        public IEnumerable<DiscogsSearchResult> SearchAsEnumerable(DiscogsSearch search, int? max = null)
        {
            return Search(search, max).ToEnumerable();
        }

        public IObservable<DiscogsSearchResult> Search(DiscogsSearch search, int? max = null)
        {
            var observable = RawSearchAll(search, max);
            return max.HasValue? observable.Take(max.Value) : observable;
        }

        private IObservable<DiscogsSearchResult> RawSearchAll(DiscogsSearch search, int? max = null)
        {
            Func<IRestRequest> requestBuilder = () =>  _Client.GetSearchRequest().AddAsParameter(search);
            return GenerateFromPaginable<DiscogsSearchResult, DiscogsSearchResults>(requestBuilder, max);
        }

        public IEnumerable<DiscogsReleaseVersion> GetMasterReleaseVersionsAsEnumerable(int masterId, int? max = default(int?))
        {
            return GetMasterReleaseVersions(masterId, max).ToEnumerable();        
        }

        public IObservable<DiscogsReleaseVersion> GetMasterReleaseVersions(int masterId, int? max = default(int?)) 
        {
            var observable = GetMasterReleaseVersionseRaw(masterId, max);
            return max.HasValue ? observable.Take(max.Value) : observable;
        }

        private IObservable<DiscogsReleaseVersion> GetMasterReleaseVersionseRaw(int masterId, int? max = default(int?))
        {
            Func<IRestRequest> requestBuilder = () => _Client.GetMasterReleaseVersionRequest(masterId);
            return GenerateFromPaginable<DiscogsReleaseVersion, DiscogsReleaseVersions>(requestBuilder, max);
        }

        public IEnumerable<DiscogsArtistRelease> GetArtistReleaseAsEnumerable(int artistId, DiscogsSortInformation sort = null, int? max = null) 
        {
            return GetArtistRelease(artistId, sort, max).ToEnumerable();
        }

        public IObservable<DiscogsArtistRelease> GetArtistRelease(int artistId, DiscogsSortInformation sort = null, int? max = null) 
        {
            var observable = GetArtistReleaseRaw(artistId, sort, max);
            return max.HasValue ? observable.Take(max.Value) : observable;
        }

        private IObservable<DiscogsArtistRelease> GetArtistReleaseRaw(int artistId, DiscogsSortInformation sort = null, int? max = null) 
        {
            Func<IRestRequest> requestBuilder = () => _Client.GetArtistReleaseVersionRequest(artistId).AddAsParameter(sort);
            return GenerateFromPaginable<DiscogsArtistRelease, DiscogsArtistReleases>(requestBuilder, max);
        }

        public IEnumerable<DiscogsLabelRelease> GetAllLabelReleasesAsEnumerable(int labelId, int? max = null)
        {
            return GetAllLabelReleases(labelId, max).ToEnumerable();
        }

        public IObservable<DiscogsLabelRelease> GetAllLabelReleases(int labelId, int? max = null)
        {
            var observable = GetAllLabelReleasesRaw(labelId, max);
            return max.HasValue ? observable.Take(max.Value) : observable;
        }

        private IObservable<DiscogsLabelRelease> GetAllLabelReleasesRaw(int labelId, int? max = null)
        {
            Func<IRestRequest> requestBuilder = () => _Client.GetAllLabelReleasesRequest(labelId);
            return GenerateFromPaginable<DiscogsLabelRelease, DiscogsLabelReleases>(requestBuilder, max);
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
