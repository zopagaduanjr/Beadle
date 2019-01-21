using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Beadle.Core.Repository
{
    public interface IDataService<T> where T : class, new()
    {
        List<T> GetAllData();

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
