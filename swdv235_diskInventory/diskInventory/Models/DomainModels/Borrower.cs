using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage="A valid name is required."), StringLength(100, ErrorMessage = "A name must not be longer than 100 characters.")]
        public string FullName { get; set; }
        [Column("borrower_phone_number")]
        [Required(ErrorMessage ="A valid phone number is required."), RegularExpression(@"^[0-9]{10}$", ErrorMessage ="A phone number must contain 10 digits, with no special characters.")]
        public string Phone { get; set; }

        //nav property
        public ICollection<BorrowedItem> BorrowedItems { get; set; }
    }
}
