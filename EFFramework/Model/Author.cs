using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFFramework.Model
{
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int author_id { get; set; }

        [Required]
        public required string name { get; set; }
        public string? country { get; set; }
        public DateTime? date_of_birth { get; set; }
    }
}