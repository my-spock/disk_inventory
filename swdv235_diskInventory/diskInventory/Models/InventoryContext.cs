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
            //define one-to-many relationship b/t genre and item
            modelBuilder.Entity<Item>()
                .HasOne(i => i.Genre)
                .WithMany(g => g.Items);
            modelBuilder.Entity<Genre>()
                .HasMany(g => g.Items)
                .WithOne(i => i.Genre)
                .HasForeignKey(i => i.GenreId);

            //define one-to-meny relationship between status type and item
            modelBuilder.Entity<Item>()
               .HasOne(i => i.Status)
               .WithMany(s => s.Items);
            modelBuilder.Entity<StatusType>()
                .HasMany(s => s.Items)
                .WithOne(i => i.Status)
                .HasForeignKey(i => i.StatusId);

            //define one-to-meny relationship between item type and item
            modelBuilder.Entity<Item>()
               .HasOne(i => i.Type)
               .WithMany(t => t.Items);
            modelBuilder.Entity<ItemType>()
                .HasMany(t => t.Items)
                .WithOne(i => i.Type)
                .HasForeignKey(i => i.TypeId);

            //define one-to-many relationship between item and borrowed item
            modelBuilder.Entity<BorrowedItem>()
             .HasOne(bi => bi.Item)
             .WithMany(i => i.BorrowedInstances);
            modelBuilder.Entity<Item>()
                .HasMany(i => i.BorrowedInstances)
                .WithOne(bi => bi.Item)
                .HasForeignKey(bi => bi.ItemId);

            //define one-to-many relationship between borrower and borrowed item
            modelBuilder.Entity<BorrowedItem>()
             .HasOne(bi => bi.Borrower)
             .WithMany(b => b.BorrowedItems);
            modelBuilder.Entity<Borrower>()
                .HasMany(b => b.BorrowedItems)
                .WithOne(bi => bi.Borrower)
                .HasForeignKey(bi => bi.BorrowerId);

            modelBuilder.Entity<Borrower>().ToTable("borrower");
            modelBuilder.Entity<ItemType>().ToTable("item_type");
            modelBuilder.Entity<Genre>().ToTable("genre");
            modelBuilder.Entity<StatusType>().ToTable("status_type");
            modelBuilder.Entity<Item>().ToTable("item");
            modelBuilder.Entity<BorrowedItem>().ToTable("borrowed_item");
        }
    }
}