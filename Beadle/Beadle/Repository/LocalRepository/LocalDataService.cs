using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Beadle.Models;

using SQLite;

namespace Beadle.Repository.LocalRepository
{
    public class LocalDataService<T> : IDataService<T> where T : class, new()
    {
        private static string dbPath = Path.Combine(Environment.GetFolderPath
            (Environment.SpecialFolder.LocalApplicationData), "TodoSQLite.db3");
        public void Add(T item)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                db.CreateTable<AbsentItem>();
                db.Insert(item);
                db.Close();
            }   
        }

        //method for open and close instantly sql database

    }
}
