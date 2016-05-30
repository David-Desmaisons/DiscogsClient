using System;

namespace DiscogsClient
{
    public class DiscogsException : Exception
    {
        public DiscogsException(string message, Exception innerException): base(message, innerException)
        {
        }
    }
}
