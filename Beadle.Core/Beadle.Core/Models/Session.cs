using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Beadle.Core.Models
{
    public class Session
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

        public Session()
        {
            
        }
    }
}
