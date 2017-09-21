using System;
using System.Collections.Generic;

namespace Coda.Http
{
    public interface ITransportConfiguration
    {
        /// <summary>
        /// Authentication Key for Transport
        /// </summary>
        string AuthenticationKey { get; set; }

        /// <summary>
        /// Service Name
        /// </summary>
        string ServiceName { get; set; }

        /// <summary>
        /// Request Timeout
        /// </summary>
        TimeSpan RequestTimeout { get; set; }

        /// <summary>
        /// Overrides for the individual transports if any are defined
        /// </summary>
        Dictionary<int, object> TransportOverrides { get; set; }
    }
}
