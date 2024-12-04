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
        public int orderDetails { get; set; }
        public int orderId { get; set; }
        public int bookId { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
    }
}