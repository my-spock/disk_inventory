using Microsoft.EntityFrameworkCore;

namespace diskInventory.Models
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options)
            : base(options)
        { }

        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<StatusType> StatusTypes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<BorrowedItem> BorrowedItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Borrower>().ToTable("borrower");
            modelBuilder.Entity<ItemType>().ToTable("item_type");
            modelBuilder.Entity<Genre>().ToTable("genre");
            modelBuilder.Entity<StatusType>().ToTable("status_type");
            modelBuilder.Entity<Item>().ToTable("item");
            modelBuilder.Entity<BorrowedItem>().ToTable("borrowed_item");

            modelBuilder.Entity<Item>().HasKey(i => new { i.TypeId, i.StatusId, i.GenreId });
        }
    }
}