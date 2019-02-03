using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Beadle.Core.Models;
using Beadle.Core.Services;
using SQLite;

namespace Beadle.Core.Repository.LocalRepository
{
    public class LocalDataService<T> : IDataService<T> where T : class, new()

    {
        private string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoSQLite.db3");
        private readonly SQLiteAsyncConnection database;


        //ctor
        public LocalDataService()
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Session>().Wait();
            database.CreateTableAsync<Student>().Wait();
        }
        //methods.

        //INTERFACE REQUIREMENTS
        //CREATE crud implementation
        public async Task<T> SaveItemAsync(T item)
        {
            await database.InsertAsync(item);
            return item;
        }
        //READ crud implementation
        //read all
        public  Task<List<T>> GetItemsAsync()
        {
            return  database.Table<T>().ToListAsync();

        }
        //UPDATE crud implementation



        //DELETE crud implementation
        public async Task<T> DeleteItemAsync(T item)
        {
            await database.DeleteAsync(item);
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
