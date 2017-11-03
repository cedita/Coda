using System;

namespace Coda.Transport.Abstractions
{
    public interface ITransportConfiguration
    {
        /// <summary>
        /// Gets or Sets Authentication Key for Transport
        /// </summary>
        string AuthenticationKey { get; set; }

        /// <summary>
        /// Gets or Sets the target url for the request.
        /// </summary>
        string BaseUrl { get; set; }

        /// <summary>
        /// Gets or Sets Request Timeout
        /// </summary>
        TimeSpan RequestTimeout { get; set; }
    }
}