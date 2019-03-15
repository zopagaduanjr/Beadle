using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Beadle.Core.Models
{
    public class Record
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public string BeadleName { get; set; }
        public string SubjectCode { get; set; }
        public string ClassCode { get; set; }
        public string Professor { get; set; }
        public DateTime DateTime { get; set; }
        public int WeekNumber { get; set; }

        [OneToMany]
        public List<Item> Items { get; set; }




        [ForeignKey(typeof(Session))]
        public int SessionId { get; set; }


    }
}
