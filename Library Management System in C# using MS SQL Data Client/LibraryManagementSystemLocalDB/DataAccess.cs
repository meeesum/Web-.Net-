using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryManagementSystemDAL
{
    public class DataAccess
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryManagementSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";


        public void addBook(Book book)
        {
            using (SqlConnection con = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "INSERT INTO BOOK (Title, Author, Genre, IsAvailable) VALUES (@Title,@Author, @Genre, @IsAvailable)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Title", book.Title);
                        cmd.Parameters.AddWithValue("@Author", book.Author);
                        cmd.Parameters.AddWithValue("@Genre", book.Genre);
                        cmd.Parameters.AddWithValue("@IsAvailable", book.IsAvailable);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Book Added Successfully\n");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An Error Occured: " + ex.Message);
                }
            }
        }
        public void removeBook(Book book)
        {
            using (SqlConnection con = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "DELETE FROM BOOK WHERE ID = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", book.BookId);
                        int rowAffeted = cmd.ExecuteNonQuery();

                        if (rowAffeted > 0)
                        {
                            Console.WriteLine("Book Deleted Successfully\n");
                        }
                        else
                        {
                            Console.WriteLine("No book found with ID : " + book.BookId + "\n");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occured : " + ex.Message);
                }
            }
        }
        public void updateBook(Book book)
        {
            using (SqlConnection con = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "UPDATE BOOK SET ";
                    List<string> setClauses = new List<string>();

                    // Adding attributes as provided from the user in query
                    if (!string.IsNullOrEmpty(book.Title))
                    {
                        setClauses.Add("Title = @Title");
                    }
                    if (!string.IsNullOrEmpty(book.Author))
                    {
                        setClauses.Add("Author = @Author");
                    }
                    if (!string.IsNullOrEmpty(book.Genre))
                    {
                        setClauses.Add("Genre = @Genre");
                    }
                    // I am getting the isAvailable attribute from the user
                    setClauses.Add("IsAvailable = @IsAvailable");

                    if (setClauses.Count == 0)
                    {
                        Console.WriteLine("No attributes to update.");
                        return;
                    }

                    // Making the complete query
                    query += string.Join(", ", setClauses) + " WHERE BookId = @BookId";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        if (!string.IsNullOrEmpty(book.Title))
                        {
                            cmd.Parameters.AddWithValue("@Title", book.Title);
                        }
                        if (!string.IsNullOrEmpty(book.Author))
                        {
                            cmd.Parameters.AddWithValue("@Author", book.Author);
                        }
                        if (!string.IsNullOrEmpty(book.Genre))
                        {
                            cmd.Parameters.AddWithValue("@Genre", book.Genre);
                        }
                        // Add the IsAvailable parameter
                        cmd.Parameters.AddWithValue("@IsAvailable", book.IsAvailable);

                        // Add BookId parameter for the WHERE clause
                        cmd.Parameters.AddWithValue("@BookId", book.BookId);

                        // Execute the query to update the book
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Check if any rows were affected
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Book updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("No book found with the specified ID.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message + "\n");
                }
            }
        }
        public void registerBorrower(Borrower borrower)
        {
            using (SqlConnection con = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "INSERT INTO BORROWER (Name, Email) VALUES (@Name, @Email)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", borrower.Name);
                        cmd.Parameters.AddWithValue("Email", borrower.Email);

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("\nBorrower Registered Successfully\n");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occured : " + ex.Message);
                }
            }
        }
        public void updateBorrower(Borrower borrower)
        {
            using (SqlConnection con = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "UPDATE Borrower SET VALUES Name = @Name, Email = @Email WHERE BorrowerId = @BorrowerId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", borrower.Name);
                        cmd.Parameters.AddWithValue("@Email", borrower.Email);
                        cmd.Parameters.AddWithValue("@BorrowerId", borrower.BorrowerId);

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("\nBorrower Updated Successfully\n");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occured " + ex.Message + "\n");
                }
            }
        }
        public void removeBorrower(int bId)
        {
            using (SqlConnection con = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "DELETE FROM BORROWER WHERE BorrowerId = @BorrowerId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@BorrowerId", bId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Borrower Removed Successfully!\n");
                        }
                        else
                        {
                            Console.WriteLine("No such Borrower found in the Database!\n");
                        }

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occured " + ex.Message + "\n");
                }

            }
        }
        public void borrowBook(int BookID, int BorrowerId)
        {
            using (SqlConnection con = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Check if the book is already borrowed
                    string checkQuery = "SELECT IsAvailable FROM Book WHERE BookId = @bookId";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@bookId", BookID);
                        bool isAvailable = (bool)checkCmd.ExecuteScalar();

                        if (isAvailable)
                        {
                            // Insert into TRANSACTION table
                            string insertQuery = "INSERT INTO [TRANSACTION] (BookId, BorrowerId, IsBorrowed, Date) VALUES (@bookId, @borrowerId, @isBorrowed, CAST(GETDATE() AS DATE))";
                            using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                            {
                                cmd.Parameters.AddWithValue("@bookId", BookID);
                                cmd.Parameters.AddWithValue("@borrowerId", BorrowerId);
                                cmd.Parameters.AddWithValue("@isBorrowed", true);
                                int rowsAffected = cmd.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    // Update the IsAvailable attribute in the Book table
                                    string updateQuery = "UPDATE Book SET IsAvailable = @isAvailable WHERE BookId = @bookId";
                                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, con))
                                    {
                                        updateCmd.Parameters.AddWithValue("@isAvailable", false);
                                        updateCmd.Parameters.AddWithValue("@bookId", BookID);
                                        int updateRowsAffected = updateCmd.ExecuteNonQuery();
                                        if (updateRowsAffected > 0)
                                        {
                                            Console.WriteLine("Book Borrowed Successfully!\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine("An error occurred while updating book availability.\n");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("An error occurred while borrowing the book.\n");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("The book is already borrowed.\n");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message + "\n");
                }
            }
        }


        public void returnBook(int bookId)
        {
            using (SqlConnection con = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Update the IsAvailable attribute in the Book table
                    string updateBookQuery = "UPDATE BOOK SET IsAvailable = @isAvailable WHERE BookId = @bookId";
                    using (SqlCommand cmd = new SqlCommand(updateBookQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@isAvailable", true);
                        cmd.Parameters.AddWithValue("@bookId", bookId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected < 1)
                        {
                            Console.WriteLine("An error occurred: Book Id not found\n");
                            return;
                        }
                    }

                    // Update the IsBorrowed attribute in the TRANSACTION table
                    string updateTransactionQuery = "UPDATE [TRANSACTION] SET IsBorrowed = @isBorrowed WHERE BookId = @bookId";
                    using (SqlCommand cmd = new SqlCommand(updateTransactionQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@isBorrowed", false);
                        cmd.Parameters.AddWithValue("@bookId", bookId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("\nBook Returned Successfully\n");
                        }
                        else
                        {
                            Console.WriteLine("Transaction Table not Updated\n");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message + "\n");
                }
            }
        }

        public List<Book> searchBook(string searchTerm)
        {
            List<Book> books = new List<Book>();

            using (SqlConnection con = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT * FROM BOOK WHERE Title LIKE @SearchTerm OR Author LIKE @SearchTerm OR Genre LIKE @SearchTerm";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Book book = new Book
                                ();
                                book.BookId = reader.GetInt32(0);
                                book.Title = reader.GetString(1);
                                book.Author = reader.GetString(2);
                                book.Genre = reader.GetString(3);
                                book.IsAvailable = reader.GetBoolean(4);
                           
                                books.Add(book);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            return books;
        }
        public void ViewAllBooks()
        {
            List<Book> books = new List<Book>();
            using (SqlConnection con = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT * FROM BOOK";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Book book = new Book();
                                book.BookId = reader.GetInt32(0);
                                book.Title = reader.GetString(1);
                                book.Author = reader.GetString(2);
                                book.Genre = reader.GetString(3);
                                book.IsAvailable = reader.GetBoolean(4);

                                // Adding the books to the list I created
                                books.Add(book);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            // Displaying the list of books
            foreach (var book in books)
            {
                Console.WriteLine($"BookId: {book.BookId}, Title: {book.Title}, Author: {book.Author}, Genre: {book.Genre}, IsAvailable: {book.IsAvailable}");
            }
        }
        public void ViewBooksByBorrower(int borrowerId)
        {

            //I am creating a list of book objects to store every book tuple in this list and display every tuple at the end
            List<Book> books = new List<Book>();

            using (SqlConnection con = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT b.BookId, b.Title, b.Author, b.Genre, b.IsAvailable, t.BorrowDate FROM Book b INNER JOIN dbo.[Transaction] t ON b.BookId = t.BookId WHERE t.BorrowerId = @BorrowerId";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@BorrowerId", borrowerId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Book book = new Book();
                                book.BookId = reader.GetInt32(0);
                                book.Title = reader.GetString(1);
                                book.Author = reader.GetString(2);
                                book.Genre = reader.GetString(3);
                                book.IsAvailable = reader.GetBoolean(4);

                                books.Add(book);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message + "\n");
                }
            }

            foreach (var book in books)
            {
                Console.WriteLine($"BookId: {book.BookId}, Title: {book.Title}, Author: {book.Author}, Genre: {book.Genre}, IsAvailable: {book.IsAvailable}");
            }
        }
        public void showBorrowers()
        {
            List<Borrower> borrowers = new List<Borrower>();
            using (SqlConnection con = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT * FROM Borrower";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Borrower borrower = new Borrower();
                                borrower.BorrowerId = reader.GetInt32(0);
                                borrower.Name = reader.GetString(1);
                                borrower.Email = reader.GetString(2);

                                // Adding the borrowers to the list I created
                                borrowers.Add(borrower);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            // Displaying the list of books
            foreach (var borrower in borrowers)
            {
                Console.WriteLine($"Borrower Id: {borrower.BorrowerId}, Name: {borrower.Name}, Email: {borrower.Email}");
            }
        }
    }
    
}