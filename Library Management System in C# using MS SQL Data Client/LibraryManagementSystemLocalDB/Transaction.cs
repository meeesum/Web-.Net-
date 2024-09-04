using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystemDAL
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int BookId { get; set; }
        public int BorrowerId { get; set; }
        public DateTime Date { get; set; }
        public bool IsBorrowed {  get; set; }

        public Transaction()
        {
            this.TransactionId = 0;
            this.BookId = -1;
            this.BorrowerId = -1;
            this.Date = DateTime.Now;
            this.IsBorrowed = false;
        }
    }
}
