using System;
using System.Collections.Generic;
using System.Text;
namespace Beadle.Repository
{
    public interface IDataService<T> where T : class
    {
        //implementations of CRUD
        //create read update delete

        void Add(T record);

        //T Get();  //READ

        //int Update(T record); //UPDATE

        // int Delete(T record); //DELETE
    }
}
