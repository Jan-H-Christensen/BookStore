using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFFramework.Model
{
    public class Inventory
    {
        [Key]
        [ForeignKey("bookId")]
        public required int book_id { get; set; }
        [Required]
        public required int stock_level { get; set; }
        [Required]
        public required DateTime last_updated { get; set; }
    }
}