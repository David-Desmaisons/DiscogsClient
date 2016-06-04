using DiscogsClient.Data.Result;
using System.Threading;
using System.Threading.Tasks;

namespace DiscogsClient
{
    public interface IDiscogsReleaseRatingClient
    {
        /// <summary>
        /// Retrieves the release’s rating for a given user.
        /// See https://www.discogs.com/developers/#page:database,header:database-release-rating-by-user
        /// </summary>
        /// <param name="userName">The username of the rating you are trying to request.</param>
        /// <param name="releaseId">The Release ID</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The corresponding release’s rating</returns>
        Task<DiscogsReleaseRating> GetUserReleaseRating(string userName, int releaseId, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves the release’s rating for a given user.
        /// See https://www.discogs.com/developers/#page:database,header:database-release-rating-by-user
        /// </summary>
        /// <param name="userName">The username of the rating you are trying to request.</param>
        /// <param name="releaseId">The Release ID</param>
        /// <returns>The corresponding release’s rating</returns>
        Task<DiscogsReleaseRating> GetUserReleaseRating(string userName, int releaseId);

        /// <summary>
        /// Retrieves the community release rating average and count.
        /// See https://www.discogs.com/developers/#page:database,header:database-community-release-rating
        /// </summary>
        /// <param name="releaseId">The Release ID</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The community release rating</returns>
        Task<DiscogsCommunityReleaseRating> GetCommunityReleaseRating(int releaseId, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves the community release rating average and count.
        /// See https://www.discogs.com/developers/#page:database,header:database-community-release-rating
        /// </summary>
        /// <param name="releaseId">The Release ID</param>
        /// <returns>The community release rating</returns>
        Task<DiscogsCommunityReleaseRating> GetCommunityReleaseRating(int releaseId);
    }
}
