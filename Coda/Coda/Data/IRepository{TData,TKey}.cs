// Copyright (c) Cedita Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.

using System;
using System.Linq;
using Coda.Operations;

namespace Coda.Data
{
    /// <summary>
    /// Provides an abstraction for a repository which manages data.
    /// </summary>
    /// <typeparam name="TData">Type of data being managed</typeparam>
    /// <typeparam name="TKey">Type of key for the data being managed</typeparam>
    public interface IRepository<TData, TKey> :
        IDisposable, IQueryable<TData>, IAsyncRepository<TData, TKey>
        where TData : class
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Get object from the repository by ID.
        /// </summary>
        /// <param name="id">Object ID</param>
        /// <returns>Resulting Object</returns>
        TData GetById(TKey id);

        /// <summary>
        /// Add an object to the repository.
        /// </summary>
        /// <param name="obj">Object to Add</param>
        /// <returns>Result with created ID</returns>
        OperationResult<int> Add(TData obj);

        /// <summary>
        /// Update an object in the repository.
        /// </summary>
        /// <param name="obj">Object to Update</param>
        /// <returns>Result</returns>
        OperationResult Update(TData obj);

        /// <summary>
        /// Delete an object from the repository.
        /// </summary>
        /// <param name="obj">Object to Delete</param>
        /// <returns>Result</returns>
        OperationResult Delete(TData obj);

        /// <summary>
        /// Save Changes made against the Repository manually.
        /// </summary>
        /// <returns>Result</returns>
        OperationResult SaveChanges();
    }
}
