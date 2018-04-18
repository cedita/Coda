// Copyright (c) Cedita Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.

using System;
using System.Linq;
using System.Threading.Tasks;

namespace Coda.Data
{
    /// <summary>
    /// Provides an abstraction for a repository which manages data.
    /// </summary>
    /// <typeparam name="TData">Type of data being managed</typeparam>
    /// <typeparam name="TKey">Type of key for the data being managed</typeparam>
    public interface IRepository<TData, TKey> :
        IDisposable, IQueryable<TData>
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
        /// Get object from the repository by ID, asynchronously.
        /// </summary>
        /// <param name="id">Object ID</param>
        /// <returns>Resulting Object</returns>
        Task<TData> GetByIdAsync(TKey id);

        /// <summary>
        /// Add an object to the repository.
        /// </summary>
        /// <param name="obj">Object to Add</param>
        void Add(TData obj);

        /// <summary>
        /// Add an object to the repository, asynchronously.
        /// </summary>
        /// <param name="obj">Object to Add</param>
        /// <returns>Task Result</returns>
        Task AddAsync(TData obj);

        /// <summary>
        /// Update an object in the repository.
        /// </summary>
        /// <param name="obj">Object to Update</param>
        void Update(TData obj);

        /// <summary>
        /// Update an object in the repository, asynchronously.
        /// </summary>
        /// <param name="obj">Object to Update</param>
        /// <returns>Task Result</returns>
        Task UpdateAsync(TData obj);

        /// <summary>
        /// Delete an object from the repository.
        /// </summary>
        /// <param name="obj">Object to Delete</param>
        void Delete(TData obj);

        /// <summary>
        /// Delete an object from the repository, asynchronously.
        /// </summary>
        /// <param name="obj">Object to Delete</param>
        /// <returns>Task Result</returns>
        Task DeleteAsync(TData obj);

        /// <summary>
        /// Save Changes made against the Repository manually.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Save Changes made against the Repository manually, asynchronously.
        /// </summary>
        /// <returns>Task Result</returns>
        Task SaveChangesAsync();

        /// <summary>
        /// Attach an object to the context.
        /// </summary>
        /// <param name="obj">Object to attach</param>
        void Attach(object obj);
    }
}
