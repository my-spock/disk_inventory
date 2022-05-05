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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Column("borrowed_date")]
        [Required(ErrorMessage ="Please enter the borrow date."), MinDate(ErrorMessage = "The borrowed date must be in the past and after 1900/01/01.")]
        public DateTime BorrowedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Column("returned_date")]
        [MinDate(ErrorMessage ="The returned date must be in the past and after 1900/01/01.")]
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
