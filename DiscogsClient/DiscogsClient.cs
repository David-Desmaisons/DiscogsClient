using DiscogsClient.Data.Query;
using DiscogsClient.Data.Result;
using DiscogsClient.Internal;
using RestSharp;
using RestSharpInfra;
using RestSharpInfra.OAuth1;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace DiscogsClient
{
    public class DiscogsClient : IDiscogsDataBaseClient, IDiscogsUserIdentityClient
    {
        private readonly IDiscogsWebClient _Client;
        private DiscogsIdentity _DiscogsIdentity;

        public DiscogsClient(OAuthCompleteInformation oAuthCompleteInformation, string userAgent=null, int timeOut = 5000)
        {
            _Client = new DiscogsWebClient(oAuthCompleteInformation, timeOut)
            {
                UserAgent = userAgent
            };
        }

        public Task<DiscogsIdentity> GetUserIdentity()
        {
            return GetUserIdentity(CancellationToken.None);
        }

        public async Task<DiscogsIdentity> GetUserIdentity(CancellationToken token)
        {
            if (_DiscogsIdentity != null)
                return _DiscogsIdentity;

            var request = _Client.GetUserIdentityRequest();
            return _DiscogsIdentity = await _Client.Execute<DiscogsIdentity>(request, token);
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
            var request = _Client.GetGetUserReleaseRatingRequest(userName, releaseId);
            return await _Client.Execute<DiscogsReleaseRating>(request, token);          
        }

        public Task<DiscogsReleaseRating> SetUserReleaseRating(int releaseId, int rating)
        {
            return SetUserReleaseRating(releaseId, rating, CancellationToken.None);
        }

        public async Task<DiscogsReleaseRating> SetUserReleaseRating(int releaseId, int rating, CancellationToken token)
        {
            var userIdendity = await GetUserIdentity(token);
            var request = _Client.GetPutUserReleaseRatingRequest(userIdendity.username, releaseId);
            request.AddJsonBody(new { rating= rating});
            return await _Client.Execute<DiscogsReleaseRating>(request, token);
        }

        public Task<bool> DeleteUserReleaseRating(int releaseId)
        {
            return DeleteUserReleaseRating(releaseId, CancellationToken.None);
        }

        public async Task<bool> DeleteUserReleaseRating(int releaseId, CancellationToken token)
        {
            var userIdendity = await GetUserIdentity(token);
            var request = _Client.GetDeleteUserReleaseRatingRequest(userIdendity.username, releaseId);
            return await _Client.Execute(request, token) == HttpStatusCode.NoContent;
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

        public Task DownloadImage(DiscogsImage image, Stream copyStream, DiscogsImageFormatType type = DiscogsImageFormatType.Normal)
        {
            return DownloadImage(image, copyStream, CancellationToken.None, type);
        }

        public async Task DownloadImage(DiscogsImage image, Stream copyStream, CancellationToken cancellationToken, DiscogsImageFormatType type = DiscogsImageFormatType.Normal)
        {
            var url = (type == DiscogsImageFormatType.Normal) ? image.uri : image.uri150;
            await _Client.Download(url, copyStream, cancellationToken); 
        }

        public Task SaveImage(DiscogsImage image, string path, string fileName, DiscogsImageFormatType type = DiscogsImageFormatType.Normal)
        {
            return SaveImage(image, path, fileName, CancellationToken.None, type);
        }

        public async Task SaveImage(DiscogsImage image, string path, string fileName, CancellationToken cancellationToken, DiscogsImageFormatType type = DiscogsImageFormatType.Normal)
        { 
            var url = (type == DiscogsImageFormatType.Normal) ? image.uri : image.uri150;
            var extension = Path.GetExtension(url);
            var fullPath = Path.Combine(path, fileName + extension);
            using (var writer = File.Create(fullPath))
            {
                await DownloadImage(image, writer, cancellationToken, type);
            }
        }
    }
}
