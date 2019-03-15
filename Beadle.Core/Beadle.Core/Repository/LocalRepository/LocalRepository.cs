using System;
using System.Collections.Generic;
using System.Text;
using Beadle.Core.Models;
using Beadle.Core.Services;
using SQLite;

namespace Beadle.Core.Repository.LocalRepository
{
    public class LocalRepository : IRepository
    {
        public IDataService<Student> Student { get; } = new LocalDataService<Student>();
        public IDataService<Session> Session { get; } = new LocalDataService<Session>();
        public IDataService<Person> Person { get; } = new LocalDataService<Person>();
        public IDataService<Record> Record { get; } = new LocalDataService<Record>();
        public IDataService<Item> Item { get; } = new LocalDataService<Item>();
    }
}
