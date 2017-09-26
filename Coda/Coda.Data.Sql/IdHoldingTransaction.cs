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
    /// Create a transactional scope and #Ids table for working with large data sets on the SQL Database side
    /// 
    /// Queries ran against this should use #Ids
    /// </summary>
    public class IdHolderTransaction : IdHolderTable, IDisposable
    {
        public IdHolderTransaction(SqlConnection db, SqlTransaction SqlTransaction = null) : base(db, SqlTransaction)
        {
        }

        public void Dispose()
        {
            Transaction?.Dispose();
        }
    }
}
