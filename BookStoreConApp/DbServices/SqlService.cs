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

        public void CreateOrder(int costumerId, Dictionary<Books, int> books)
        {
            try
            {
                _sqlContext.Database.EnsureCreated();
                if (books != null)
                {
                    var totalPrice = 0.0m;

                    foreach (var book in books)
                    {
                        var calc = book.Key.price * book.Value;
                        totalPrice += calc;
                    }

                    var order = new Orders
                    {
                        costumers_id = costumerId,
                        total_amount = totalPrice,
                        order_date = DateTime.Now
                    };

                    _sqlContext.Orders!.Add(order);
                    _sqlContext.SaveChanges();

                    int newOrderId = order.order_id;

                    CreateOrderDetailByPurchaseOrder(newOrderId, books);

                    foreach (var book in books)
                    {
                        UpdateInventoryByPurchaseOrder(book.Key.book_id, book.Value);
                        UpdateBookStockLevel(book.Key.book_id, book.Value);
                    }
                }
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("order not created");
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

        private void CreateOrderDetailByPurchaseOrder(int orderId, Dictionary<Books, int> books)
        {
            try
            {
                _sqlContext.Database.EnsureCreated();

                foreach (var book in books)
                {
                    var orderDetail = new OrderDetails
                    {
                        order_id = orderId,
                        book_id = book.Key.book_id,
                        quantity = book.Value,
                        price = book.Key.price
                    };

                    _sqlContext.OrderDetails!.Add(orderDetail);
                }

                _sqlContext.SaveChanges();
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("failed to create order detail by purchase order");
                throw;
            }
        }
    }
}