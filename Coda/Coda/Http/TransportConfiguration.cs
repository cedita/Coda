using System;

namespace Coda.Http
{
    public class TransportConfiguration : ITransportConfiguration
    {
        /// <summary>
        /// Authentication Key for Transport
        /// </summary>
        public string AuthenticationKey { get; set; }

        /// <summary>
        /// Request Timeout
        /// </summary>
        public TimeSpan RequestTimeout { get; set; } = TimeSpan.FromSeconds(30);

        public string BaseUrl { get; set; }
    }
}
