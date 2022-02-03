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
    public static class DbEfQueryService
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
                book = context.Books.Where(b => b.Author == author);
            }

            return book;
        }


        public static void AddBook(Book book)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                context.Books.Add(book);
                context.SaveChanges();
            }
        }
        public static void UpdateBook(Book book)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                context.Books.Update(book);
                context.SaveChanges();
            }
        }

        public static void DeleteBook(int id, string barCode)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                var result = context.Books.Find(new object[] { id, barCode });
                context.Books.Remove(result);
                context.SaveChanges();
            }
        }
        public static void DeleteBook(string barCode)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                var result = context.Books.Find(barCode);
                context.Books.Remove(result);
                context.SaveChanges();
            }
        }
    }
}
