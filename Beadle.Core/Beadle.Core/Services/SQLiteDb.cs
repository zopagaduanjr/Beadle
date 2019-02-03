using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SQLite;

namespace Beadle.Core.Services
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoSQLite.db3");
            return new SQLiteAsyncConnection(dbPath);

        }
    }
}
