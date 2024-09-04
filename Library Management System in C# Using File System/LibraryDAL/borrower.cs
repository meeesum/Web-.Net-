namespace LibraryDAL
{
    public class Borrower
    {
        public int BorrowerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public Borrower(int borrowerId, string name, string email)
        {
            BorrowerId = borrowerId;
            Name = name;
            Email = email;
        }
    }
}
