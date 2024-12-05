using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFFramework.Data;
using EFFramework.Model;
using Microsoft.EntityFrameworkCore;

namespace DBApi.Service
{
    public class DbService : IDbService
    {
        private readonly BsDbContext _sqlContext;

        private readonly Client _client;

        public DbService(BsDbContext sqlContext, Client client)
        {
            _sqlContext = sqlContext;
            _client = client;
        }

        public async Task<Costumers> Login(string name, string email)
        {
            try
            {
                await _sqlContext.Database.EnsureCreatedAsync();
                return await _sqlContext.Costumers.Where(c => c.name == name && c.email == email).FirstOrDefaultAsync();
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("costumer not found");
                throw;
            }
        }

        public async Task<List<Books>> GetBooks()
        {
            try
            {
                await _sqlContext.Database.EnsureCreatedAsync();
                var books = await _sqlContext.Books.ToListAsync();
                if (books != null && books.Count > 0)
                {
                    foreach (var book in books)
                    {
                        await _client.SaveBooks(book.book_id.ToString(), book);
                    }
                }
                return books;
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("books not found");
                throw;
            }
        }

        public async Task<Books> GetBooksId(int bookId)
        {
            try
            {
                await _sqlContext.Database.EnsureCreatedAsync();
                return await _sqlContext.Books.Where(b => b.book_id == bookId).FirstOrDefaultAsync();
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("book not found");
                throw;
            }
        }

        public async Task<Author> GetAuthor(int authorId)
        {
            try
            {
                await _sqlContext.Database.EnsureCreatedAsync();

                Task task = new Task(async () => await _client.SaveAuthor(await _sqlContext.Authors.ToListAsync()));
                task.Start();

                return await _sqlContext.Authors.Where(a => a.author_id == authorId).FirstOrDefaultAsync();
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("author not found");
                throw;
            }
        }

        public async Task<List<Inventory>> GetInventory()
        {
            try
            {
                await _sqlContext.Database.EnsureCreatedAsync();
                return await _sqlContext.Inventory.ToListAsync();
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("inventory not found");
                throw;
            }
        }

        public async Task<bool> CreateOrder(int costumerId, Dictionary<Books, int> books)
        {
            try
            {
                await _sqlContext.Database.EnsureCreatedAsync();
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

                    await _sqlContext.Orders!.AddAsync(order);
                    await _sqlContext.SaveChangesAsync();

                    int newOrderId = order.order_id;

                    await CreateOrderDetailByPurchaseOrder(newOrderId, books);

                    foreach (var book in books)
                    {
                        await UpdateInventoryByPurchaseOrder(book.Key.book_id, book.Value);
                        await UpdateBookStockLevel(book.Key.book_id, book.Value);
                    }
                    return true;
                }
                return false;
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("order not created");
                return false;
                throw;
            }
        }

        public async Task<bool> UpdateInventory(Inventory inventory)
        {
            try
            {
                await _sqlContext.Database.EnsureCreatedAsync();
                await _sqlContext.Inventory!.AddAsync(inventory);
                await _sqlContext.SaveChangesAsync();
                return true;
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("inventory not updated");
                return false;
                throw;
            }
        }

        private async Task UpdateInventoryByPurchaseOrder(int bookId, int quantity)
        {
            try
            {
                await _sqlContext.Database.EnsureCreatedAsync();
                var inventory = await _sqlContext.Inventory.Where(i => i.book_id == bookId).FirstOrDefaultAsync();
                inventory.stock_level -= quantity;
                await _sqlContext.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("failed to update inventory by purchase order");
                throw;
            }
        }

        private async Task UpdateBookStockLevel(int bookId, int quantity)
        {
            try
            {
                await _sqlContext.Database.EnsureCreatedAsync();
                var book = await _sqlContext.Books.Where(b => b.book_id == bookId).FirstOrDefaultAsync();
                book.stock_level -= quantity;
                await _sqlContext.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("failed to update book stock level by purchase order");
                throw;
            }
        }

        private async Task CreateOrderDetailByPurchaseOrder(int orderId, Dictionary<Books, int> books)
        {
            try
            {
                await _sqlContext.Database.EnsureCreatedAsync();

                foreach (var book in books)
                {
                    var orderDetail = new OrderDetails
                    {
                        order_id = orderId,
                        book_id = book.Key.book_id,
                        quantity = book.Value,
                        price = book.Key.price
                    };

                    await _sqlContext.OrderDetails!.AddAsync(orderDetail);
                }

                await _sqlContext.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("failed to create order detail by purchase order");
                throw;
            }
        }
    }
}