using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diskInventory.Models
{
    public class CheckoutViewModel
    {
        private InventoryContext context;
        public CheckoutViewModel(InventoryContext ctx)
        {
            context = ctx;
        }

        public string action { get; set; }
        public BorrowedItem borrowedItem { get; set; }

        public IEnumerable<Borrower> Borrowers
        {
            get
            {
                return context.Borrowers.ToList();
            }
        }

        public IEnumerable<Item> Items
        {
            get
            {
                StatusType statusAvailable = context.StatusTypes.Where(st => st.Name == "Available").Single();
                int availableId = statusAvailable.Id;
                return context.Items.Where(i => i.StatusId == availableId).ToList();
            }
        }
    }
}
