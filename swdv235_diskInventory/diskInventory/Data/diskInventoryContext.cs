using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using diskInventory.Models;

namespace diskInventory.Data
{
    public class diskInventoryContext : DbContext
    {
        public diskInventoryContext (DbContextOptions<diskInventoryContext> options)
            : base(options)
        {
        }

        public DbSet<diskInventory.Models.BorrowedItem> BorrowedItem { get; set; }
    }
}
