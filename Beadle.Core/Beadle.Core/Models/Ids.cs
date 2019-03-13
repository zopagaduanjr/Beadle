using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Beadle.Core.Models
{
    public class Ids
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string TimeIn { get; set; }
        public string Remarks { get; set; }

        [ForeignKey(typeof(Record))]
        public int RecordId { get; set; }

    }
}
