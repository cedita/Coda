// Copyright (c) Cedita Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.
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
