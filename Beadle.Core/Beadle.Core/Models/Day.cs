using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Beadle.Core.Models
{
    public class Day
    {
        public string Name { get; set; }
        public ObservableCollection<Item> Items { get; set; }

        public Day(string name, ObservableCollection<Item> items)
        {
            Name = name;
            Items = items;
        }

    }
}
