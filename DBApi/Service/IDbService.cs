using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFFramework.Model;

namespace DBApi.Service
{
    public interface IDbService
    {
        public Task<Costumers> Login(string name, string email);
        public Task<List<Books>> GetBooks();
        public Task<Books> GetBooksId(int bookId);
        public Task<Author> GetAuthor(int authorId);
        public Task<List<Inventory>> GetInventory();
        public Task<bool> CreateOrder(int costumerId, Dictionary<Books, int> books);
        public Task<bool> UpdateInventory(Inventory inventory);
    }
}