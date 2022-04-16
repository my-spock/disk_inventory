using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diskInventory.Models
{
    public class ItemListViewModel
    {
        public List<ItemType> Types { get; set; }
        public List<StatusType> Statuses { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Item> Items { get; set; }

        public string FormatReleaseDate(DateTime date)
        {
            return date.ToString("yyyy/MM/dd");
        }
    }
}
