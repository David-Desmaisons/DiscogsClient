using DiscogsClient.Data.Query;
using DiscogsClient.Data.Result;
using DiscogsClient.Internal;
using RestSharp;
using RestSharpHelper;
using RestSharpHelper.OAuth1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiscogsClient
{
    public class DiscogsClient : IDiscogsDataBaseClient, IDiscogsUserIdentityClient
    {
        private readonly IDiscogsWebClient _Client;
        private DiscogsIdentity _DiscogsIdentity;

        public DiscogsClient(OAuthCompleteInformation oAuthCompleteInformation, string userAgent = null, int timeOut = 5000)
        {
            _Client = new DiscogsWebClient(oAuthCompleteInformation, userAgent, timeOut);
        }

        public DiscogsClient(TokenAuthenticationInformation tokenAuthenticationInformation, string userAgent = null, int timeOut = 5000)
        {
            _Client = new DiscogsWebClient(tokenAuthenticationInformation, userAgent, timeOut);
        }

        public Task<DiscogsIdentity> GetUserIdentityAsync()
        {
            return GetUserIdentityAsync(CancellationToken.None);
        }

        public async Task<DiscogsIdentity> GetUserIdentityAsync(CancellationToken token)
        {
            if (_DiscogsIdentity != null)
                return _DiscogsIdentity;

            var request = _Client.GetUserIdentityRequest();
            return _DiscogsIdentity = await _Client.Execute<DiscogsIdentity>(request, token);
        }

        public Task<DiscogsRelease> GetReleaseAsync(int releaseId)
        {
            return GetReleaseAsync(releaseId, CancellationToken.None);
        }

        public async Task<DiscogsRelease> GetReleaseAsync(int releaseId, CancellationToken token)
        {
            var request = _Client.GetReleaseRequest(releaseId);
            return await _Client.Execute<DiscogsRelease>(request, token);
        }

        public Task<DiscogsMaster> GetMasterAsync(int masterId)
        {
            return GetMasterAsync(masterId, CancellationToken.None);
        }

        public async Task<DiscogsMaster> GetMasterAsync(int masterId, CancellationToken token)
        {
            var request = _Client.GetMasterRequest(masterId);
            return await _Client.Execute<DiscogsMaster>(request, token);
        }

        public Task<DiscogsArtist> GetArtistAsync(int artistId)
        {
            return GetArtistAsync(artistId, CancellationToken.None);
        }

        public async Task<DiscogsArtist> GetArtistAsync(int artistId, CancellationToken token)
        {
            var request = _Client.GetArtistRequest(artistId);
            return await _Client.Execute<DiscogsArtist>(request, token);
        }

        public Task<DiscogsLabel> GetLabelAsync(int labelId)
        {
            return GetLabelAsync(labelId, CancellationToken.None);
        }

        public async Task<DiscogsLabel> GetLabelAsync(int labelId, CancellationToken token)
        {
            var request = _Client.GetLabelRequest(labelId);
            return await _Client.Execute<DiscogsLabel>(request, token);
        }

        public Task<DiscogsReleaseRating> GetUserReleaseRatingAsync(string userName, int releaseId)
        {
            return GetUserReleaseRatingAsync(userName, releaseId, CancellationToken.None);
        }

        public async Task<DiscogsReleaseRating> GetUserReleaseRatingAsync(string userName, int releaseId, CancellationToken token)
        {
            var request = _Client.GetGetUserReleaseRatingRequest(userName, releaseId);
            return await _Client.Execute<DiscogsReleaseRating>(request, token);
        }

        public Task<DiscogsReleaseRating> SetUserReleaseRatingAsync(int releaseId, int rating)
        {
            return SetUserReleaseRatingAsync(releaseId, rating, CancellationToken.None);
        }

        public async Task<DiscogsReleaseRating> SetUserReleaseRatingAsync(int releaseId, int rating, CancellationToken token)
        {
            var userIdentity = await GetUserIdentityAsync(token);
            var request = _Client.GetPutUserReleaseRatingRequest(userIdentity.username, releaseId);
            request.AddJsonBody(new { rating = rating });
            return await _Client.Execute<DiscogsReleaseRating>(request, token);
        }

        public Task<bool> DeleteUserReleaseRatingAsync(int releaseId)
        {
            return DeleteUserReleaseRatingAsync(releaseId, CancellationToken.None);
        }

        public async Task<bool> DeleteUserReleaseRatingAsync(int releaseId, CancellationToken token)
        {
            var userIdentity = await GetUserIdentityAsync(token);
            var request = _Client.GetDeleteUserReleaseRatingRequest(userIdentity.username, releaseId);
            return await _Client.Execute(request, token) == HttpStatusCode.NoContent;
        }

        public Task<DiscogsCommunityReleaseRating> GetCommunityReleaseRatingAsync(int releaseId)
        {
            return GetCommunityReleaseRatingAsync(releaseId, CancellationToken.None);
        }

        public async Task<DiscogsCommunityReleaseRating> GetCommunityReleaseRatingAsync(int releaseId, CancellationToken token)
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
            return max.HasValue ? observable.Take(max.Value) : observable;
        }

        public Task<DiscogsSearchResults> SearchAsync(DiscogsSearch search, DiscogsPaginable paginable, CancellationToken token)
        {
            IRestRequest RequestBuilder() => _Client.GetSearchRequest().AddAsParameter(search);
            return GetPaginableAsync<DiscogsSearchResults>(RequestBuilder, paginable, token);
        }

        public Task<DiscogsSearchResults> SearchAsync(DiscogsSearch search, DiscogsPaginable paginable = null)
        {
            return SearchAsync(search, paginable, CancellationToken.None);
        }

        private IObservable<DiscogsSearchResult> RawSearchAll(DiscogsSearch search, int? max = null)
        {
            IRestRequest RequestBuilder() => _Client.GetSearchRequest().AddAsParameter(search);
            return GenerateFromPaginable<DiscogsSearchResult, DiscogsSearchResults>(RequestBuilder, max);
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

        public Task<DiscogsReleaseVersions> GetMasterReleaseVersionsAsync(int masterId, DiscogsPaginable paginable, CancellationToken token)
        {
            IRestRequest RequestBuilder() => _Client.GetMasterReleaseVersionRequest(masterId);
            return GetPaginableAsync<DiscogsReleaseVersions>(RequestBuilder, paginable, token);
        }

        public Task<DiscogsReleaseVersions> GetMasterReleaseVersionsAsync(int masterId, DiscogsPaginable paginable = null)
        {
            return GetMasterReleaseVersionsAsync(masterId, paginable, CancellationToken.None);
        }

        private IObservable<DiscogsReleaseVersion> GetMasterReleaseVersionseRaw(int masterId, int? max = default(int?))
        {
            IRestRequest RequestBuilder() => _Client.GetMasterReleaseVersionRequest(masterId);
            return GenerateFromPaginable<DiscogsReleaseVersion, DiscogsReleaseVersions>(RequestBuilder, max);
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

        public Task<DiscogsArtistReleases> GetArtistReleaseAsync(int artistId, DiscogsSortInformation sort, DiscogsPaginable paginable, CancellationToken token)
        {
            IRestRequest RequestBuilder() => _Client.GetArtistReleaseVersionRequest(artistId).AddAsParameter(sort);
            return GetPaginableAsync<DiscogsArtistReleases>(RequestBuilder, paginable, token);
        }

        public Task<DiscogsArtistReleases> GetArtistReleaseAsync(int artistId, DiscogsSortInformation sort = null, DiscogsPaginable paginable = null)
        {
            return GetArtistReleaseAsync(artistId, sort, paginable, CancellationToken.None);
        }

        private IObservable<DiscogsArtistRelease> GetArtistReleaseRaw(int artistId, DiscogsSortInformation sort = null, int? max = null)
        {
            IRestRequest RequestBuilder() => _Client.GetArtistReleaseVersionRequest(artistId).AddAsParameter(sort);
            return GenerateFromPaginable<DiscogsArtistRelease, DiscogsArtistReleases>(RequestBuilder, max);
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

        public Task<DiscogsLabelReleases> GetAllLabelReleasesAsync(int labelId, DiscogsPaginable paginable, CancellationToken token)
        {
            IRestRequest RequestBuilder() => _Client.GetAllLabelReleasesRequest(labelId);
            return GetPaginableAsync<DiscogsLabelReleases>(RequestBuilder, paginable, token);
        }

        public Task<DiscogsLabelReleases> GetAllLabelReleasesAsync(int labelId, DiscogsPaginable paginable = null)
        {
            return GetAllLabelReleasesAsync(labelId, paginable, CancellationToken.None);
        }

        private IObservable<DiscogsLabelRelease> GetAllLabelReleasesRaw(int labelId, int? max = null)
        {
            IRestRequest RequestBuilder() => _Client.GetAllLabelReleasesRequest(labelId);
            return GenerateFromPaginable<DiscogsLabelRelease, DiscogsLabelReleases>(RequestBuilder, max);
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

        private async Task<T> GetPaginableAsync<T>(Func<IRestRequest> requestBuilder, DiscogsPaginable paginable, CancellationToken token)
        {
            paginable = paginable ?? new DiscogsPaginable()
            {
                per_page = 50,
                page = 1
            };

            var request = requestBuilder().AddAsParameter(paginable);
            return await _Client.Execute<T>(request, token);
        }

        public Task DownloadImageAsync(DiscogsImage image, Stream copyStream, DiscogsImageFormatType type = DiscogsImageFormatType.Normal)
        {
            return DownloadImageAsync(image, copyStream, CancellationToken.None, type);
        }

        public async Task DownloadImageAsync(DiscogsImage image, Stream copyStream, CancellationToken cancellationToken, DiscogsImageFormatType type = DiscogsImageFormatType.Normal)
        {
            var url = (type == DiscogsImageFormatType.Normal) ? image.uri : image.uri150;
            await _Client.Download(url, copyStream, cancellationToken);
        }

        public Task<string> SaveImageAsync(DiscogsImage image, string path, string fileName, DiscogsImageFormatType type = DiscogsImageFormatType.Normal)
        {
            return SaveImageAsync(image, path, fileName, CancellationToken.None, type);
        }

        public async Task<string> SaveImageAsync(DiscogsImage image, string path, string fileName, CancellationToken cancellationToken, DiscogsImageFormatType type = DiscogsImageFormatType.Normal)
        {
            var url = (type == DiscogsImageFormatType.Normal) ? image.uri : image.uri150;
            return await _Client.SaveFile(url, path, fileName, cancellationToken);
        }
    }
}
