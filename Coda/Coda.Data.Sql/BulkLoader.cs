// Copyright (c) Cedita Digital Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coda.Data.Sql
{
    /// <summary>
    /// Given a large list of IDs that would otherwise be used in IN() statements, create a transactional scope
    /// for returning large data sets.
    /// 
    /// Queries used should pull from #Ids and perform necessary joins from within
    /// </summary>
    public class BulkLoader : IdHolderTransaction
    {
        public BulkLoader(SqlConnection db) : base(db) { }

        /// <summary>
        /// Executes a bulk query, returning the data typed as per T
        /// </summary>
        /// <typeparam name="TResultType"></typeparam>
        /// <param name="query"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IEnumerable<TResultType> QueryBulk<TResultType>(string query, int[] ids, object param = null)
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
        public IEnumerable<dynamic> QueryBulk(string query, int[] ids, object param = null)
        {
            CreateTempWithIds(ids);
            return Connection.Query(query, param, transaction: Transaction);
        }
    }
}
