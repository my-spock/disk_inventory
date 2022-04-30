using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diskInventory.Models
{
    public class LendingViewModel
    {
        private InventoryContext context;
        public LendingViewModel(InventoryContext ctx)
        {
            context = ctx;
        }

        public string action { get; set; }
        public BorrowedItem BorrowedItem { get; set; }

        public IEnumerable<Borrower> Borrowers
        {
            get
            {
                return context.Borrowers.OrderBy(b => b.FullName).ToList();
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