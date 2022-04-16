using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace diskInventory.Models
{
    public class BorrowedItem
    {
        [Column("borrowed_item_id")]
        public int Id { get; set; }
        [Column("borrowed_date")]
        public DateTime BorrowedDate { get; set; }
        [Column("returned_date")]
        public DateTime? ReturnedDate { get; set; }
        [Column("borrower_id")]
        public Borrower Borrower { get; set; }
        [Column("item_id")]
        public Item Item { get; set; }
    }
}
