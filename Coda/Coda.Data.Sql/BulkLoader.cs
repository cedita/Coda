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
    public class BulkLoader : IDisposable
    {
        protected SqlConnection Connection { get; set; }
        protected SqlTransaction Transaction { get; set; }

        protected bool HasCreatedTempInTxn = false;

        public BulkLoader(SqlConnection db)
        {
            if (db.State != ConnectionState.Open)
                db.Open();

            Connection = db;
        }

        protected void CreateTempWithIds(params int[] ids)
        {
            if (!HasCreatedTempInTxn)
            {
                Connection.Execute(
                    "CREATE TABLE #Ids (Id INT NOT NULL PRIMARY KEY CLUSTERED)",
                    transaction: Transaction);

                HasCreatedTempInTxn = true;
            }

            Connection.Execute("TRUNCATE TABLE #Ids");

            var dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(int));
            foreach (var id in ids)
                dataTable.Rows.Add(id);

            new SqlBulkCopy(Connection, SqlBulkCopyOptions.Default, Transaction) { DestinationTableName = "#Ids" }
                .WriteToServer(dataTable);
        }

        protected void DropTable()
        {
            if (!HasCreatedTempInTxn) return;

            Connection.Execute("DROP TABLE #Ids");
        }

        // Query Bulk
        public IEnumerable<TResultType> QueryBulk<TResultType>(string query, params int[] ids)
        {
            CreateTempWithIds(ids);
            return Connection.Query<TResultType>(query, transaction: Transaction);
        }

        public void Dispose()
        {
            DropTable();
            Transaction?.Rollback();
            Transaction?.Dispose();
        }
    }
}
