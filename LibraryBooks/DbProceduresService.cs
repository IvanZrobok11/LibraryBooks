using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryBooks.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace LibraryBooks
{
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

        public static IQueryable<Book> FindBooksByAuthor(string author)
        {
            IQueryable<Book> book;
            using (DataBaseContext context = new DataBaseContext())
            {
                NpgsqlParameter param = new NpgsqlParameter(":author", "Ernest Hemingway");
                book = context.Books.FromSqlRaw("select * from get_books_by_author(:author)", param);
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
                NpgsqlParameter paramCode = new NpgsqlParameter(":barCode", code);
                context.Database
                    .ExecuteSqlRaw(
                        "select delete_book_by_barcode(:id)", paramCode);
            }
        }
    }
}
