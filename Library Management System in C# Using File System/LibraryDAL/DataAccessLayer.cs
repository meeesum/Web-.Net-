using System;
using System.Collections.Generic;
using System.IO;

namespace LibraryDAL
{
    public class DataAccessLayer
    {
        private const string BooksFile = "books.txt";
        private const string BorrowersFile = "borrowers.txt";
        private const string TransactionsFile = "transactions.txt";

        public void AddBook(Book book)
        {
            using (StreamWriter writer = new StreamWriter(BooksFile, true))
            {
                writer.WriteLine($"{book.BookId},{book.Title},{book.Author},{book.Genre},{book.IsAvailable}");
            }
        }

        public void RemoveBook(int bookId)
        {
            var books = GetAllBooks();
            books.RemoveAll(b => b.BookId == bookId);
            WriteAllBooks(books);
        }

        public void UpdateBook(Book updatedBook)
        {
            var books = GetAllBooks();
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].BookId == updatedBook.BookId)
                {
                    books[i] = updatedBook;
                    break;
                }
            }
            WriteAllBooks(books);
        }

        public List<Book> GetAllBooks()
        {
            var books = new List<Book>();
            if (File.Exists(BooksFile))
            {
                using (StreamReader reader = new StreamReader(BooksFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(',');
                        if (parts.Length == 5)
                        {
                            books.Add(new Book(
                                int.Parse(parts[0]),
                                parts[1],
                                parts[2],
                                parts[3])
                            {
                                IsAvailable = bool.Parse(parts[4])
                            });
                        }
                    }
                }
            }
            return books;
        }

        public Book GetBookById(int bookId)
        {
            var books = GetAllBooks();
            return books.Find(b => b.BookId == bookId);
        }

        public List<Book> SearchBooks(string query)
        {
            var books = GetAllBooks();
            query = query.ToLower();
            return books.FindAll(b =>
                b.Title.ToLower().Contains(query) ||
                b.Author.ToLower().Contains(query) ||
                b.Genre.ToLower().Contains(query)
            );
        }

        public void RegisterBorrower(Borrower borrower)
        {
            using (StreamWriter writer = new StreamWriter(BorrowersFile, true))
            {
                writer.WriteLine($"{borrower.BorrowerId},{borrower.Name},{borrower.Email}");
            }
        }

        public void UpdateBorrower(Borrower updatedBorrower)
        {
            var borrowers = GetAllBorrowers();
            for (int i = 0; i < borrowers.Count; i++)
            {
                if (borrowers[i].BorrowerId == updatedBorrower.BorrowerId)
                {
                    borrowers[i] = updatedBorrower;
                    break;
                }
            }
            WriteAllBorrowers(borrowers);
        }

        public void DeleteBorrower(int borrowerId)
        {
            var borrowers = GetAllBorrowers();
            borrowers.RemoveAll(b => b.BorrowerId == borrowerId);
            WriteAllBorrowers(borrowers);
        }

        public void RecordTransaction(Transaction transaction)
        {
            using (StreamWriter writer = new StreamWriter(TransactionsFile, true))
            {
                writer.WriteLine($"{transaction.TransactionId},{transaction.BookId},{transaction.BorrowerId},{transaction.Date},{transaction.IsBorrowed}");
            }
        }

        public List<Transaction> GetBorrowedBooksByBorrower(int borrowerId)
        {
            var transactions = GetAllTransactions();
            return transactions.FindAll(t => t.BorrowerId == borrowerId && t.IsBorrowed);
        }

        private void WriteAllBooks(List<Book> books)
        {
            using (StreamWriter writer = new StreamWriter(BooksFile, false))
            {
                foreach (var book in books)
                {
                    writer.WriteLine($"{book.BookId},{book.Title},{book.Author},{book.Genre},{book.IsAvailable}");
                }
            }
        }

        private List<Borrower> GetAllBorrowers()
        {
            var borrowers = new List<Borrower>();
            if (File.Exists(BorrowersFile))
            {
                using (StreamReader reader = new StreamReader(BorrowersFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(',');
                        if (parts.Length == 3)
                        {
                            borrowers.Add(new Borrower(
                                int.Parse(parts[0]),
                                parts[1],
                                parts[2]
                            ));
                        }
                    }
                }
            }
            return borrowers;
        }

        private void WriteAllBorrowers(List<Borrower> borrowers)
        {
            using (StreamWriter writer = new StreamWriter(BorrowersFile, false))
            {
                foreach (var borrower in borrowers)
                {
                    writer.WriteLine($"{borrower.BorrowerId},{borrower.Name},{borrower.Email}");
                }
            }
        }

        private List<Transaction> GetAllTransactions()
        {
            var transactions = new List<Transaction>();
            if (File.Exists(TransactionsFile))
            {
                using (StreamReader reader = new StreamReader(TransactionsFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(',');
                        if (parts.Length == 5)
                        {
                            transactions.Add(new Transaction(
                                int.Parse(parts[0]),
                                int.Parse(parts[1]),
                                int.Parse(parts[2]),
                                DateTime.Parse(parts[3]),
                                bool.Parse(parts[4])
                            ));
                        }
                    }
                }
            }
            return transactions;
        }
    }
}
