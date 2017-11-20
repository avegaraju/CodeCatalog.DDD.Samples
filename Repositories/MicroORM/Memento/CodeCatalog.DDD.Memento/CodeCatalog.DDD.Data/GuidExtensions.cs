using System;

namespace CodeCatalog.DDD.Data
{
    public static class GuidExtensions
    {
        public static string ToSqliteGuid(this Guid guid)
        {
            return 
                string
                .Format("x'{0}'",
                                BitConverter
                                .ToString(guid.ToByteArray()).Replace("-", ""));
        }
    }
}