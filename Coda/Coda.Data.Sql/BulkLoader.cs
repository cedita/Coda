// Copyright (c) Cedita Digital Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.
using System;
using System.Data.SqlClient;

namespace Coda.Data.Sql
{
    /// <summary>
    /// Given a large list of IDs that would otherwise be used in IN() statements, create a transactional scope
    /// for returning large data sets.
    /// 
    /// <see cref="int"/> by default.
    /// 
    /// Queries used should pull from #Ids and perform necessary joins from within
    /// </summary>
    [Obsolete("Use generic version of BulkLoader instead.")]
    public class BulkLoader : BulkLoader<int>
    {
        public BulkLoader(SqlConnection db) : base(db) { }
    }
}
