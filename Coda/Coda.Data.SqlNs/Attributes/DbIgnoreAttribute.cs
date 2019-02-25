using System;

namespace Coda.Data.Sql
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DbIgnoreAttribute : Attribute
    {
    }
}
