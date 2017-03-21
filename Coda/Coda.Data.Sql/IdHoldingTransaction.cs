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
    public class IdHolderTransaction : IDisposable
    {
        public SqlConnection Connection { get; set; }
        public SqlTransaction Transaction { get; set; }

        protected bool HasCreatedTempInTxn = false;

        public IdHolderTransaction(SqlConnection db)
        {
            if (db.State != ConnectionState.Open)
                db.Open();

            Connection = db;
            Transaction = db.BeginTransaction();
        }

        public void CreateTempWithIds(IEnumerable<int> ids)
        {
            if (!HasCreatedTempInTxn)
            {
                Connection.Execute(
                    "CREATE TABLE #Ids (Id INT NOT NULL PRIMARY KEY CLUSTERED)",
                    transaction: Transaction);

                HasCreatedTempInTxn = true;
            }

            Connection.Execute("TRUNCATE TABLE #Ids", transaction: Transaction);

            var dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(int));
            foreach (var id in ids)
                dataTable.Rows.Add(id);

            new SqlBulkCopy(Connection, SqlBulkCopyOptions.Default, Transaction) { DestinationTableName = "#Ids" }
                .WriteToServer(dataTable);
        }

        public void DropTable()
        {
            if (!HasCreatedTempInTxn) return;

            Connection.Execute("DROP TABLE #Ids", transaction: Transaction);
        }

        public void Dispose()
        {
            Transaction?.Dispose();
        }
    }
}
