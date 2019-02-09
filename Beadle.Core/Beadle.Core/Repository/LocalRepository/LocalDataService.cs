using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Beadle.Core.Models;
using Beadle.Core.Services;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

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
            await database.InsertWithChildrenAsync(item, true);
            return item;
        }
        //READ crud implementation
        //read all
        public async Task<List<T>> GetItemsAsync()
        {
            return  await database.GetAllWithChildrenAsync<T>();
            //return new ObservableCollection<T>(b);
            //return database.Table<T>().ToListAsync();

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

        //one to many functions
        public async Task<T> UpdateWithChildrenAsync(T item)
        {
            await database.UpdateWithChildrenAsync(item);
            await database.UpdateAsync(item);
            return item;
        }

        public async Task<T> UpdateItemAsync(T item)
        {
             await database.UpdateAsync(item);
             return item;
        }




    }
}
