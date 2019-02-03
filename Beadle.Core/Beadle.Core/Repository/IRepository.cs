using System;
using System.Collections.Generic;
using System.Text;
using Beadle.Core.Models;
using Beadle.Core.Services;
using SQLite;

namespace Beadle.Core.Repository
{
    public interface IRepository
    {
        IDataService<Student> Student { get; }
        IDataService<Session> Session { get; }
        IDataService<Person> Person { get; }

    }
}
