// Copyright (c) Cedita Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.

namespace Coda.Operations
{
    /// <summary>
    /// Represents the result of an operation.
    /// </summary>
    /// <typeparam name="TResult">Type of Result.</typeparam>
    public class OperationResult<TResult> : OperationResult
    {
        public TResult Result { get; set; }
    }
}
