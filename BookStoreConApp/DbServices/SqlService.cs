using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFFramework.Data;
using EFFramework.Model;

namespace BookStoreConApp.DbServices
{
    public class SqlService
    {
        private readonly BsDbContext _sqlContext;

        public SqlService(BsDbContext sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public Costumers Login(string name, string email)
        {
            _sqlContext.Database.EnsureCreated();
            return _sqlContext.Costumers.Where(c => c.name == name && c.email == email).FirstOrDefault();
        }


        public List<Books> GetBooks()
        {
            _sqlContext.Database.EnsureCreated();
            return _sqlContext.Books.ToList();
        }

        public void CreateOrder(Orders order)
        {
            _sqlContext.Database.EnsureCreated();
            if (order != null)
            {
                _sqlContext.Orders!.Add(order);
                UpdateInventoryByPurchaseOrder(order.order_id, order.total_amount);
                UpdateBookStockLevel(order.order_id, order.total_amount);
                _sqlContext.SaveChanges();
            }
        }

        public void CreateOrderDetail(OrderDetails orderDetail)
        {

        }

        public void UpdateInventory(Inventory inventory)
        {

        }

        private void UpdateInventoryByPurchaseOrder(int bookId, int quantity)
        {
            _sqlContext.Database.EnsureCreated();
            _sqlContext.Inventory.Where(i => i.book_id == bookId).FirstOrDefault().stock_level -= quantity;
            _sqlContext.SaveChanges();
        }

        private void UpdateBookStockLevel(int bookId, int quantity)
        {
            _sqlContext.Database.EnsureCreated();
            _sqlContext.Books.Where(i => i.book_id == bookId).FirstOrDefault().stock_level -= quantity;
            _sqlContext.SaveChanges();
        }
    }
}