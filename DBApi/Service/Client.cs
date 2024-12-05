using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFFramework.Model;
using StackExchange.Redis;

namespace DBApi.Service
{
    public class Client
    {
         private readonly string _hostname;
        private readonly int _port;
        private readonly string _password;
        private ConnectionMultiplexer _redis;

        public Client(string hostname, int port, string password)
        {
            _hostname = hostname;
            _port = port;
            _password = password;
        }

        public void Connect()
        {
            _redis = ConnectionMultiplexer.Connect($"{_hostname}:{_port},password={_password}");
        }

        public IDatabase GetDatabase()
        {
            return _redis.GetDatabase();
        }

        public async Task SaveBooks(string BookId, Books books)
        {
            var database = GetDatabase();
            await database.StringSetAsync(BookId, Newtonsoft.Json.JsonConvert.SerializeObject(books));
            await database.SetAddAsync("Books:Index", BookId);
        }

        public async Task SaveAuthor(List<Author> author)
        {
            var database = GetDatabase();
            foreach (var item in author)
            {
                await database.StringSetAsync(item.author_id.ToString(), Newtonsoft.Json.JsonConvert.SerializeObject(item));
                await database.SetAddAsync("Author:Index", item.author_id.ToString());
            }
        }

        public async Task<List<Books>> GetBooks()
        {
            var database = GetDatabase();
            
            var bookKeys = await database.SetMembersAsync("Books:Index");
            var allBooks = new List<Books>();

            foreach (var key in bookKeys)
            {
                var bookData = await database.StringGetAsync(key.ToString());
                if (bookData.HasValue)
                {
                    allBooks.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<Books>(bookData));
                }
            }

            return allBooks;
        }

        public async Task<Books?> GetBooksById(string id)
        {
            var database = GetDatabase();
            
            var bookKeys = await database.SetMembersAsync("Books:Index");

            foreach (var key in bookKeys)
            {
                var bookData = await database.StringGetAsync(key.ToString());
                if (bookData.HasValue)
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Books>(bookData);
                }
            }
            return null;
        }

        public async Task<Author?> GetAuthor(string id)
        {
            var database = GetDatabase();
            
            var authorKeys = await database.SetMembersAsync("Author:Index");

            foreach (var key in authorKeys)
            {
                var authorData = await database.StringGetAsync(key.ToString());
                if (authorData.HasValue)
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Author>(authorData);
                }
            }
            return null;
        }

        public async Task CashUpdateBooks(string id)
        {
            var database = GetDatabase();
            var bookIds = await database.SetMembersAsync("Books:Index");
            foreach (var bookId in bookIds)
            {
                await database.KeyDeleteAsync(bookId.ToString());
            }
            await database.KeyDeleteAsync("Books:Index");
        }

        public async Task CashUpdateAuthor(string id)
        {
            var database = GetDatabase();
            var AuthorIds = await database.SetMembersAsync("Author:Index");
            foreach (var AuthorId in AuthorIds)
            {
                await database.KeyDeleteAsync(AuthorId.ToString());
            }
            await database.KeyDeleteAsync("Author:Index");
        }

        public bool RateLimit(string userId, string postId)
        {
            var key = $"{userId}{postId}";
            var userComments = GetDatabase().StringGet(key);

            int commentsCount = userComments.HasValue ? int.Parse(userComments.ToString()) : 0;
            if (commentsCount >= 5)
            {
                return false;
            }

            GetDatabase().StringIncrement(key);
            if (commentsCount == 0)
            {
                GetDatabase().KeyExpire(key, TimeSpan.FromSeconds(30));
            }
            return true;
        }
    }
}