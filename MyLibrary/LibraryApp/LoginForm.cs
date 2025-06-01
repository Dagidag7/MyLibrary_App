using System;
using System.Windows.Forms;

namespace LibraryApp
{
    public partial class LoginForm : Form
    {
        // Create an instance of LibraryDatabaseManager
        private LibraryDatabaseManager _dbManager;

        public LoginForm()
        {
            InitializeComponent();
            txtPassword.UseSystemPasswordChar = true; // Ensure password characters are masked

            // Initialize the database manager
            _dbManager = new LibraryDatabaseManager();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Set initial focus to the username field when the form loads
            txtUsername.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim(); // Get and trim username
            string password = txtPassword.Text.Trim(); // Get and trim password

            // --- Input Validation ---
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username and password cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Stop execution if validation fails
            }

            // --- Authenticate using LibraryDatabaseManager ---
            bool isAuthenticated = _dbManager.AuthenticateUser(username, password);

            if (isAuthenticated)
            {
                MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Create an instance of the MainForm
                MainForm mainForm = new MainForm();

                // Hide the Login Form (this form)
                this.Hide();

                // Show the Main Form
                mainForm.Show();

                // Optional: This line ensures that if the MainForm is closed, the LoginForm also closes,
                // and thus the entire application exits cleanly.
                mainForm.FormClosed += (s, args) => this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear(); // Clear password field for security
                txtUsername.Focus(); // Set focus back to username field
            }
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPassword.Focus();
                e.Handled = true;
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin_Click(sender, e);
                e.Handled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}