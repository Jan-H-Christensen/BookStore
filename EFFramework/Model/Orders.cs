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
        public int orderId { get; set; }
        [Required]
        [ForeignKey("Costumers")]
        public int costumerId { get; set; }
        [Required]
        public DateTime orderDate { get; set; }
        [Required]
        public int totalAmount { get; set; }
    }
}