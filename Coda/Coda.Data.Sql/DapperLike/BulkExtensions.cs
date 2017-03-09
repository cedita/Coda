using System.Collections.Generic;
using System.Data.SqlClient;

namespace Coda.Data.Sql.DapperLike
{
    public static class BulkExtensions
    {
        public static IEnumerable<TResultType> QueryBulk<TResultType>(this SqlConnection db, string query, params int[] ids)
        {
            using (var bulkLoader = new BulkLoader(db))
                return bulkLoader.QueryBulk<TResultType>(query, ids);
        }
    }
}
