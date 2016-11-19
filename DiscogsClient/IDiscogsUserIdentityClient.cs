using DiscogsClient.Data.Result;
using System.Threading;
using System.Threading.Tasks;

namespace DiscogsClient
{
    public interface IDiscogsUserIdentityClient
    {
        /// <summary>
        /// Retrieve basic information about the authenticated user.
        /// You can use this resource to find out who you’re authenticated as, and 
        /// it also doubles as a good sanity check to ensure that you’re using OAuth correctly.
        /// See https://www.discogs.com/developers/#page:user-identity,header:user-identity-identity
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The current discogs user identity</returns>
        Task<DiscogsIdentity> GetUserIdentityAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Retrieve basic information about the authenticated user.
        /// You can use this resource to find out who you’re authenticated as, and 
        /// it also doubles as a good sanity check to ensure that you’re using OAuth correctly.
        /// See https://www.discogs.com/developers/#page:user-identity,header:user-identity-identity
        /// </summary>
        /// <returns>The current discogs user identity</returns>
        Task<DiscogsIdentity> GetUserIdentity();
    }
}
