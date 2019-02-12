using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Beadle.Core.Repository
{
    public interface IDataService<T> where T : class, new()
    {


        //Create
        Task<T> SaveItemAsync(T item);

        //Read
        Task<List<T>> GetAllItemsAsync();

        //Delete
        Task<T> DeleteItemAsync(T item);

        //UpdateOneToMany
        Task<T> UpdateWithChildrenAsync(T item);

        Task<T> UpdateItemAsync(T item);

        Task<T> GetItemAsync(Expression<Func<T, bool>> item);
        //Get

        //Get Specific Contact data  
        //T GetTData(int contactID);

        // Delete all Contacts Data  
        //void DeleteAllContacts();

        //// Delete Specific Contact  
        //void DeleteItem(int contactID);

        //// Insert new Contact to DB   
        //void InsertItem(T contact);

        //// Update Contact Data  
        //void UpdateItem(T contact);




    }
}
