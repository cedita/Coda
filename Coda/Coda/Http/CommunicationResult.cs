using System;

namespace Coda.Http
{
    /// <summary>
    /// Communication Result for Typed data
    /// </summary>
    /// <typeparam name="TResponse">Response Type</typeparam>
    public class CommunicationResult<TResponse> : ICommunicationResult
    {
        /// <summary>
        /// Gets or Sets Status of the Communication
        /// </summary>
        public CommunicationStatus Status { get; set; }

        /// <summary>
        /// Gets or Sets Date that the Communication was sent
        /// </summary>
        public DateTime DateSent { get; set; }

        /// <summary>
        /// Gets or Sets Date that the Communication had data returned, if any
        /// </summary>
        public DateTime? DateReturned { get; set; }

        /// <summary>
        /// Gets or Sets date to be set for a Re-Attempt, if Pending is supported
        /// </summary>
        public DateTime? DateReattempt { get; set; }

        /// <summary>
        /// Gets or Sets Remote Data Returned back to the Transport
        /// </summary>
        public TResponse Data { get; set; }

        /// <summary>
        /// Gets or Sets Exception if any, when the <see cref="Status"/> is either <see cref="CommunicationStatus.ClientError"/> or <see cref="CommunicationStatus.RemoteError"/>
        /// </summary>
        public Exception Exception { get; set; }
    }
}
