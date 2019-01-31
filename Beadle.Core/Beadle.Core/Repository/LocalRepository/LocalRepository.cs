using System;
using System.Collections.Generic;
using System.Text;
using Beadle.Core.Models;

namespace Beadle.Core.Repository.LocalRepository
{
    public class LocalRepository : IRepository
    {
        public IDataService<Student> Student { get; } = new LocalDataService<Student>();
        public IDataService<Session> Session { get; } = new LocalDataService<Session>();

    }
}
