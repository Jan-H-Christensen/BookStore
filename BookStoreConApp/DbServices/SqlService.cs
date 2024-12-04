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
            try
            {
                _sqlContext.Database.EnsureCreated();
                return _sqlContext.Costumers.Where(c => c.name == name && c.email == email).FirstOrDefault();
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("costumer not found");
                throw;
            }
        }


        public List<Books> GetBooks()
        {
            try
            {
                _sqlContext.Database.EnsureCreated();
                return _sqlContext.Books.ToList();
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("books not found");
                throw;
            }
        }

        public void CreateOrder(Orders order)
        {
            try
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
            catch (System.Exception)
            {
                System.Console.WriteLine("order not created");
                throw;
            }
        }

        public void CreateOrderDetail(OrderDetails orderDetail)
        {
            try
            {

            }
            catch (System.Exception)
            {
                System.Console.WriteLine("order detail not created");
                throw;
            }
        }

        public void UpdateInventory(Inventory inventory)
        {
            try
            {
                _sqlContext.Database.EnsureCreated();
                _sqlContext.Inventory!.Add(inventory);
                _sqlContext.SaveChanges();
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("inventory not updated");
                throw;
            }
        }

        private void UpdateInventoryByPurchaseOrder(int bookId, int quantity)
        {
            try
            {
                _sqlContext.Database.EnsureCreated();
                _sqlContext.Inventory.Where(i => i.book_id == bookId).FirstOrDefault().stock_level -= quantity;
                _sqlContext.SaveChanges();
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("failed to update inventory by purchase order");
                throw;
            }
        }

        private void UpdateBookStockLevel(int bookId, int quantity)
        {
            try
            {
                _sqlContext.Database.EnsureCreated();
                _sqlContext.Books.Where(b => b.book_id == bookId).FirstOrDefault().stock_level -= quantity;
                _sqlContext.SaveChanges();
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("failed to update book stock level by purchase order");
                throw;
            }
        }
    }
}