using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Beadle.Models;
using SQLite;

namespace Beadle.Data
{
    public class AbsentItemDatabase
    {
        private readonly SQLiteAsyncConnection database;

        public AbsentItemDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<AbsentItem>().Wait();
        }

        //READ items
        public Task<List<AbsentItem>> GetItemsAsync()
        {
            return database.Table<AbsentItem>().ToListAsync();
        }

        //READ items that has FD == True
        public Task<List<AbsentItem>> GetItemsFDAsync()
        {
            return database.QueryAsync<AbsentItem>("SELECT * FROM [AbsentItem] WHERE [FailureDebarment] = 0");
        }


        public Task<AbsentItem> GetItemAsync(int id)
        {
            return database.Table<AbsentItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        //UPDATE
        public Task<int> SaveItemAsync(AbsentItem item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }
        //DELETE
        public Task<int> DeleteItemAsync(AbsentItem item)
        {
            return database.DeleteAsync(item);
        }

    }
}
