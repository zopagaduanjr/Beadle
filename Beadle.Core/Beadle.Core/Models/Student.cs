using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Beadle.Core.Models
{
    public class Student
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public string MobileNumber { get; set; }
        public int Late { get; set; }
        public int Absence { get; set; }
        public bool FailureDebarment { get; set; }
        [ForeignKey(typeof(Session))]
        public int SessionId { get; set; }

    }
}
