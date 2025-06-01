#Dagimawit Kelem DBU1501101
# LibraryApp
LibraryApp is a Windows Forms application developed in C# that functions as a comprehensive library management system. It allows for the management of books, borrowers, and book lending/returning operations, with user authentication handled via a login form.

* **Database Management:**
    * Automated database and table creation on application startup (if they don't exist).
    * Utilizes SQL Server for data storage.
* **User Authentication:**
    * Secure login form with input validation.
    * Authenticates users against credentials stored in the database.
* **Book Management:**
    * Add new books with details like title, author, ISBN, and number of copies.
    * Update existing book information.
    * Delete books from the library.
    * View all books in a structured table.
* **Borrower Management:**
    * Add new borrowers with details such as name, email, phone, and address.
    * Update borrower information.
    * Delete borrower records.
    * View all registered borrowers.
* **Book Issuing and Returning:**
    * Issue books to registered borrowers, recording issue and due dates.
    * Process the return of borrowed books, updating book availability.
    * View a list of all currently issued books.

* **Operating System:** Windows
* **.NET Framework:** .NET Framework 4.8
* **Database:** SQL Server (e.g., SQL Server Express, SQL Server Developer Edition). The application is configured to connect to `LEO\SQLEXPRESS01` with a database named `MyLibraryDB`. You may need to adjust the connection string in `LibraryDatabaseManager.cs` if your SQL Server instance name is different.

1.  **Clone the repository:**
    ```bash
    git clone [repository_url_here]
    cd LibraryApp
    ```
2.  **Open in Visual Studio:**
    Open the `LibraryApp.sln` file (or the project folder) in Visual Studio (2019 or later is recommended).
3.  **Restore NuGet Packages:**
    Visual Studio should automatically restore the necessary NuGet packages (e.g., `Microsoft.Data.SqlClient`, `System.Data.SqlClient`). If not, right-click on the solution in Solution Explorer and select "Restore NuGet Packages."
4.  **Build the project:**
    Build the solution (`Build > Build Solution` or `Ctrl+Shift+B`) to compile the application.

1.  **Ensure SQL Server is Running:** Make sure your SQL Server instance (e.g., `LEO\SQLEXPRESS01`) is running and accessible.
2.  **Run from Visual Studio:**
    Press `F5` or click the "Start" button in Visual Studio.
3.  **Run from Executable:**
    Navigate to the `bin/Debug` (or `bin/Release` after building in Release mode) folder within your project directory and run `LibraryApp.exe`.

The application will attempt to initialize the `MyLibraryDB` database and its tables on first run. If the database or tables do not exist, they will be created.

The application uses **SQL Server** for its backend. The connection string defined in `LibraryDatabaseManager.cs` targets:

* **Server Instance:** `LEO\SQLEXPRESS01`
* **Initial Catalog (Database Name):** `MyLibraryDB`
* **Authentication:** `Integrated Security=True` (Windows Authentication)

The database manager creates the following tables:
* `Users` (for login authentication)
* `Books`
* `Borrowers`
* `IssuedBooks` (to track loans)

**Note:** You might need to adjust the `_appConnectionString` and `_masterConnectionString` in `LibraryDatabaseManager.cs` to match your SQL Server instance name and authentication method if it differs from the default.

1.  **Login:**
    Upon launching the application, you will be presented with a login screen.
    * **Default Credentials (if any are set in `LibraryDatabaseManager.cs` initial data):** Check the `LibraryDatabaseManager.cs` file for `CreateTablesAndInsertInitialData()` method to see if any default users are added. Otherwise, you'll need to manually add a user to the `Users` table in your SQL Server database for initial login.
2.  **Main Application:**
    After successful login, the main application form will appear with tabs for managing Books, Borrowers, and Issued Books. Navigate between these tabs to perform the respective operations.

* `Program.cs`: The main entry point of the application. It initializes the database and launches the `LoginForm`.
* `LibraryDatabaseManager.cs`: Contains all logic for database interaction, including creating the database and tables, and performing CRUD operations on books, borrowers, and issues.
* `LoginForm.cs`: Handles the user interface and logic for user authentication.
* `LoginForm.Designer.cs`: Auto-generated code for the design of the login form.
* `MainForm.cs`: Implements the main application interface with tabs for managing different entities (Books, Borrowers, Issued Books). Contains the event handlers and logic for these operations.
* `MainForm.Designer.cs`: Auto-generated code for the design of the main application form.
* `App.config`: Configuration file for the .NET application, specifying the target framework and assembly binding redirects.
* `packages.config`: Lists the NuGet packages used by the project, such as `Microsoft.Data.SqlClient`.

The project relies on the following key NuGet packages, as specified in `packages.config`:

* `Microsoft.Data.SqlClient`
* `Microsoft.Bcl.AsyncInterfaces`
* `Microsoft.Bcl.Cryptography`
* `Microsoft.Data.SqlClient.SNI`
* `Microsoft.Extensions.Caching.Abstractions`
* `Microsoft.Extensions.Caching.Memory`
* `Microsoft.Extensions.DependencyInjection.Abstractions`
* `Microsoft.Extensions.Logging.Abstractions`
* `Microsoft.Extensions.Options`
* `Microsoft.Extensions.Primitives`
* `System.Buffers`
* `System.ClientModel`
* `System.Diagnostics.DiagnosticSource`
* `System.IdentityModel.Tokens.Jwt`
* `System.IO.FileSystem.AccessControl`
* `System.Memory`
* `System.Memory.Data`
* `System.Numerics.Vectors`
* `System.Runtime.CompilerServices.Unsafe`
* `System.Security.AccessControl`
* `System.Security.Cryptography.Pkcs`
* `System.Security.Cryptography.ProtectedData`
* `System.Security.Principal.Windows`
* `System.Text.Encodings.Web`
* `System.Text.Json`
* `System.Threading.Tasks.Extensions`

These dependencies will be automatically managed by NuGet when you build the project in Visual Studio.
