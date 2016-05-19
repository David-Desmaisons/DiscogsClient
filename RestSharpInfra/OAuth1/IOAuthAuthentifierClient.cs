using System.Threading.Tasks;
using System;

namespace RestSharpInfra.OAuth1
{
    public interface IOAuthAuthentifierClient 
    {
        Task<OAuthCompleteInformation> AuthorizeImplicitelly();

        Task<OAuthCompleteInformation> AuthorizeExplicitelly(Func<string, Task<string>> extractVerifier);
    }
}
