using Microsoft.Data.SqlClient;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Thread.Sleep(60000);
        string masterConnectionString = "Server=bookstore;Initial Catalog=master;User ID=sa;Password=SuperSecret7!;TrustServerCertificate=True;";

        using (SqlConnection conn = new SqlConnection(masterConnectionString))
        {
            conn.Open();
            string checkDbQuery = "IF DB_ID('BookStoreDB') IS NULL CREATE DATABASE BookStoreDB";
            using (SqlCommand cmd = new SqlCommand(checkDbQuery, conn))
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("Database checked or created.");
            }
        }

        string connectionString = "Server=bookstore;Initial Catalog=BookStoreDB;User ID=sa;Password=SuperSecret7!;TrustServerCertificate=True;";

        string currentDir = Directory.GetCurrentDirectory();
        string sqlFilePath = Path.Combine(currentDir, "db_setup.sql");

        if (File.Exists(sqlFilePath))
        {
            string sqlQuery = File.ReadAllText(sqlFilePath);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.ExecuteNonQuery();
            }
        }
        else
        {
            Console.WriteLine("SQL file not found!");
            Console.WriteLine(currentDir);
        }
    }
}