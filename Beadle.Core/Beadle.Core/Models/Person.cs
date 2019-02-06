using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Beadle.Core.Models
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public string Type { get; set; }
        public bool Late { get; set; }

        [OneToMany]
        public List<Session> Sessions { get; set; }
    }
}
