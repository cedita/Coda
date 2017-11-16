namespace Coda.Transport
{
    /// <summary>
    /// Status of the Communication that was just sent
    /// </summary>
    public enum CommunicationStatus
    {
        /// <summary>
        /// Unknown status
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Client Transport returned an error
        /// </summary>
        ClientError = 1,

        /// <summary>
        /// Remote returned an error
        /// </summary>
        RemoteError = 2,

        /// <summary>
        /// Pending / Polling is not permitted, and no response was received within a certain time limit
        /// </summary>
        Timeout = 3,

        /// <summary>
        /// Communication was successful
        /// </summary>
        Success = 4,
    }
}
