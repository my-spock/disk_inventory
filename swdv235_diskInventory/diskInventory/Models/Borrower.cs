using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace diskInventory.Models
{
    public class Borrower
    {
        [Column("borrower_id")]
        public int Id { get; set; }
        [Column("borrower_name")]
        public string FullName { get; set; }
        [Column("borrower_phone_number")]
        public string Phone { get; set; }

        //nav property
        public ICollection<BorrowedItem> BorrowedItems { get; set; }
    }
}
