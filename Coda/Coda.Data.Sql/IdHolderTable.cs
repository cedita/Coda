// Copyright (c) Cedita Digital Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.
using System;
using System.Data.SqlClient;

namespace Coda.Data.Sql
{
    /// <summary>
    /// Creates a temporary table storing Ids for large data sets which can then be joined onto for subsequent queries
    /// </summary>
    [Obsolete("Use generic version of IdHolderTable instead.")]
    public class IdHolderTable : IdHolderTable<int>
    {
        public IdHolderTable(SqlConnection db, SqlTransaction SqlTransaction = null) : base (db, SqlTransaction) { }
    }
}
