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
        public int order_detail_id { get; set; }
        [Required]
        [ForeignKey("orderId")]
        public int order_id { get; set; }
        [Required]
        [ForeignKey("bookId")]
        public int book_id { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]
        public decimal price { get; set; }
    }
}