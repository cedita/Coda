// Copyright (c) Cedita Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Coda.Data.Sql
{
    /// <summary>
    /// Given a large list of IDs that would otherwise be used in IN() statements, create a transactional scope
    /// for returning large data sets.
    /// 
    /// Queries used should pull from #Ids and perform necessary joins from within
    /// </summary>
    public class BulkLoader<TIdType> : IdHolderTransaction<TIdType>
    {
        public BulkLoader(SqlConnection db) : base(db) { }

        /// <summary>
        /// Executes a bulk query, returning the data typed as per T
        /// </summary>
        /// <typeparam name="TResultType"></typeparam>
        /// <param name="query"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IEnumerable<TResultType> QueryBulk<TResultType>(string query, TIdType[] ids, object param = null)
        {
            CreateTempWithIds(ids);
            return Connection.Query<TResultType>(query, param, transaction: Transaction);
        }

        /// <summary>
        /// Executes a bulk query, returning the data typed dynamically with properties matching the columns
        /// </summary>
        /// <param name="query"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> QueryBulk(string query, TIdType[] ids, object param = null)
        {
            CreateTempWithIds(ids);
            return Connection.Query(query, param, transaction: Transaction);
        }

        /// <summary>
        /// Executes a bulk query
        /// </summar>
        /// <param name="query"></param>
        /// <param name="ids"></param>
        public void ExecuteBulk(string query, TIdType[] ids, object param = null)
        {
            CreateTempWithIds(ids);
            Connection.Execute(query, param, transaction: Transaction);
        }
    }
}
