using System;
using System.Collections.Generic;
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
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true), Column("release_date")]
        public DateTime ReleaseDate { get; set; }
        [Column("status_id")]
        public int StatusId { get; set; }
        [Column("item_type_id")]
        public int TypeId { get; set; }
        [Column("genre_id")]
        public int GenreId { get; set; }

        //nav properties
        public StatusType Status { get; set; }
        public ItemType Type { get; set; }
        public Genre Genre { get; set; }
        public ICollection<BorrowedItem> BorrowedInstances { get; set; }
    }
}
