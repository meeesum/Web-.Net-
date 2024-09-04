using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystemDAL
{
    public class Book
{
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public bool IsAvailable { get; set; }

        public Book()
        {
            this.BookId = -1;
            this.Title = string.Empty;
            this.Author = string.Empty;
            this.Genre = string.Empty;
            this.IsAvailable = true;
        }
        
}
}
