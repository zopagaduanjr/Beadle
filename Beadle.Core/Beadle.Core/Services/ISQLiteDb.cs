using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Beadle.Core.Services
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
