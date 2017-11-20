using System;
using System.Data.SqlClient;
using System.IO;

using Microsoft.Data.Sqlite;

namespace CodeCatalog.DDD.Data.Test.Integration.Helpers
{
    public class HelperBase
    {
        protected SqliteConnection Connection { get; private set; }

        public HelperBase()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            var databaseFile = Path.Combine(appData, "DDDOrderSampleDbTest.sqlite");

            Connection = new SqliteConnection("Data Source=" + databaseFile);
        }
    }
}
