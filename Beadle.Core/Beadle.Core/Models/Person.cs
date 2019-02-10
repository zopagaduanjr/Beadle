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

        //important properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return LastName + ", " + FirstName; } }
        public int Late { get; set; }
        public int Absence { get; set; }
        public bool FailureDebarment { get; set; }
        //etcetera properties
        public string Gender { get; set; }
        public string MiddleName { get; set; }
        public string Age { get; set; }
        public string MobileNumber { get; set; }

        [ForeignKey(typeof(Session))]
        public int SessionId { get; set; }
    }
}
