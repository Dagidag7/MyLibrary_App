using System;
using System.Data; // Required for DataTable
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibraryApp
{
    public class LibraryDatabaseManager
    {
        // IMPORTANT: Make sure your connection strings match your SQL Server instance.
        // Example: "Data Source=YOUR_SERVER_NAME;Initial Catalog=MyLibraryDB;Integrated Security=True;"
        private readonly string _appConnectionString = "Data Source=LEO\\SQLEXPRESS01;Initial Catalog=MyLibraryDB;Integrated Security=True;";
        private readonly string _masterConnectionString = "Data Source=LEO\\SQLEXPRESS01;Initial Catalog=master;Integrated Security=True;";

        public LibraryDatabaseManager()
        {
            // Constructor
        }

        public void InitializeDatabase()
        {
            try
            {
                CreateDatabaseIfNotExists();
                CreateTablesAndInsertInitialData();
                // MessageBox.Show("Database and tables initialized successfully!", "Database Setup", MessageBoxButtons.OK, MessageBoxIcon.Information); // Optional: You might want to remove this popup after initial setup
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fatal Database Initialization Error: {ex.Message}\n\nApplication cannot proceed.", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void CreateDatabaseIfNotExists()
        {
            using (SqlConnection connection = new SqlConnection(_masterConnectionString))
            {
                connection.Open();
                string createDbQuery = "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'MyLibraryDB') " +
                                       "BEGIN CREATE DATABASE MyLibraryDB; END;";
                using (SqlCommand command = new SqlCommand(createDbQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private void CreateTablesAndInsertInitialData()
        {
            using (SqlConnection connection = new SqlConnection(_appConnectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string sql = @"
                    -- Users Table
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users' AND schema_id = SCHEMA_ID('dbo'))
                    BEGIN
                        CREATE TABLE Users (
                            UserID INT PRIMARY KEY IDENTITY(1,1),
                            Username NVARCHAR(50) NOT NULL UNIQUE,
                            PasswordHash NVARCHAR(255) NOT NULL, -- Storing plain password for simplicity, but hashing is recommended
                            Role NVARCHAR(20) DEFAULT 'User',
                            Email NVARCHAR(100),
                            CreatedDate DATETIME DEFAULT GETDATE()
                        );
                        INSERT INTO Users (Username, PasswordHash, Role) VALUES ('admin', 'password123', 'Admin');
                        INSERT INTO Users (Username, PasswordHash, Role) VALUES ('librarian', 'libpass', 'User');
                    END;

                    -- Books Table
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Books' AND schema_id = SCHEMA_ID('dbo'))
                    BEGIN
                        CREATE TABLE Books (
                            BookID INT PRIMARY KEY IDENTITY(1,1),
                            Title NVARCHAR(255) NOT NULL,
                            Author NVARCHAR(255) NOT NULL,
                            PublicationYear INT,
                            ISBN NVARCHAR(20) UNIQUE,
                            TotalCopies INT NOT NULL DEFAULT 1,
                            AvailableCopies INT NOT NULL DEFAULT 1
                        );
                        INSERT INTO Books (Title, Author, PublicationYear, ISBN, TotalCopies, AvailableCopies)
                        VALUES
                        ('The Great Gatsby', 'F. Scott Fitzgerald', 1925, '978-0743273565', 5, 5),
                        ('1984', 'George Orwell', 1949, '978-0451524935', 3, 3),
                        ('To Kill a Mockingbird', 'Harper Lee', 1960, '978-0446310789', 4, 4);
                    END;

                    -- Borrowers Table
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Borrowers' AND schema_id = SCHEMA_ID('dbo'))
                    BEGIN
                        CREATE TABLE Borrowers (
                            BorrowerID INT PRIMARY KEY IDENTITY(1,1),
                            Name NVARCHAR(255) NOT NULL,
                            Email NVARCHAR(100) UNIQUE,
                            Phone NVARCHAR(20),
                            Address NVARCHAR(255),
                            RegistrationDate DATETIME DEFAULT GETDATE()
                        );
                        INSERT INTO Borrowers (Name, Email, Phone, Address)
                        VALUES
                        ('Alice Smith', 'alice.s@example.com', '111-222-3333', '123 Main St'),
                        ('Bob Johnson', 'bob.j@example.com', '444-555-6666', '456 Oak Ave');
                    END;

                    -- IssuedBooks Table
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'IssuedBooks' AND schema_id = SCHEMA_ID('dbo'))
                    BEGIN
                        CREATE TABLE IssuedBooks (
                            IssueID INT PRIMARY KEY IDENTITY(1,1),
                            BookID INT NOT NULL,
                            BorrowerID INT NOT NULL,
                            IssueDate DATETIME NOT NULL DEFAULT GETDATE(),
                            DueDate DATETIME NOT NULL,
                            ReturnDate DATETIME NULL,
                            FOREIGN KEY (BookID) REFERENCES Books(BookID),
                            FOREIGN KEY (BorrowerID) REFERENCES Borrowers(BorrowerID)
                        );
                    END;
                    ";

                    using (SqlCommand command = new SqlCommand(sql, connection, transaction))
                    {
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error creating tables or inserting initial data.", ex);
                }
            }
        }

        public bool AuthenticateUser(string username, string password)
        {
            string query = "SELECT COUNT(1) FROM Users WHERE Username = @Username AND PasswordHash = @PasswordHash;";
            using (SqlConnection connection = new SqlConnection(_appConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@PasswordHash", password);
                    try
                    {
                        connection.Open();
                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Database error during login: {ex.Message}\n\nPlease check your connection string.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An unexpected error occurred during login: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }

        // --- Methods for Books Management ---

        /// <summary>
        /// Retrieves all books from the Books table.
        /// </summary>
        /// <returns>A DataTable containing book records.</returns>
        public DataTable GetAllBooks()
        {
            DataTable dt = new DataTable();
            string query = "SELECT BookID, Title, Author, PublicationYear, ISBN, TotalCopies, AvailableCopies FROM Books;";
            using (SqlConnection connection = new SqlConnection(_appConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dt);
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Database error retrieving books: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An unexpected error occurred retrieving books: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// Adds a new book to the Books table.
        /// </summary>
        /// <returns>True if the book was added successfully, false otherwise.</returns>
        public bool AddBook(string title, string author, int publicationYear, string isbn, int totalCopies)
        {
            string query = "INSERT INTO Books (Title, Author, PublicationYear, ISBN, TotalCopies, AvailableCopies) " +
                           "VALUES (@Title, @Author, @PublicationYear, @ISBN, @TotalCopies, @AvailableCopies);";
            using (SqlConnection connection = new SqlConnection(_appConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@Author", author);
                    command.Parameters.AddWithValue("@PublicationYear", publicationYear);
                    command.Parameters.AddWithValue("@ISBN", isbn);
                    command.Parameters.AddWithValue("@TotalCopies", totalCopies);
                    command.Parameters.AddWithValue("@AvailableCopies", totalCopies); // Initially, available copies = total copies

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2627) // Unique constraint violation error number for SQL Server (for ISBN)
                        {
                            MessageBox.Show("A book with this ISBN already exists.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show($"Database error adding book: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        return false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An unexpected error occurred adding book: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Updates an existing book in the Books table.
        /// </summary>
        /// <returns>True if the book was updated successfully, false otherwise.</returns>
        public bool UpdateBook(int bookId, string title, string author, int publicationYear, string isbn, int totalCopies)
        {
            string query = "UPDATE Books SET Title = @Title, Author = @Author, PublicationYear = @PublicationYear, " +
                           "ISBN = @ISBN, TotalCopies = @TotalCopies, AvailableCopies = @TotalCopies " + // Simplified update for AvailableCopies
                           "WHERE BookID = @BookID;";
            using (SqlConnection connection = new SqlConnection(_appConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@Author", author);
                    command.Parameters.AddWithValue("@PublicationYear", publicationYear);
                    command.Parameters.AddWithValue("@ISBN", isbn);
                    command.Parameters.AddWithValue("@TotalCopies", totalCopies);
                    command.Parameters.AddWithValue("@BookID", bookId);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2627) // Unique constraint violation error number for SQL Server (for ISBN)
                        {
                            MessageBox.Show("Another book with this ISBN already exists. Please use a unique ISBN.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show($"Database error updating book: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        return false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An unexpected error occurred updating book: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Deletes a book from the Books table.
        /// </summary>
        /// <returns>True if the book was deleted successfully, false otherwise.</returns>
        public bool DeleteBook(int bookId)
        {
            string query = "DELETE FROM Books WHERE BookID = @BookID;";
            using (SqlConnection connection = new SqlConnection(_appConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BookID", bookId);
                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 547) // Foreign Key constraint violation error number for SQL Server
                        {
                            MessageBox.Show("Cannot delete book. It is currently referenced in issued books.", "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show($"Database error deleting book: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        return false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An unexpected error occurred deleting book: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }

        // --- Methods for Borrowers Management ---

        /// <summary>
        /// Retrieves all borrowers from the Borrowers table.
        /// </summary>
        /// <returns>A DataTable containing borrower records.</returns>
        public DataTable GetAllBorrowers()
        {
            DataTable dt = new DataTable();
            string query = "SELECT BorrowerID, Name, Email, Phone, Address, RegistrationDate FROM Borrowers;";
            using (SqlConnection connection = new SqlConnection(_appConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dt);
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Database error retrieving borrowers: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An unexpected error occurred retrieving borrowers: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// Adds a new borrower to the Borrowers table.
        /// </summary>
        /// <returns>True if the borrower was added successfully, false otherwise.</returns>
        public bool AddBorrower(string name, string email, string phone, string address)
        {
            string query = "INSERT INTO Borrowers (Name, Email, Phone, Address) VALUES (@Name, @Email, @Phone, @Address);";
            using (SqlConnection connection = new SqlConnection(_appConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(phone) ? (object)DBNull.Value : phone);
                    command.Parameters.AddWithValue("@Address", string.IsNullOrEmpty(address) ? (object)DBNull.Value : address);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2627) // Unique constraint violation for Email
                        {
                            MessageBox.Show("A borrower with this email already exists.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show($"Database error adding borrower: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        return false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An unexpected error occurred adding borrower: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Updates an existing borrower in the Borrowers table.
        /// </summary>
        /// <returns>True if the borrower was updated successfully, false otherwise.</returns>
        public bool UpdateBorrower(int borrowerId, string name, string email, string phone, string address)
        {
            string query = "UPDATE Borrowers SET Name = @Name, Email = @Email, Phone = @Phone, Address = @Address WHERE BorrowerID = @BorrowerID;";
            using (SqlConnection connection = new SqlConnection(_appConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(phone) ? (object)DBNull.Value : phone);
                    command.Parameters.AddWithValue("@Address", string.IsNullOrEmpty(address) ? (object)DBNull.Value : address);
                    command.Parameters.AddWithValue("@BorrowerID", borrowerId);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2627) // Unique constraint violation for Email
                        {
                            MessageBox.Show("A borrower with this email already exists. Please use a unique email.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show($"Database error updating borrower: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        return false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An unexpected error occurred updating borrower: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Deletes a borrower from the Borrowers table.
        /// </summary>
        /// <returns>True if the borrower was deleted successfully, false otherwise.</returns>
        public bool DeleteBorrower(int borrowerId)
        {
            // IMPORTANT: Before deleting a borrower, ensure they have no outstanding issued books.
            // Or set up CASCADE DELETE on the foreign key if appropriate for your business logic.
            // For now, if referenced, the FK constraint will prevent deletion.
            string query = "DELETE FROM Borrowers WHERE BorrowerID = @BorrowerID;";
            using (SqlConnection connection = new SqlConnection(_appConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BorrowerID", borrowerId);
                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 547) // Foreign Key constraint violation error number for SQL Server
                        {
                            MessageBox.Show("Cannot delete borrower. They have issued books that need to be returned first.", "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show($"Database error deleting borrower: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        return false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An unexpected error occurred deleting borrower: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }

        // --- NEW METHODS FOR ISSUED BOOKS MANAGEMENT ---

        /// <summary>
        /// Retrieves all issued books, including book title and borrower name.
        /// </summary>
        /// <returns>A DataTable containing issued book records.</returns>
        public DataTable GetAllIssuedBooks()
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT
                    ib.IssueID,
                    b.BookID,
                    b.Title AS BookTitle,
                    br.BorrowerID,
                    br.Name AS BorrowerName,
                    ib.IssueDate,
                    ib.DueDate,
                    ib.ReturnDate
                FROM IssuedBooks ib
                JOIN Books b ON ib.BookID = b.BookID
                JOIN Borrowers br ON ib.BorrowerID = br.BorrowerID
                ORDER BY ib.IssueDate DESC;";
            using (SqlConnection connection = new SqlConnection(_appConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dt);
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Database error retrieving issued books: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An unexpected error occurred retrieving issued books: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// Retrieves book titles and IDs for populating a combo box.
        /// </summary>
        /// <returns>A DataTable with BookID and Title columns.</returns>
        public DataTable GetAllBooksForComboBox()
        {
            DataTable dt = new DataTable();
            string query = "SELECT BookID, Title FROM Books ORDER BY Title;";
            using (SqlConnection connection = new SqlConnection(_appConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dt);
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Database error retrieving books for combo box: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An unexpected error occurred retrieving books for combo box: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// Retrieves borrower names and IDs for populating a combo box.
        /// </summary>
        /// <returns>A DataTable with BorrowerID and Name columns.</returns>
        public DataTable GetAllBorrowersForComboBox()
        {
            DataTable dt = new DataTable();
            string query = "SELECT BorrowerID, Name FROM Borrowers ORDER BY Name;";
            using (SqlConnection connection = new SqlConnection(_appConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dt);
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Database error retrieving borrowers for combo box: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An unexpected error occurred retrieving borrowers for combo box: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// Issues a book to a borrower. Decrements AvailableCopies.
        /// </summary>
        /// <returns>True if the book was issued successfully, false otherwise.</returns>
        public bool IssueBook(int bookId, int borrowerId, DateTime issueDate, DateTime dueDate)
        {
            using (SqlConnection connection = new SqlConnection(_appConnectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // 1. Check available copies
                    string checkCopiesQuery = "SELECT AvailableCopies FROM Books WHERE BookID = @BookID;";
                    using (SqlCommand checkCommand = new SqlCommand(checkCopiesQuery, connection, transaction))
                    {
                        checkCommand.Parameters.AddWithValue("@BookID", bookId);
                        int availableCopies = (int)checkCommand.ExecuteScalar();

                        if (availableCopies <= 0)
                        {
                            transaction.Rollback();
                            MessageBox.Show("No available copies of this book to issue.", "Issue Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    // 2. Insert into IssuedBooks
                    string insertIssueQuery = "INSERT INTO IssuedBooks (BookID, BorrowerID, IssueDate, DueDate) VALUES (@BookID, @BorrowerID, @IssueDate, @DueDate);";
                    using (SqlCommand insertCommand = new SqlCommand(insertIssueQuery, connection, transaction))
                    {
                        insertCommand.Parameters.AddWithValue("@BookID", bookId);
                        insertCommand.Parameters.AddWithValue("@BorrowerID", borrowerId);
                        insertCommand.Parameters.AddWithValue("@IssueDate", issueDate);
                        insertCommand.Parameters.AddWithValue("@DueDate", dueDate);
                        insertCommand.ExecuteNonQuery();
                    }

                    // 3. Decrement AvailableCopies in Books table
                    string updateBookQuery = "UPDATE Books SET AvailableCopies = AvailableCopies - 1 WHERE BookID = @BookID;";
                    using (SqlCommand updateCommand = new SqlCommand(updateBookQuery, connection, transaction))
                    {
                        updateCommand.Parameters.AddWithValue("@BookID", bookId);
                        updateCommand.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return true;
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Database error issuing book: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"An unexpected error occurred issuing book: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// Returns an issued book. Increments AvailableCopies.
        /// </summary>
        /// <returns>True if the book was returned successfully, false otherwise.</returns>
        public bool ReturnBook(int issueId, int bookId)
        {
            using (SqlConnection connection = new SqlConnection(_appConnectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Check if the book is already returned
                    string checkReturnQuery = "SELECT ReturnDate FROM IssuedBooks WHERE IssueID = @IssueID;";
                    using (SqlCommand checkCommand = new SqlCommand(checkReturnQuery, connection, transaction))
                    {
                        checkCommand.Parameters.AddWithValue("@IssueID", issueId);
                        object returnDate = checkCommand.ExecuteScalar();
                        if (returnDate != DBNull.Value)
                        {
                            transaction.Rollback();
                            MessageBox.Show("This book has already been returned.", "Return Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    // 1. Update ReturnDate in IssuedBooks table
                    string updateIssueQuery = "UPDATE IssuedBooks SET ReturnDate = GETDATE() WHERE IssueID = @IssueID;";
                    using (SqlCommand updateCommand = new SqlCommand(updateIssueQuery, connection, transaction))
                    {
                        updateCommand.Parameters.AddWithValue("@IssueID", issueId);
                        updateCommand.ExecuteNonQuery();
                    }

                    // 2. Increment AvailableCopies in Books table
                    string updateBookQuery = "UPDATE Books SET AvailableCopies = AvailableCopies + 1 WHERE BookID = @BookID;";
                    using (SqlCommand updateBookCommand = new SqlCommand(updateBookQuery, connection, transaction))
                    {
                        updateBookCommand.Parameters.AddWithValue("@BookID", bookId);
                        updateBookCommand.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return true;
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Database error returning book: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"An unexpected error occurred returning book: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
}