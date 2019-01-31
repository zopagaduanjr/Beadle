using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Beadle.Core.Models;
using SQLite;

namespace Beadle.Core.Repository.LocalRepository
{
    public class LocalDataService<T> : IDataService<T> where T : class, new()

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

        //methods.

        //INTERFACE REQUIREMENTS
        //CREATE crud implementation
        public async Task<T> SaveItemAsync(T item)
        {
            database.InsertAsync(item);
            return item;
        }
        //READ crud implementation
        //read all
        public async Task<List<T>> GetItemsAsync()
        {
            return await database.Table<T>().ToListAsync();
        }
        //UPDATE crud implementation



        //DELETE crud implementation
        public async Task<T> DeleteItemAsync(T item)
        {
            database.DeleteAsync(item);
            return item; //still use as void, 
        }

        //read specific item via its ID


        //public async Task<T> GetItemAsync(Func<T, bool> finder)
        //{

        //    return await database.Table<T>().Where(x => finder(x) ).FirstOrDefaultAsync();
        //}

        public Task<List<Student>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Student>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }



    }
}
