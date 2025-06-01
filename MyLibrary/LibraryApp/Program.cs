using System;
using System.Windows.Forms;

namespace LibraryApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // --- Database Initialization ---
            // Create an instance of our database manager
            LibraryDatabaseManager dbManager = new LibraryDatabaseManager();

            // Call the method to ensure the database and tables are created
            // This will create the DB/tables if they don't exist, and do nothing if they do.
            dbManager.InitializeDatabase();

            // --- Run the Login Form ---
            // After database initialization, run the login form
            Application.Run(new LoginForm());
        }
    }
}