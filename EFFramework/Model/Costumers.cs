using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFFramework.Model
{
    public class Costumers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int costumerId { get; set; }
        [Required]
        public required string name { get; set; }
        [Required]
        public required string email { get; set; }
        [Required]
        public required string phone { get; set; }
    }
}