using System;

namespace Coda.Http
{
    public interface ITransportConfiguration
    {
        /// <summary>
        /// Authentication Key for Transport
        /// </summary>
        string AuthenticationKey { get; set; }

        /// <summary>
        /// The target url for the request.
        /// </summary>
        string BaseUrl { get; set; }

        /// <summary>
        /// Request Timeout
        /// </summary>
        TimeSpan RequestTimeout { get; set; }
    }
}