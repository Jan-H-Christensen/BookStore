using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DBApi.Service;
using EFFramework.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DBApi.Controllers
{
    [ApiController]
    [Route("[controller]")] 
    public class DbCommunicationController : Controller
    {
        private readonly IDbService _dbService;
        public DbCommunicationController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("Login")]
        public async Task<Costumers> Login(string name, string email)
        {
            try
            {
                return await _dbService.Login(name, email);
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("costumer not found");
                throw;
            }
        }

        [HttpGet("GetBooks")]
        public async Task<List<Books>> GetBooks()
        {
            try
            {
                return await _dbService.GetBooks();
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("books not found");
                throw;
            }
        }

        [HttpGet("GetBooksId")]
        public async Task<Books> GetBooksId(int bookId)
        {
            try
            {
                return await _dbService.GetBooksId(bookId);
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("book not found");
                throw;
            }
        }

        [HttpGet("GetAuthor")]
        public async Task<Author> GetAuthor(int authorId)
        {
            try
            {
                return await _dbService.GetAuthor(authorId);
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("author not found");
                throw;
            }
        }

        [HttpGet("GetInventory")]
        public async Task<List<Inventory>> GetInventory()
        {
            try
            {
                return await _dbService.GetInventory();
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("inventory not found");
                throw;
            }
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(int costumerId,[FromBody] Dictionary<Books, int> books)
        {
            try
            {
                var status = await _dbService.CreateOrder(costumerId, books);
                if (status)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("order not created");
                throw;
            }
        }

        [HttpPut("UpdateInventory")]
        public async Task<IActionResult> UpdateInventory([FromBody] Inventory inventory)
        {
            try
            {
                var status = await _dbService.UpdateInventory(inventory);
                if (status)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("inventory not updated");
                throw;
            }
        }
    }
}