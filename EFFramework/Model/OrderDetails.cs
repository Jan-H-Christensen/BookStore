using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFFramework.Model
{
    public class OrderDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int orderDetailsId { get; set; }
        [Required]
        [ForeignKey("orderId")]
        public int orderId { get; set; }
        [Required]
        [ForeignKey("bookId")]
        public int bookId { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]
        public double price { get; set; }
    }
}