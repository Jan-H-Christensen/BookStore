// See https://aka.ms/new-console-template for more information
using BookStoreConApp.DbServices;
using EFFramework.Data;

Console.WriteLine("get all books!");
var context = new BsDbContextFactory().CreateDbContext();
var service = new SqlService(context);
var books = service.GetBooks();

foreach (var book in books)
{
    Console.WriteLine(book.title);
}

var costumer = service.Login("Kimberly Nichols", "cmartinez@santiago-medina.com");

System.Console.WriteLine("Costumer: " + costumer.name + "costumer id: " + costumer.costumers_id);