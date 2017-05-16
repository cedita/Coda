// Copyright (c) Cedita Digital Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coda.Data
{
    /// <summary>
    /// Provides an abstraction for a store which manages data based on a typed key.
    /// </summary>
    /// <typeparam name="TData">Type of data being managed</typeparam>
    /// <typeparam name="TKey">Type of key on <see cref="TData"/></typeparam>
    public interface IKeyedDataStore<TData, TKey> :
        IDataStore<TData>, IDisposable
        where TData : class
        where TKey : IEquatable<TKey>
    {
        Task<TKey> GetIdAsync(TData dataObj, CancellationToken cancellationToken);

        Task<TData> FindByIdAsync(TKey dataId, CancellationToken cancellationToken);
    }
}
