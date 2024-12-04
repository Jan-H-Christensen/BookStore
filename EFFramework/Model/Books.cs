using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFFramework.Model
{
    public class Books
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int bookId { get; set; }
        [Required]
        public required string bookTitle { get; set; }
        [Required]
        public required string genre { get; set; }
        [Required]
        public required int authorId { get; set; }
        [Required]
        public required string bookPrice { get; set; }
        [Required]
        public required int stockLevel { get; set; }
    }
}