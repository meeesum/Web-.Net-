namespace LibraryDAL
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public bool IsAvailable { get; set; }

        public Book(int bookId, string title, string author, string genre)
        {
            BookId = bookId;
            Title = title;
            Author = author;
            Genre = genre;
            IsAvailable = true; // Assuming a new book is initially available
        }
    }
}
