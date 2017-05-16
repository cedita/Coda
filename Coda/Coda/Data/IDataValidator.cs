// Copyright (c) Cedita Digital Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.

using System.Threading.Tasks;
using Coda.Operations;

namespace Coda.Data
{
    public interface IDataValidator<TData>
    {
        /// <summary>
        /// Validates the specified <paramref name="dataObj"/> as an asynchronous operation.
        /// </summary>
        /// <param name="dataObj">The data object to validate.</param>
        /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="OperationResult"/> of the validation operation.</returns>
        Task<OperationResult> ValidateAsync(TData dataObj);
    }
}
