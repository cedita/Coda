// Copyright (c) Cedita Digital Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.
using Coda.Operations;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Coda.Data
{
    /// <summary>
    /// Provides an abstraction for a store which manages data.
    /// </summary>
    /// <typeparam name="TData">Type of data being managed</typeparam>
    public interface IDataStore<TData> :
        IDisposable, IQueryable<TData>
        where TData : class
    {
        Task<string> GetIdAsStringAsync(TData dataObj, CancellationToken cancellationToken);
        Task<TData> FindByIdStringAsync(string dataId, CancellationToken cancellationToken);
        Task<OperationResult> CreateAsync(TData dataObj, CancellationToken cancellationToken);
        Task<OperationResult> UpdateAsync(TData dataObj, CancellationToken cancellationToken);
        Task<OperationResult> DeleteAsync(TData dataObj, CancellationToken cancellationToken);
    }
}
