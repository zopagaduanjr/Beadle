using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Beadle.Core.Models
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int PersonId { get; set; }
        public string Remarks { get; set; }
        public DateTime TimeIn { get; set; }


        [ForeignKey(typeof(Record))]
        public int RecordId { get; set; }
    }
}
