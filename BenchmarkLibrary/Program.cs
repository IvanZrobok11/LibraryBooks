using LibraryBooks.Models;
using System;
using System.Diagnostics;
using System.Linq;
using LibraryBooks;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BenchmarkLibraryBooks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            DbProceduresService.AddBook(new Book(){Id = 21, BarCode = "21", Name = "SomeBook"});
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
        }
    }

    public static class DbProceduresService
    {
        public static Book FindBookById(int id)
        {
            Book book;
            using (DataBaseContext context = new DataBaseContext())
            {
                NpgsqlParameter param = new NpgsqlParameter(":id", id);
                book = context.Books.FromSqlRaw("select * from find_book_by_id(:id)", param).First();
            }
            return book;
        }

        public static Book GetBookByAuthor(string author)
        {
            Book book;
            using (DataBaseContext context = new DataBaseContext())
            {
                NpgsqlParameter param = new NpgsqlParameter(":author", "Ernest Hemingway");
                book = context.Books.FromSqlRaw("select * from get_books_by_author(:author)", param).First();
            }

            return book;
        }

        public static void AddBook(Book book)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                NpgsqlParameter paramId = new NpgsqlParameter(":id", book.Id);
                NpgsqlParameter paramName = new NpgsqlParameter(":bar_code", book.Name);
                NpgsqlParameter paramBarCode = new NpgsqlParameter(":name", book.BarCode);
                NpgsqlParameter paramDesk = new NpgsqlParameter(":desk", book.Description);
                NpgsqlParameter paramAuthor = new NpgsqlParameter(":author", book.Author);
                NpgsqlParameter paramPrice = new NpgsqlParameter(":price", book.Price);
                NpgsqlParameter paramQuantity = new NpgsqlParameter(":quantity", book.Quantity);
                context.Database
                    .ExecuteSqlRaw(
                        "select add_book(:id, :bar_code, :name, :desk, :author, :price, :quantity)",
                        paramId, paramBarCode, paramName, paramDesk, paramAuthor, paramPrice, paramQuantity);
            }
        }
        public static void UpdateBook(Book book)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                NpgsqlParameter paramId = new NpgsqlParameter(":id", book.Id);
                NpgsqlParameter paramName = new NpgsqlParameter(":bar_code", book.Name);
                NpgsqlParameter paramBarCode = new NpgsqlParameter(":name", book.BarCode);
                NpgsqlParameter paramDesk = new NpgsqlParameter(":desk", book.Description);
                NpgsqlParameter paramAuthor = new NpgsqlParameter(":author", book.Author);
                NpgsqlParameter paramPrice = new NpgsqlParameter(":price", book.Price);
                NpgsqlParameter paramQuantity = new NpgsqlParameter(":quantity", book.Quantity);
                context.Database
                    .ExecuteSqlRaw(
                        "select update_book(:id, :bar_code, :name, :desk, :author, :price, :quantity)",
                        paramId, paramBarCode, paramName, paramDesk, paramAuthor, paramPrice, paramQuantity);
            }
        }

        public static void DeleteBook(int id)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                NpgsqlParameter paramId = new NpgsqlParameter(":id", id);
                context.Database
                    .ExecuteSqlRaw(
                        "select delete_book(:id)", paramId);
            }
        }
        public static void DeleteBook(string code)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                NpgsqlParameter paramCode = new NpgsqlParameter(":code", code);
                context.Database
                    .ExecuteSqlRaw(
                        "select delete_book_by_barcode(:id)", paramCode);
            }
        }
    }
}
