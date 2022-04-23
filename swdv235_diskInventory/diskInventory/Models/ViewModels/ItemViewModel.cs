using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace diskInventory.Models
{
    public class ItemViewModel
    {
        private InventoryContext context;

        public ItemViewModel(InventoryContext ctx)
        {
            context = ctx;
        }
        public Item Item { get; set; }
        public string action { get; set; }
        public IEnumerable<Genre> Genres {
            get
            {
                return context.Genres.ToList();
            }
        }
        public IEnumerable<ItemType> Types {
            get
            {
                return context.ItemTypes.ToList();
            }
        }
        public IEnumerable<StatusType> Statuses { 
            get
            {
                return context.StatusTypes.ToList();
            }
        }
        //check for reference in BorrowedItems before delete action
        public int borrowCount {
            get
            {
                return context.BorrowedItems.Where(bi => bi.ItemId == Item.Id).Count();
            }
        }

    }
}
