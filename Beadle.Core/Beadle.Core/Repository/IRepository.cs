using System;
using System.Collections.Generic;
using System.Text;
using Beadle.Core.Models;

namespace Beadle.Core.Repository
{
    public interface IRepository
    {
        IDataService<Student> Student { get; }
        IDataService<Session> Session { get; }

    }
}
