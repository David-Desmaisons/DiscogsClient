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
        /// <summary>
        /// Get a release
        /// See https://www.discogs.com/developers/#page:database,header:database-release
        /// </summary>
        /// <param name="releaseId">The Release ID</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>The corresponding release</returns>
        Task<DiscogsRelease> GetRelease(int releaseId, CancellationToken token);

        /// <summary>
        /// Get a release
        /// See https://www.discogs.com/developers/#page:database,header:database-release
        /// </summary>
        /// <param name="releaseId">The Release ID</param>
        /// <returns>The corresponding release</returns>
        Task<DiscogsRelease> GetRelease(int releaseId);

        /// <summary>
        /// Get a master release
        /// See https://www.discogs.com/developers/#page:database,header:database-master-release
        /// </summary>
        /// <param name="masterId">The Master ID</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>The corresponding master</returns>
        Task<DiscogsMaster> GetMaster(int masterId, CancellationToken token);

        /// <summary>
        /// Get a master release
        /// see https://www.discogs.com/developers/#page:database,header:database-master-release
        /// </summary>
        /// <param name="masterId">The Master ID</param>
        /// <returns>The corresponding master</returns>
        Task<DiscogsMaster> GetMaster(int masterId);

        /// <summary>
        /// Retrieves a list of all Releases that are versions of this master.
        /// See https://www.discogs.com/developers/#page:database,header:database-master-release-versions
        /// </summary>
        /// <param name="masterId">The Master ID</param>
        /// <param name="max">Number maximum of elements. If null return all elements.</param>
        /// <returns>The corresponding releases</returns>
        IObservable<DiscogsReleaseVersion> GetMasterReleaseVersions(int masterId, int? max = null);

        /// <summary>
        /// Retrieves a list of all Releases that are versions of this master.
        /// See https://www.discogs.com/developers/#page:database,header:database-master-release-versions
        /// </summary>
        /// <param name="masterId">The Master ID</param>
        /// <param name="max">Number maximum of elements. If null return all elements.</param>
        /// <returns>The corresponding releases</returns>
        IEnumerable<DiscogsReleaseVersion> GetMasterReleaseVersionsAsEnumerable(int masterId, int? max = null);

        /// <summary>
        /// Get an artist
        /// See https://www.discogs.com/developers/#page:database,header:database-artist
        /// </summary>
        /// <param name="artistId">The Release ID</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>The corresponding artist</returns>
        Task<DiscogsArtist> GetArtist(int artistId, CancellationToken token);

        /// <summary>
        /// Get an artist
        /// See https://www.discogs.com/developers/#page:database,header:database-artist
        /// </summary>
        /// <param name="artistId">The Release ID</param>
        /// <returns>The corresponding artist</returns>
        Task<DiscogsArtist> GetArtist(int artistId);

        /// <summary>
        /// Returns a list of Releases and Masters associated with the Artist.
        /// See https://www.discogs.com/developers/#page:database,header:database-artist-releases
        /// </summary>
        /// <param name="artistId">The artist ID</param>
        /// <param name="sort">Sorting information.</param>
        /// <param name="max">Number maximum of elements. If null return all elements.</param>
        /// <returns>The corresponding releases</returns>
        IObservable<DiscogsArtistRelease> GetArtistRelease(int artistId, DiscogsSortInformation sort = null, int ? max = null);

        /// <summary>
        /// Returns a list of Releases and Masters associated with the Artist.
        /// See https://www.discogs.com/developers/#page:database,header:database-artist-releases
        /// </summary>
        /// <param name="artistId">The artist ID</param>
        /// <param name="sort">Sorting information.</param>
        /// <param name="max">Number maximum of elements. If null return all elements.</param>
        /// <returns>The corresponding releases</returns>
        IEnumerable<DiscogsArtistRelease> GetArtistReleaseAsEnumerable(int artistId, DiscogsSortInformation sort =null, int? max = null);

        /// <summary>
        /// Issue a search query to Discogs database.
        /// See https://www.discogs.com/developers/#page:database,header:database-search.
        /// </summary>
        /// <param name="search">The Corresponding query</param>
        /// <param name="max">Number maximum of elements. If null return all elements.</param>
        /// <returns></returns>
        IObservable<DiscogsSearchResult> Search(DiscogsSearch search, int? max = null);

        /// <summary>
        /// Issue a search query to Discogs database.
        /// See https://www.discogs.com/developers/#page:database,header:database-search.
        /// </summary>
        /// <param name="search">The Corresponding query</param>
        /// <param name="max">Number maximum of elements. If null return all elements.</param>
        /// <returns></returns>
        IEnumerable<DiscogsSearchResult> SearchAsEnumerable(DiscogsSearch search, int? max = null);
    }
}