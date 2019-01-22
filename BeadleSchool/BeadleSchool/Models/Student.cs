using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BeadleSchool.Models
{
    public class Student
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
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
