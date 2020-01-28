// Copyright (c) Cedita Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using Coda.Operations;

namespace Coda.Data
{
    /// <summary>
    /// Provides an abstraction for a repository which manages data.
    /// </summary>
    /// <typeparam name="TData">Type of data being managed</typeparam>
    /// <typeparam name="TKey">Type of key for the data being managed</typeparam>
    public interface IAsyncRepository<TData, TKey> :
        IDisposable, IQueryable<TData>
        where TData : class
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Get object from the repository by ID, asynchronously.
        /// </summary>
        /// <param name="id">Object ID</param>
        /// <returns>Resulting Object</returns>
        Task<TData> GetByIdAsync(TKey id);

        /// <summary>
        /// Add an object to the repository, asynchronously.
        /// </summary>
        /// <param name="obj">Object to Add</param>
        /// <returns>Task Result</returns>
        Task<OperationResult<TKey>> AddAsync(TData obj);

        /// <summary>
        /// Update an object in the repository, asynchronously.
        /// </summary>
        /// <param name="obj">Object to Update</param>
        /// <returns>Task Result</returns>
        Task<OperationResult> UpdateAsync(TData obj);

        /// <summary>
        /// Delete an object from the repository, asynchronously.
        /// </summary>
        /// <param name="obj">Object to Delete</param>
        /// <returns>Task Result</returns>
        Task<OperationResult> DeleteAsync(TData obj);

        /// <summary>
        /// Save Changes made against the Repository manually, asynchronously.
        /// </summary>
        /// <returns>Task Result</returns>
        Task<OperationResult> SaveChangesAsync();

        /// <summary>
        /// Attach an object to the context.
        /// </summary>
        /// <param name="obj">Object to attach</param>
        void Attach(object obj);
    }
}
