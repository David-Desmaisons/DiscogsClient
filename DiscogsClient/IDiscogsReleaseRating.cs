using DiscogsClient.Data.Result;
using System.Threading;
using System.Threading.Tasks;

namespace DiscogsClient
{
    public interface IDiscogsReleaseRating
    {
        /// <summary>
        /// Retrieves the release’s rating for a given user.
        /// See https://www.discogs.com/developers/#page:database,header:database-release-rating-by-user
        /// </summary>
        /// <param name="userName">The username of the rating you are trying to request.</param>
        /// <param name="releaseId">The Release ID</param>
        /// <returns>The corresponding release’s rating</returns>
        Task<DiscogsReleaseRating> GetUserReleaseRating(string userName, int releaseId);

        /// <summary>
        /// Retrieves the release’s rating for a given user.
        /// See https://www.discogs.com/developers/#page:database,header:database-release-rating-by-user
        /// </summary>
        /// <param name="userName">The username of the rating you are trying to request.</param>
        /// <param name="releaseId">The Release ID</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The corresponding release’s rating</returns>
        Task<DiscogsReleaseRating> GetUserReleaseRating(string userName, int releaseId, CancellationToken cancellationToken);
    }
}
