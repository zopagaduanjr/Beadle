using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Beadle.Core.Models
{
    public class Student
    {
        public Student(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public string MobileNumber { get; set; }
        public bool Late { get; set; }
        public Student()
        {

        }
    }
}
