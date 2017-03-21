// Copyright (c) Cedita Digital Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Coda.Data.Sql.DapperLike
{
    public static class BulkExtensions
    {
        /// <summary>
        /// Executes a bulk query, returning the data typed as per T
        /// </summary>
        /// <typeparam name="TResultType">Resulting Type</typeparam>
        /// <param name="query">Query utilising #Ids internal table</param>
        /// <param name="ids">Bulk IDs to query with</param>
        /// <param name="param">Optional Query Parameters</param>
        /// <returns>Bulk Query Results</returns>
        public static IEnumerable<TResultType> QueryBulk<TResultType>(this SqlConnection db, string query, int[] ids, object param = null)
        {
            using (var bulkLoader = new BulkLoader(db))
                return bulkLoader.QueryBulk<TResultType>(query, ids, param);
        }


        /// <summary>
        /// Executes a bulk query, returning the data typed dynamically with properties matching the columns
        /// </summary>
        /// <param name="query">Query utilising #Ids internal table</param>
        /// <param name="ids">Bulk IDs to query with</param>
        /// <param name="param">Optional Query Parameters</param>
        /// <returns>Bulk Query Results</returns>
        public static IEnumerable<dynamic> QueryBulk(this SqlConnection db, string query, int[] ids, object param = null)
        {
            using (var bulkLoader = new BulkLoader(db))
                return bulkLoader.QueryBulk(query, ids, param);
        }
    }
}
