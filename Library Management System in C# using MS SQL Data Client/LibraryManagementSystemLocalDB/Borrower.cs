using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystemDAL
{
    public class Borrower
    {
        public int BorrowerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public Borrower()
        {
            this.BorrowerId = -1;
            this.Email = string.Empty;
            this.Name = string.Empty;
        }
    }
    
}
 