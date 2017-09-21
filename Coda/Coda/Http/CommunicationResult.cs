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
        /// Status of the Communication
        /// </summary>
        public CommunicationStatus Status { get; set; }

        /// <summary>
        /// Date that the Communication was sent
        /// </summary>
        public DateTime DateSent { get; set; }

        /// <summary>
        /// Date that the Communication had data returned, if any
        /// </summary>
        public DateTime? DateReturned { get; set; }

        /// <summary>
        /// If Pending is supported, this date will be set for a Re-Attempt
        /// </summary>
        public DateTime? DateReattempt { get; set; }

        /// <summary>
        /// Remote Data Returned back to the Transport
        /// </summary>
        public TResponse Data { get; set; }

        /// <summary>
        /// Exception if any, when the <see cref="Status"/> is either <see cref="CommunicationStatus.ClientError"/> or <see cref="CommunicationStatus.RemoteError"/>
        /// </summary>
        public Exception Exception { get; set; }
    }
}
