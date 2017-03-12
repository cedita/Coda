using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Coda.Data.Sql
{
    public interface ISqlBulkCopier : IDisposable
    {
        SqlConnection Connection { get; set; }
        SqlBulkCopy BulkCopy { get; set; }
        DataTable InternalTable { get; set; }
        void AddRows(params object[] rows);
        void WriteToServer();
        Task WriteToServerAsync();
    }
}
