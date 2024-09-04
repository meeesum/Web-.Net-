namespace LibraryDAL
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int BookId { get; set; }
        public int BorrowerId { get; set; }
        public DateTime Date { get; set; }
        public bool IsBorrowed { get; set; }

        public Transaction(int transactionId, int bookId, int borrowerId, DateTime date, bool isBorrowed)
        {
            TransactionId = transactionId;
            BookId = bookId;
            BorrowerId = borrowerId;
            Date = date;
            IsBorrowed = isBorrowed;
        }
    }
}
