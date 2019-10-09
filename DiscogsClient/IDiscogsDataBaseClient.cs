using DiscogsClient.Data.Query;
using DiscogsClient.Data.Result;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DiscogsClient
{
    public interface IDiscogsDataBaseClient : IDiscogsReleaseRatingClient
    {
        /// <summary>
        /// Get a release
        /// See https://www.discogs.com/developers/#page:database,header:database-release
        /// </summary>
        /// <param name="releaseId">The Release ID</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>The corresponding release</returns>
        Task<DiscogsRelease> GetReleaseAsync(int releaseId, CancellationToken token);

        /// <summary>
        /// Get a release
        /// See https://www.discogs.com/developers/#page:database,header:database-release
        /// </summary>
        /// <param name="releaseId">The Release ID</param>
        /// <returns>The corresponding release</returns>
        Task<DiscogsRelease> GetReleaseAsync(int releaseId);

        /// <summary>
        /// Get a master release
        /// See https://www.discogs.com/developers/#page:database,header:database-master-release
        /// </summary>
        /// <param name="masterId">The Master ID</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>The corresponding master</returns>
        Task<DiscogsMaster> GetMasterAsync(int masterId, CancellationToken token);

        /// <summary>
        /// Get a master release
        /// see https://www.discogs.com/developers/#page:database,header:database-master-release
        /// </summary>
        /// <param name="masterId">The Master ID</param>
        /// <returns>The corresponding master</returns>
        Task<DiscogsMaster> GetMasterAsync(int masterId);

        /// <summary>
        /// Retrieves an observable of all Releases that are versions of this master.
        /// See https://www.discogs.com/developers/#page:database,header:database-master-release-versions
        /// </summary>
        /// <param name="masterId">The Master ID</param>
        /// <param name="max">Number maximum of elements. If null return all elements.</param>
        /// <returns>The corresponding releases</returns>
        IObservable<DiscogsReleaseVersion> GetMasterReleaseVersions(int masterId, int? max = null);

        /// <summary>
        /// Retrieves an enumerable of all Releases that are versions of this master.
        /// See https://www.discogs.com/developers/#page:database,header:database-master-release-versions
        /// </summary>
        /// <param name="masterId">The Master ID</param>
        /// <param name="max">Number maximum of elements. If null return all elements.</param>
        /// <returns>The corresponding releases</returns>
        IEnumerable<DiscogsReleaseVersion> GetMasterReleaseVersionsAsEnumerable(int masterId, int? max = null);

        /// <summary>
        /// Retrieves an enumerable of all Releases that are versions of this master.
        /// See https://www.discogs.com/developers/#page:database,header:database-master-release-versions 
        /// </summary>
        /// <param name="masterId">The Master ID</param>
        /// <param name="paginable">Paginable information</param>
        /// <param name="token">CancellationToken</param>
        /// <returns>The corresponding releases</returns>
        Task<DiscogsPaginableResults<DiscogsReleaseVersion>> GetMasterReleaseVersionsAsync(int masterId,
            DiscogsPaginable paginable, CancellationToken token);

        /// <summary>
        /// Retrieves an enumerable of all Releases that are versions of this master.
        /// See https://www.discogs.com/developers/#page:database,header:database-master-release-versions
        /// </summary>
        /// <param name="masterId">The Master ID</param>
        /// <param name="paginable">Paginable information</param>
        /// <returns>The corresponding releases</returns>
        Task<DiscogsPaginableResults<DiscogsReleaseVersion>> GetMasterReleaseVersionshAsync(int masterId,
            DiscogsPaginable paginable = null);

        /// <summary>
        /// Get an artist
        /// See https://www.discogs.com/developers/#page:database,header:database-artist
        /// </summary>
        /// <param name="artistId">The Release ID</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>The corresponding artist</returns>
        Task<DiscogsArtist> GetArtistAsync(int artistId, CancellationToken token);

        /// <summary>
        /// Get an artist
        /// See https://www.discogs.com/developers/#page:database,header:database-artist
        /// </summary>
        /// <param name="artistId">The Release ID</param>
        /// <returns>The corresponding artist</returns>
        Task<DiscogsArtist> GetArtistAsync(int artistId);

        /// <summary>
        /// Returns a observable of Releases and Masters associated with the Artist.
        /// See https://www.discogs.com/developers/#page:database,header:database-artist-releases
        /// </summary>
        /// <param name="artistId">The artist ID</param>
        /// <param name="sort">Sorting information.</param>
        /// <param name="max">Number maximum of elements. If null return all elements.</param>
        /// <returns>The corresponding releases</returns>
        IObservable<DiscogsArtistRelease> GetArtistRelease(int artistId, DiscogsSortInformation sort = null, int? max = null);

        /// <summary>
        /// Returns a enumerable of Releases and Masters associated with the Artist.
        /// See https://www.discogs.com/developers/#page:database,header:database-artist-releases
        /// </summary>
        /// <param name="artistId">The artist ID</param>
        /// <param name="sort">Sorting information</param>
        /// <param name="max">Number maximum of elements. If null return all elements.</param>
        /// <returns>The corresponding releases</returns>
        IEnumerable<DiscogsArtistRelease> GetArtistReleaseAsEnumerable(int artistId, DiscogsSortInformation sort = null, int? max = null);

        /// <summary>
        /// Returns a enumerable of Releases and Masters associated with the Artist.
        /// See https://www.discogs.com/developers/#page:database,header:database-artist-releases
        /// </summary>
        /// <param name="artistId">The artist ID</param>
        /// <param name="sort">Sorting information</param>
        /// <param name="paginable">Paginable information</param>
        /// <param name="token">CancellationToken</param>
        /// <returns>The corresponding releases</returns>
        Task<DiscogsPaginableResults<DiscogsArtistRelease>> GetArtistReleaseAsync(int artistId,
            DiscogsSortInformation sort, DiscogsPaginable paginable, CancellationToken token);

        /// <summary>
        /// Returns a enumerable of Releases and Masters associated with the Artist.
        /// See https://www.discogs.com/developers/#page:database,header:database-artist-releases
        /// </summary>
        /// <param name="artistId">The artist ID</param>
        /// <param name="sort">Sorting information</param>
        /// <param name="paginable">Paginable information</param>
        /// <returns>The corresponding releases</returns>
        Task<DiscogsPaginableResults<DiscogsArtistRelease>> GetArtistReleaseAsync(int artistId,
            DiscogsSortInformation sort = null, DiscogsPaginable paginable = null);

        /// <summary>
        /// Get a label
        /// See https://www.discogs.com/developers/#page:database,header:database-label
        /// </summary>
        /// <param name="labelId">The Label ID</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>The corresponding label</returns>
        Task<DiscogsLabel> GetLabelAsync(int labelId, CancellationToken token);

        /// <summary>
        /// Get a label
        /// See https://www.discogs.com/developers/#page:database,header:database-label
        /// </summary>
        /// <param name="labelId">The Label ID</param>
        /// <returns>The corresponding label</returns>
        Task<DiscogsLabel> GetLabelAsync(int labelId);

        /// <summary>
        /// Returns an observable of Releases associated with the label.
        /// See https://www.discogs.com/developers/#page:database,header:database-all-label-releases
        /// </summary>
        /// <param name="labelId">The label ID</param>
        /// <param name="max">Number maximum of elements. If null return all elements.</param>
        /// <returns>The corresponding releases</returns>
        IObservable<DiscogsLabelRelease> GetAllLabelReleases(int labelId, int? max = null);

        /// <summary>
        /// Returns an enumerable of Releases associated with the label.
        /// See https://www.discogs.com/developers/#page:database,header:database-all-label-releases
        /// </summary>
        /// <param name="labelId">The label ID</param>
        /// <param name="max">Number maximum of elements. If null return all elements.</param>
        /// <returns>The corresponding label releases</returns>
        IEnumerable<DiscogsLabelRelease> GetAllLabelReleasesAsEnumerable(int labelId, int? max = null);

        /// <summary>
        /// Returns an enumerable of Releases associated with the label.
        /// See https://www.discogs.com/developers/#page:database,header:database-all-label-releases
        /// </summary>
        /// <param name="labelId">The label ID</param>
        /// <param name="paginable">Paginable information</param>
        /// <param name="token">CancellationToken</param>
        /// <returns>The corresponding label releases</returns>
        Task<DiscogsPaginableResults<DiscogsLabelRelease>> GetAllLabelReleasesAsync(int labelId,
            DiscogsPaginable paginable, CancellationToken token);

        /// <summary>
        /// Returns an enumerable of Releases associated with the label.
        /// See https://www.discogs.com/developers/#page:database,header:database-all-label-releases 
        /// </summary>
        /// <param name="labelId">The label ID</param>
        /// <param name="paginable">Paginable information</param>
        /// <returns>The corresponding label releases</returns>
        Task<DiscogsPaginableResults<DiscogsLabelRelease>> GetAllLabelReleasesAsync(int labelId,
            DiscogsPaginable paginable = null);

        /// <summary>
        /// Issue a search query to Discogs database.
        /// See https://www.discogs.com/developers/#page:database,header:database-search.
        /// </summary>
        /// <param name="search">The Corresponding query</param>
        /// <param name="max">Number maximum of elements. If null return all elements.</param>
        /// <returns>The corresponding search result</returns>
        IObservable<DiscogsSearchResult> Search(DiscogsSearch search, int? max = null);

        /// <summary>
        /// Issue a search query to Discogs database.
        /// See https://www.discogs.com/developers/#page:database,header:database-search.
        /// </summary>
        /// <param name="search">The Corresponding query</param>
        /// <param name="max">Number maximum of elements. If null return all elements.</param>
        /// <returns>The corresponding search result</returns>
        IEnumerable<DiscogsSearchResult> SearchAsEnumerable(DiscogsSearch search, int? max = null);

        /// <summary>
        /// Issue a search query to Discogs database.
        /// See https://www.discogs.com/developers/#page:database,header:database-search. 
        /// </summary>
        /// <param name="search">The Corresponding query</param>
        /// <param name="paginable">paginable information</param>
        /// <param name="token">CancellationToken</param>
        /// <returns>The corresponding search result</returns>
        Task<DiscogsPaginableResults<DiscogsSearchResult>> SearchAsync(DiscogsSearch search, DiscogsPaginable paginable,
            CancellationToken token);

        /// <summary>
        /// Issue a search query to Discogs database.
        /// See https://www.discogs.com/developers/#page:database,header:database-search. 
        /// </summary>
        /// <param name="search">The Corresponding query</param>
        /// <param name="paginable">paginable information</param>
        /// <returns>The corresponding search result</returns>
        Task<DiscogsPaginableResults<DiscogsSearchResult>>
            SearchAsync(DiscogsSearch search, DiscogsPaginable paginable = null);

        /// <summary>
        /// Download the stream corresponding to a given image
        /// </summary>
        /// <param name="image">The image to download</param>
        /// <param name="copyStream">Stream where image stream will be copied</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="type">Type of image to download: thumbnail or normal</param>
        Task DownloadImageAsync(DiscogsImage image, Stream copyStream, CancellationToken cancellationToken, DiscogsImageFormatType type = DiscogsImageFormatType.Normal);

        /// <summary>
        /// Download the stream corresponding to a given image
        /// </summary>
        /// <param name="image">The image to download</param>
        /// <param name="copyStream">Stream where image stream will be copied</param>
        /// <param name="type">Type of image to download: thumbnail or normal</param>
        Task DownloadImageAsync(DiscogsImage image, Stream copyStream, DiscogsImageFormatType type = DiscogsImageFormatType.Normal);

        /// <summary>
        /// Save a given image to disk.
        /// </summary>
        /// <param name="image">The image to download</param>
        /// <param name="path">Type of image to download: thumbnail or normal</param>
        /// <param name="fileName">Type of image to download: thumbnail or normal</param>
        /// <param name="type">Type of image to download: thumbnail or normal</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>the file path</returns>
        Task<string> SaveImageAsync(DiscogsImage image, string path, string fileName, CancellationToken cancellationToken, DiscogsImageFormatType type = DiscogsImageFormatType.Normal);

        /// <summary>
        /// Download the stream corresponding to a given image
        /// </summary>
        /// <param name="image">The image to download</param>
        /// <param name="path">Type of image to download: thumbnail or normal</param>
        /// <param name="fileName">Type of image to download: thumbnail or normal</param>
        /// <param name="type">Type of image to download: thumbnail or normal</param>
        /// <returns>the file path</returns>
        Task<string> SaveImageAsync(DiscogsImage image, string path, string fileName, DiscogsImageFormatType type = DiscogsImageFormatType.Normal);
    }
}