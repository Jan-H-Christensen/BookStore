// See https://aka.ms/new-console-template for more information
using BookStoreConApp.DbServices;
using EFFramework.Data;
using EFFramework.Model;

Console.WriteLine("get all books!");
var context = new BsDbContextFactory().CreateDbContext();
var service = new SqlService(context);
var books = service.GetBooks();

foreach (var book in books)
{
    Console.WriteLine("Author: " + service.GetAuthor(book.author_Id).name + " Title: " + book.title);
}

var costumer = service.Login("Kimberly Nichols", "cmartinez@santiago-medina.com");

System.Console.WriteLine("Costumer: " + costumer.name + "costumer id: " + costumer.costumers_id);

var bookList = new Dictionary<Books, int> { { books[0], 1 }, { books[1], 1 }, { books[2], 1 } };

// foreach (var book in bookList)
// {
//     Console.WriteLine("Book: " + service.GetBook(book.Key.book_id).title + " Quantity: " + book.Value);
// }

var inventory = service.GetInventory();

foreach (var item in inventory)
{
    Console.WriteLine("Book: " + service.GetBook(item.book_id).title + " Inventory: " + item.stock_level);
}

service.CreateOrder(costumer.costumers_id, bookList);

var newInventory = service.GetInventory();

foreach (var item in newInventory)
{
    Console.WriteLine("Book: " + service.GetBook(item.book_id).title + " Inventory: " + item.stock_level);
}
