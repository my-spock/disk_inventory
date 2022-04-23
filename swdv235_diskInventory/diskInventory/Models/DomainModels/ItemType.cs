using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace diskInventory.Models
{
    public class ItemType
    {
        [Column("item_type_id")]
        public int Id { get; set; }
        [Column("item_type_name")]
        public string Name { get; set; }

        //nav property
        public ICollection<Item> Items { get; set; }
    }
}
