using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace diskInventory.Models
{
    public class Item
    {
        [Column("item_id")]
        public int Id { get; set; }
        [Column("item_name")]
        [Required(ErrorMessage ="A name is required."), StringLength(100, ErrorMessage ="A name must not be longer than 100 characters.")]
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Column("release_date")]
        [Required(ErrorMessage ="A release date is required."), MinDate(ErrorMessage ="The release date must be in the past and after 1900/01/01.")]
        public DateTime ReleaseDate { get; set; }
        [Column("status_id")]
        [Required(ErrorMessage ="A status is required.")]
        public int? StatusId { get; set; }
        [Column("item_type_id")]
        [Required(ErrorMessage = "A type is required.")]
        public int? TypeId { get; set; }
        [Column("genre_id")]
        [Required(ErrorMessage = "A genre is required.")]
        public int? GenreId { get; set; }

        //nav properties
        public StatusType Status { get; set; }
        public ItemType Type { get; set; }
        public Genre Genre { get; set; }
        public ICollection<BorrowedItem> BorrowedInstances { get; set; }
    }
}
