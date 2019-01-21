using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Beadle.Core.Models;
using SQLite;

namespace Beadle.Core.Repository.LocalRepository
{
    public class LocalDataService
    {
        readonly SQLiteAsyncConnection database;


        //ctor
        public LocalDataService(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Student>().Wait();
        }

        public LocalDataService()
        {
            
        }

        //methods
        public Task<List<Student>> GetItemsAsync()
        {
            return database.Table<Student>().ToListAsync();
        }

        public Task<List<Student>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Student>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<Student> GetItemAsync(int id)
        {
            return database.Table<Student>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Student item)
        {
            if (item.Id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Student item)
        {
            return database.DeleteAsync(item);
        }

    }
}
