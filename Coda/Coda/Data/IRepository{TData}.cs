// Copyright (c) Cedita Digital Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.

using System;
using System.Linq;

namespace Coda.Data
{
    /// <summary>
    /// Provides an abstraction for a store which manages data with a default key of int
    /// </summary>
    /// <typeparam name="TData">Type of data being managed</typeparam>
    public interface IRepository<TData> :
        IDisposable, IQueryable<TData>, IRepository<TData, int>
        where TData : class
    {
    }
}
