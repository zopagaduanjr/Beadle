using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace Beadle.Models
{
    public class AbsentItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Name { get; set; }
        public string Reason { get; set; }
        public bool FailureDebarment { get; set; }
    }
}
