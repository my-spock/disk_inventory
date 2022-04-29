using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace diskInventory.Models
{
    public class BorrowedItem
    {
        [Column("borrowed_id")]
        public int Id { get; set; }
        [Column("borrowed_date")]
        public DateTime BorrowedDate { get; set; }
        [Column("returned_date")]
        [MinDate(ErrorMessage ="The borrowed date must be in the past and before 1900/01/01.")]
        public DateTime? ReturnedDate { get; set; }
        [Column("borrower_id")]
        [Required(ErrorMessage ="Please select a borrower.")]
        public int BorrowerId { get; set; }
        [Column("item_id")]
        [Required(ErrorMessage ="Please select an item.")]
        public int ItemId { get; set; }

        //nav properties
        public Borrower Borrower { get; set; }
        public Item Item { get; set; }
    }
}
