using LibraryBooks.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryBooks
{
    internal class Program
    {
        private static Book _book = new Book()
        {
            Id = 21,
            BarCode = "21",
            Name = "SomeBook",
            Description = "",
            Author = "Dostoevsky",
            Quantity = 1,
            Price = 1
        };

        static void Main(string[] args)
        {
            Console.WriteLine("ADD");
            Console.WriteLine("EfAddBook " + CountTimeEfAddBook());
            Console.WriteLine("DbProcedureAddBook " + CountTimeDbProcedureAddBook());
            Console.WriteLine();

            Console.WriteLine("FIND BY ID");
            Console.WriteLine("EfAFindBook " + CountTimeEfFindBook());
            Console.WriteLine("DbProcedureFindBook " + CountTimeDbProcedureFindBook());
            Console.WriteLine();

            Console.WriteLine("FIND ALL BY AUTHOR");
            Console.WriteLine("EfFindAllBook " + CountTimeDbProcedureFindAllBook());
            Console.WriteLine("DbProcedureFindAllBook " + CountTimeDbProcedureFindAllBook());
            Console.WriteLine();

            Console.WriteLine("DELETE");
            Console.WriteLine("DbProcedureDeleteBook " + CountTimeDbProcedureDeleteBook());
            Console.WriteLine("EfDeleteBook " + CountTimeEfDeleteBook());
            Console.WriteLine();

            Console.WriteLine("UPDATE");
            Console.WriteLine("DbProcedureUpdateBook " + CountTimeDbProcedureUpdateBook());
            Console.WriteLine("EfUpdateBook " + CountTimeEfUpdateBook());
        }
        #region

        static long CountTimeDbProcedureDeleteBook()
        {
            var book = DbProceduresService.FindBookById(21);
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            DbProceduresService.DeleteBook(book.Id);
            sw.Stop();
            DbProceduresService.AddBook(book);
            return sw.ElapsedMilliseconds;
        }
        static long CountTimeEfDeleteBook()
        {
            var book = DbProceduresService.FindBookById(21);
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            DbEfQueryService.DeleteBook(book.Id, book.BarCode);
            sw.Stop();
            DbProceduresService.AddBook(book);
            return sw.ElapsedMilliseconds;
        }
        static long CountTimeEfFindAllBook()
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            var book = DbEfQueryService.FindBooksByAuthor("Dostoevsky");
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
        static long CountTimeDbProcedureFindAllBook()
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            DbProceduresService.FindBooksByAuthor(_book.Author);
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
        static long CountTimeEfFindBook()
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            var book = DbEfQueryService.FindBookById(_book.Id);
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
        static long CountTimeDbProcedureFindBook()
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            DbProceduresService.FindBookById(_book.Id);
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
        static long CountTimeEfUpdateBook()
        {
            Stopwatch sw = Stopwatch.StartNew();
            var book = DbProceduresService.FindBookById(21);    
            sw.Start();
            DbEfQueryService.UpdateBook(book);
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
        static long CountTimeDbProcedureUpdateBook()
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            DbProceduresService.UpdateBook(_book);
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
        static long CountTimeDbProcedureAddBook()
        {
            DbProceduresService.DeleteBook(_book.Id);
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            DbProceduresService.AddBook(_book);
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
        static long CountTimeEfAddBook()
        {
            DbProceduresService.DeleteBook(_book.Id);
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            DbEfQueryService.AddBook(_book);
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
        #endregion
    }



}
