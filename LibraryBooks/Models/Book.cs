using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBooks.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string BarCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public float? Price { get; set; }
        public int? Quantity { get; set; }
    }
}
