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
        public required int bookId { get; set; }
        [Required]
        public required int stockLevel { get; set; }
        [Required]
        public required DateTime lastUpdated { get; set; }
    }
}