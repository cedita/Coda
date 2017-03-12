using System;

namespace Coda.Operations
{
    /// <summary>
    /// Encapsulates an error from an operation.
    /// </summary>
    public class OperationError
    {
        /// <summary>
        /// The Code that represents this error.
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// The Message for this error.
        /// </summary>
        public virtual string Message { get; set; }
        /// <summary>
        /// The Exception (if any) for this error.
        /// </summary>
        public virtual Exception Exception { get; set; }
    }
}
