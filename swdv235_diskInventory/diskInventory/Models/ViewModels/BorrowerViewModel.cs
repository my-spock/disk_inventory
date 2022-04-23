using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diskInventory.Models
{
    public class BorrowerViewModel
    {
        private InventoryContext context;

        public BorrowerViewModel(InventoryContext ctx)
        {
            context = ctx;
        }
        public Borrower Borrower { get; set; }
        public string action { get; set; }

        //check for reference in BorrowedItems before delete action
        public int borrowCount
        {
            get
            {
                return context.BorrowedItems.Where(bi => bi.BorrowerId == Borrower.Id).Count();
            }
        }
    }
}
