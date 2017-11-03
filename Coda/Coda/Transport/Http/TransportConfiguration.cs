using System;
using Coda.Transport.Abstractions;

namespace Coda.Transport.Http
{
    public class TransportConfiguration : ITransportConfiguration
    {
        /// <summary>
        /// Gets or Sets Authentication Key for Transport
        /// </summary>
        public string AuthenticationKey { get; set; }

        /// <summary>
        /// Gets or Sets Request Timeout
        /// </summary>
        public TimeSpan RequestTimeout { get; set; } = TimeSpan.FromSeconds(30);

        /// <summary>
        /// Gets or Sets Base URL
        /// </summary>
        public string BaseUrl { get; set; }
    }
}
