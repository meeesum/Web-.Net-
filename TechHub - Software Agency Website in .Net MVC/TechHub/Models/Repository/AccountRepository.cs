using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TechHub.Models.Entities;

namespace TechHub.Models.Repository
{
    public class AccountRepository
    {
        private readonly string _connectionString;

        public AccountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int GetUserIdByEmail(string email)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT Id FROM Users WHERE Email = @Email";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int userId))
                        {
                            return userId;
                        }
                        return -1; // Return null if no user is found
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                Console.WriteLine($"An error occurred: {ex.Message}");
                return -1;
            }
        }
        public string GetUsernameByUserId(int userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT Name FROM Users WHERE Id = @UserId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);

                        object result = command.ExecuteScalar();

                        return result?.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public List<Project> GetProjectsByUserId(int userId)
        {
            try
            {
                var projects = new List<Project>();

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT Id, Name, Description FROM Projects WHERE UserId = @UserId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                projects.Add(new Project
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Description = reader.GetString(2)
                                });
                            }
                        }
                    }
                }

                return projects;
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Project>();
            }
        }
        public bool Register(Register newUser)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Users (Name, Email, Password) VALUES (@Name, @Email, @Password)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", newUser.Name);
                        command.Parameters.AddWithValue("@Email", newUser.Email);
                        command.Parameters.AddWithValue("@Password", newUser.Password);

                        int result = command.ExecuteNonQuery();

                        // Check if the insertion was successful
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public bool UserLogin(string email, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email AND Password = @Password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);

                        int userCount = (int)command.ExecuteScalar();

                        // Return true if the user exists, otherwise false
                        return userCount > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public bool AdminLogin(string email, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // Assuming admin check might be different, otherwise this can be used similarly to UserLogin
                    string query = "SELECT COUNT(*) FROM Admin WHERE Email = @Email AND Password = @Password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);

                        int adminCount = (int)command.ExecuteScalar();

                        // Return true if the admin exists, otherwise false
                        return adminCount > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public bool ChangeAdminPassword(string email, string oldPassword, string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
            {
                return false;
            }

            // SQL logic to check the old password and update with the new password
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE Admin SET Password = @NewPassword WHERE Email = @Email AND Password = @OldPassword";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewPassword", newPassword);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@OldPassword", oldPassword);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public void AddEmployee(Employee employee)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Employees (Name, Email, Contact) VALUES (@Name, @Email, @Contact)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@Email", employee.Email);
                    command.Parameters.AddWithValue("@Contact", employee.Contact);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Employees";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new Employee
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Email = reader["Email"].ToString(),
                                Contact = reader["Contact"].ToString()
                            });
                        }
                    }
                }
            }

            return employees;
        }


        public Employee GetEmployeeById(int id)
        {
            Employee employee = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Employees WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            employee = new Employee
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Email = reader["Email"].ToString(),
                                Contact = reader["Contact"].ToString()
                            };
                        }
                    }
                }
            }
            return employee;
        }

        public bool UpdateEmployee(Employee employee)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE Employees SET Name = @Name, Email = @Email, Contact = @Contact WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", employee.Id);
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@Email", employee.Email);
                    command.Parameters.AddWithValue("@Contact", employee.Contact);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }


        public bool DeleteEmployee(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Start a transaction
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Delete records from Skills table first
                        string deleteSkillsQuery = "DELETE FROM Skills WHERE EmployeeId = @Id";
                        using (var command = new SqlCommand(deleteSkillsQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Id", id);
                            command.ExecuteNonQuery();
                        }

                        // Delete the employee record
                        string deleteEmployeeQuery = "DELETE FROM Employees WHERE Id = @Id";
                        using (var command = new SqlCommand(deleteEmployeeQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Id", id);
                            int rowsAffected = command.ExecuteNonQuery();

                            // If rowsAffected is zero, the employee was not found
                            if (rowsAffected == 0)
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }

                        // Commit the transaction
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        // Rollback the transaction in case of error
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        //public IActionResult Details(int id)
        //{
        //    var employee = _accountrepository.GetEmployeeById(id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(employee);
        //}


        public bool UpdateEmployeeImage(int id, string imagePath)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE Employees SET ImagePath = @ImagePath WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@ImagePath", imagePath);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
