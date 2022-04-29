using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diskInventory.Models
{
    public class ItemListViewModel
    {
        private InventoryContext context;

        public ItemListViewModel(InventoryContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Item> Items {
            get
            {
                return context.Items
                .Include(i => i.Genre).Include(i => i.Status).Include(i => i.Type)
                .OrderBy(i => i.Name).ThenBy(i => i.TypeId)
                .ToList();
            }
        }

        public string FormatDate(DateTime date)
        {
            return date.ToString("yyyy/MM/dd");
        }
    }
}
