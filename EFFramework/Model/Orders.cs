using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFFramework.Model
{
    public class Orders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int order_id { get; set; }
        [Required]
        [ForeignKey("Costumers")]
        public int costumers_id { get; set; }
        [Required]
        public DateTime order_date { get; set; }
        [Required]
        public decimal total_amount { get; set; }
    }
}