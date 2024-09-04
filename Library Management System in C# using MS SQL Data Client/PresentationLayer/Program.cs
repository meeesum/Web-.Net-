using System;
using LibraryManagementSystemDAL;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;



class Program
    {
        static DataAccess dataAccess = new DataAccess();

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                exit = ShowMenu();
            }

            Console.WriteLine("Exiting application. Press any key to close.");
            Console.ReadKey();
        }

        static bool ShowMenu()
        {
            Console.WriteLine("Library Management System");
            Console.WriteLine("1. Add a new book");
            Console.WriteLine("2. Remove a book");
            Console.WriteLine("3. Update a book");
            Console.WriteLine("4. Register a new borrower");
            Console.WriteLine("5. Update a borrower");
            Console.WriteLine("6. Delete a borrower");
            Console.WriteLine("7. Borrow a book");
            Console.WriteLine("8. Return a book");
            Console.WriteLine("9. Search for books by title, author, or genre");
            Console.WriteLine("10. View all books");
            Console.WriteLine("11. View borrowed books by a specific borrower");
            Console.WriteLine("12. Show Borrowers");
            Console.WriteLine("13. Exit the application");
            Console.Write("Select an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    AddBook();
                    break;
                case "2":
                    RemoveBook();
                    break;
                case "3":
                    UpdateBook();
                    break;
                case "4":
                    RegisterBorrower();
                    break;
                case "5":
                    UpdateBorrower();
                    break;
                case "6":
                    DeleteBorrower();
                    break;
                case "7":
                    BorrowBook();
                    break;
                case "8":
                    ReturnBook();
                    break;
                case "9":
                    SearchBook();
                    break;
                case "10":
                    ViewAllBooks();
                    break;
                case "11":
                    ViewBooksByBorrower();
                    break;case "12":
                    ShowBorrowers();
                    break;
                case "13":
                    return true; // Exit the application
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            Console.WriteLine();
            return false; // Continue showing the menu
        }

        static void AddBook()
        {
            Console.Write("Enter Title: ");
            string title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Title cannot be empty.");
                return;
            }

            Console.Write("Enter Author: ");
            string author = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(author))
            {
                Console.WriteLine("Author cannot be empty.");
                return;
            }

            Console.Write("Enter Genre: ");
            string genre = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(genre))
            {
                Console.WriteLine("Genre cannot be empty.");
                return;
            }

            Console.Write("Is the book available ( true(1) / false(0) ): ");
            bool isAvailable = bool.TryParse(Console.ReadLine(), out bool available) ? available : true;

        Book book = new Book();
        book.Title = title;
        book.Author = author;
        book.Genre = genre;
        book.IsAvailable = isAvailable;

            dataAccess.addBook(book);
        }

        static void UpdateBook()
        {
            // Create an instance of DataAccess to interact with the database
            DataAccess dataAccess = new DataAccess();

            // Prompt for BookId and validate input
            Console.Write("Enter BookId: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("Invalid BookId.");
                return;
            }

            // Prompt for new Title
            Console.Write("Enter new Title (or leave empty to skip): ");
            string title = Console.ReadLine();

            // Prompt for new Author
            Console.Write("Enter new Author (or leave empty to skip): ");
            string author = Console.ReadLine();

            // Prompt for new Genre
            Console.Write("Enter new Genre (or leave empty to skip): ");
            string genre = Console.ReadLine();

            // Prompt for new Availability and validate input
            Console.Write("Enter new Availability (true/false): ");
            string availability = Console.ReadLine();

            bool isAvailable;
            if (!bool.TryParse(availability, out isAvailable))
            {
                Console.WriteLine("Invalid input. Please enter 'true' or 'false'.");
                return;
            }


        // Create a new Book object with the provided data
        Book book = new Book();
        book.BookId = bookId;
        book.Genre = genre;
        book.IsAvailable = isAvailable;
        book.Title = title;
            // Call the updateBook method to update the book in the database
            dataAccess.updateBook(book);

            Console.WriteLine("\nBook updated successfully.\n");
        }

        static void RemoveBook()
        {
            Console.Write("Enter BookId: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("Invalid BookId.");
                return;
            }

            Book book = new Book();
        book.BookId = bookId;
            dataAccess.removeBook(book);
        }

        static void RegisterBorrower()
        {
            Console.Write("Enter Borrower Name: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                return;
            }

            Console.Write("Enter Borrower Email: ");
            string email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            {
                Console.WriteLine("Invalid email format.");
                return;
            }

            Borrower borrower = new Borrower();
        borrower.Name = name;
        borrower.Email = email;
            dataAccess.registerBorrower(borrower);
        }

        static void UpdateBorrower()
        {
            Console.Write("Enter BorrowerId: ");
            if (!int.TryParse(Console.ReadLine(), out int borrowerId))
            {
                Console.WriteLine("Invalid BorrowerId.");
                return;
            }

            Console.Write("Enter new Name (or leave empty to skip): ");
            string name = Console.ReadLine();

            Console.Write("Enter new Email (or leave empty to skip): ");
            string email = Console.ReadLine();

            Borrower borrower = new Borrower();
        borrower.BorrowerId = borrowerId;
        borrower.Email = email;
        borrower.Name = name;
            dataAccess.updateBorrower(borrower);
        }

        static void DeleteBorrower()
        {
            Console.Write("Enter BorrowerId: ");
            if (!int.TryParse(Console.ReadLine(), out int borrowerId))
            {
                Console.WriteLine("Invalid BorrowerId.");
                return;
            }

            dataAccess.removeBorrower(borrowerId);
        }

        static void BorrowBook()
        {
            Console.Write("Enter BookId: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("Invalid BookId.");
                return;
            }

            Console.Write("Enter BorrowerId: ");
            if (!int.TryParse(Console.ReadLine(), out int borrowerId))
            {
                Console.WriteLine("Invalid BorrowerId.");
                return;
            }

            dataAccess.borrowBook(bookId, borrowerId);
        }

        static void ReturnBook()
        {
            Console.Write("Enter BookId: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("Invalid BookId.");
                return;
            }

            dataAccess.returnBook(bookId);
        }

        static void SearchBook()
        {
            // Create an instance of DataAccess to interact with the database
            DataAccess dataAccess = new DataAccess();

            // Prompt for search term
            Console.Write("Enter search term for Title, Author, or Genre: ");
            string searchTerm = Console.ReadLine();

            // Perform the search
            List<Book> books = dataAccess.searchBook(searchTerm);

            // Display search results
            if (books.Count == 0)
            {
                Console.WriteLine("No books found matching the search term.");
            }
            else
            {
                Console.WriteLine("Books found:");
                foreach (var book in books)
                {
                    Console.WriteLine($"BookId: {book.BookId}, Title: {book.Title}, Author: {book.Author}, Genre: {book.Genre}, IsAvailable: {book.IsAvailable}");
                }
            }
        }

        static void ViewAllBooks()
        {
            dataAccess.ViewAllBooks();
        }

        static void ViewBooksByBorrower()
        {
            Console.Write("Enter BorrowerId: ");
            if (!int.TryParse(Console.ReadLine(), out int borrowerId))
            {
                Console.WriteLine("Invalid BorrowerId.");
                return;
            }

            dataAccess.ViewBooksByBorrower(borrowerId);
        }

        static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    static void ShowBorrowers()
    {
        dataAccess.showBorrowers();
    }
    }

