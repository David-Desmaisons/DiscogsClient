using System.Threading.Tasks;
using System;

namespace RestSharpInfra.OAuth1
{
    public interface IOAuthAuthentifierClient 
    {
        /// <summary>
        /// Authorize the user account.
        /// If the implicit operation fails, the function extractVerifier will be called:
        /// it will received a link for the user to register and should return the corresponding
        /// TokenSecret. If the function returns null the operation will be cancelled.
        /// </summary>
        /// <param name="extractVerifier">
        /// A function receiving a link where user can allow the application to use its account
        /// and returning the Token secret in case of success, null otherwise.
        /// </param>
        /// <returns>The updated OAuthCompleteInformation</returns>
        Task<OAuthCompleteInformation> Authorize(Func<string, Task<string>> extractVerifier);
    }
}
