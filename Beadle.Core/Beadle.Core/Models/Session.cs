using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Beadle.Core.Models
{
    public class Session
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }

        [OneToMany]
        public List<Person> Persons { get; set; }
    }
}
