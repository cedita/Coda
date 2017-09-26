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
    /// Creates a temporary table storing Ids for large data sets which can then be joined onto for subsequent queries
    /// </summary>
    public class IdHolderTable
    {
        public SqlConnection Connection { get; set; }
        public SqlTransaction Transaction { get; set; }

        public string TemporaryTableName { get; set; } = "#Ids";

        protected bool HasCreatedTempInTxn = false;

        public IdHolderTable(SqlConnection db, SqlTransaction SqlTransaction = null)
        {
            if (db.State != ConnectionState.Open)
                db.Open();

            Connection = db;
            Transaction = (SqlTransaction != null ? SqlTransaction : db.BeginTransaction());
        }

        public void CreateTempWithIds(IEnumerable<int> ids)
        {
            if (!HasCreatedTempInTxn)
            {
                Connection.Execute(
                    $"CREATE TABLE {TemporaryTableName} (Id INT NOT NULL PRIMARY KEY CLUSTERED)",
                    transaction: Transaction);

                HasCreatedTempInTxn = true;
            }

            Connection.Execute($"TRUNCATE TABLE {TemporaryTableName}", transaction: Transaction);

            var dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(int));
            foreach (var id in ids)
                dataTable.Rows.Add(id);

            new SqlBulkCopy(Connection, SqlBulkCopyOptions.Default, Transaction) { DestinationTableName = TemporaryTableName }
                .WriteToServer(dataTable);
        }

        public void DropTable()
        {
            if (!HasCreatedTempInTxn) return;

            Connection.Execute($"DROP TABLE {TemporaryTableName}", transaction: Transaction);
        }
    }
}
