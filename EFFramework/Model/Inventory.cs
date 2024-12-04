using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFFramework.Model
{
    public class Inventory
    {
        public int bookId { get; set; }
        public int stockLevel { get; set; }
        public DateTime lastUpdated { get; set; }
    }
}