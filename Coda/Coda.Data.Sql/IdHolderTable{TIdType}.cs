// Copyright (c) Cedita Digital Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Coda.Data.Sql
{
    /// <summary>
    /// Creates a temporary table storing Ids for large data sets which can then be joined onto for subsequent queries
    /// </summary>
    public class IdHolderTable<TIdType>
    {
        public SqlConnection Connection { get; set; }
        public SqlTransaction Transaction { get; set; }

        public string TemporaryTableName { get; set; } = "#Ids";
        public string TemporaryTableIdType => ClrTypeToSqlType();

        protected bool HasCreatedTempInTxn = false;

        protected Dictionary<Type, SqlDbType> SqlTypeMap { get; set; }

        public IdHolderTable(SqlConnection db, SqlTransaction SqlTransaction = null)
        {
            if (db.State != ConnectionState.Open)
                db.Open();

            Connection = db;
            Transaction = (SqlTransaction != null ? SqlTransaction : db.BeginTransaction());
        }

        public void CreateTempWithIds(IEnumerable<TIdType> ids)
        {
            if (!HasCreatedTempInTxn)
            {
                Connection.Execute(
                    $"CREATE TABLE {TemporaryTableName} (Id {TemporaryTableIdType} NOT NULL PRIMARY KEY CLUSTERED)",
                    transaction: Transaction);

                HasCreatedTempInTxn = true;
            }

            Connection.Execute($"TRUNCATE TABLE {TemporaryTableName}", transaction: Transaction);

            var dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(TIdType));
            foreach (var id in ids)
                dataTable.Rows.Add(id);

            new SqlBulkCopy(Connection, SqlBulkCopyOptions.Default, Transaction) { DestinationTableName = TemporaryTableName }
                .WriteToServer(dataTable);
        }

        public string ClrTypeToSqlType()
        {
            var typeMap = SqlTypeMap ?? (SqlTypeMap = new Dictionary<Type, SqlDbType>
            {
                { typeof(string), SqlDbType.NVarChar },
                { typeof(char[]), SqlDbType.NVarChar },
                { typeof(byte), SqlDbType.TinyInt },
                { typeof(short), SqlDbType.SmallInt },
                { typeof(int), SqlDbType.Int },
                { typeof(long), SqlDbType.BigInt },
                { typeof(byte[]), SqlDbType.Image },
                { typeof(bool), SqlDbType.Bit },
                { typeof(DateTime), SqlDbType.DateTime2 },
                { typeof(DateTimeOffset), SqlDbType.DateTimeOffset },
                { typeof(decimal), SqlDbType.Money },
                { typeof(float), SqlDbType.Real },
                { typeof(double), SqlDbType.Float },
                { typeof(TimeSpan), SqlDbType.Time },
            });

            var type = typeof(TIdType);
            type = Nullable.GetUnderlyingType(type) ?? type;

            return typeMap[type].ToString().ToUpperInvariant();
        }

        public void DropTable()
        {
            if (!HasCreatedTempInTxn) return;

            Connection.Execute($"DROP TABLE {TemporaryTableName}", transaction: Transaction);
        }
    }
}
