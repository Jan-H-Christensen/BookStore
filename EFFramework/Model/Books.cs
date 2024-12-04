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
        public int book_id { get; set; }
        [Required]
        public required string title { get; set; }
        [Required]
        public required string genre { get; set; }
        [Required]
        public required decimal price { get; set; }
        [Required]
        [ForeignKey("authorId")]
        public required int author_Id { get; set; }
        [Required]
        public required int stock_level { get; set; }
    }
}