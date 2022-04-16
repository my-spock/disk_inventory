using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace diskInventory.Models
{
    public class Genre
    {
        [Column("genre_id")]
        public int Id { get; set; }
        [Column("genre_name")]
        public string Name { get; set; }
    }
}
