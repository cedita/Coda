
namespace Coda.Http
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
        /// Polling for a result (no response was received within a certain time limit, and pending permitted)
        /// 
        /// *NOTE* Polling to be implemented as a defined method later
        /// </summary>
        //Polling = 3,
        /// <summary>
        /// Pending / Polling is not permitted, and no response was received within a certain time limit
        /// </summary>
        Timeout = 4,
        /// <summary>
        /// Communication was successful
        /// </summary>
        Success = 5
    }
}
