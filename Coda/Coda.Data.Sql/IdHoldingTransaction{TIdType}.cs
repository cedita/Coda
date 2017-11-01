// Copyright (c) Cedita Digital Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.
using System;
using System.Data.SqlClient;

namespace Coda.Data.Sql
{
    /// <summary>
    /// Create a transactional scope and #Ids table for working with large data sets on the SQL Database side
    /// 
    /// Queries ran against this should use #Ids
    /// </summary>
    public class IdHolderTransaction<TIdType> : IdHolderTable<TIdType>, IDisposable
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
