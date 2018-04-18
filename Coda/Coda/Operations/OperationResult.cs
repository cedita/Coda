// Copyright (c) Cedita Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.

using System.Collections.Generic;
using System.Linq;

namespace Coda.Operations
{
    /// <summary>
    /// Represents the result of an operation.
    /// </summary>
    public class OperationResult
    {
        public static readonly OperationResult Success = new OperationResult { Succeeded = true };

        private List<OperationError> errors = new List<OperationError>();

        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful.
        /// </summary>
        /// <value>True if the operation succeeded, false otherwise.</value>
        public bool Succeeded { get; set; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{T}"/> of <see cref="OperationError"/>s containing any errors that
        /// occurred during the operation.
        /// </summary>
        public IEnumerable<OperationError> Errors => errors;

        /// <summary>
        /// Create an instance of <see cref="OperationResult"/> representing a failed operation with a list of
        /// <paramref name="errors"/> if applicable.
        /// </summary>
        /// <param name="errors">An optional array of <see cref="OperationError"/>s which caused the operation to fail.</param>
        /// <returns>An <see cref="OperationResult"/> indicating a failed operation, with a list of <paramref name="errors"/> if applicable.</returns>
        public static OperationResult Failure(params OperationError[] errors)
        {
            var result = new OperationResult { Succeeded = false };
            if (errors != null)
            {
                result.errors.AddRange(errors);
            }

            return result;
        }

        /// <summary>
        /// Converts the value of the current <see cref="OperationResult"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of the current <see cref="OperationResult"/> object.</returns>
        /// <remarks>
        /// If the operation was successful the ToString() will return "Succeeded" otherwise it returned
        /// "Failed: " followed by a comma delimited list of error codes from its <see cref="Errors"/> collection, if any.
        /// </remarks>
        public override string ToString()
        {
            return Succeeded ?
                   "Succeeded" :
                   string.Format("{0}: {1}", "Failed", string.Join(",", Errors.Select(x => x.Code).ToList()));
        }
    }
}
