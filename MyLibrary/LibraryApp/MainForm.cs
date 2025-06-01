using System;
using System.Data; // Required for DataTable
using System.Windows.Forms;

namespace LibraryApp
{
    public partial class MainForm : Form
    {
        private LibraryDatabaseManager _dbManager;
        private int _selectedBookId = -1; // To store the BookID of the selected row in Books DataGridView
        private int _selectedBorrowerId = -1; // To store the BorrowerID of the selected row in Borrowers DataGridView
        private int _selectedIssueId = -1; // To store the IssueID of the selected row in Issued Books DataGridView
        private int _selectedIssuedBookIdForReturn = -1; // To store the BookID associated with the selected issue for return

        public MainForm()
        {
            InitializeComponent();
            _dbManager = new LibraryDatabaseManager();
            // Set default font for consistency (optional)
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Load books into DataGridView when the form loads
            LoadBooksIntoDataGridView();
            ClearBookFields(); // Clear fields initially

            // Load borrowers when the form loads
            LoadBorrowersIntoDataGridView();
            ClearBorrowerFields(); // Clear borrower fields initially

            // Load issued books and populate combo boxes
            LoadIssuedBooksIntoDataGridView();
            PopulateBookComboBox();
            PopulateBorrowerComboBox();
            ClearIssuedBookFields(); // Clear issued book fields initially
        }

        // --- Books Management Methods ---
        private void LoadBooksIntoDataGridView()
        {
            try
            {
                DataTable booksTable = _dbManager.GetAllBooks();
                dgvBooks.DataSource = booksTable;

                // Optional: Hide UserID, PasswordHash if they were mistakenly fetched
                if (dgvBooks.Columns.Contains("UserID")) dgvBooks.Columns["UserID"].Visible = false;
                if (dgvBooks.Columns.Contains("PasswordHash")) dgvBooks.Columns["PasswordHash"].Visible = false;

                // Rename column headers for better display (optional)
                if (dgvBooks.Columns.Contains("BookID")) dgvBooks.Columns["BookID"].HeaderText = "ID";
                if (dgvBooks.Columns.Contains("Title")) dgvBooks.Columns["Title"].HeaderText = "Title";
                if (dgvBooks.Columns.Contains("Author")) dgvBooks.Columns["Author"].HeaderText = "Author";
                if (dgvBooks.Columns.Contains("PublicationYear")) dgvBooks.Columns["PublicationYear"].HeaderText = "Pub. Year";
                if (dgvBooks.Columns.Contains("ISBN")) dgvBooks.Columns["ISBN"].HeaderText = "ISBN";
                if (dgvBooks.Columns.Contains("TotalCopies")) dgvBooks.Columns["TotalCopies"].HeaderText = "Total";
                if (dgvBooks.Columns.Contains("AvailableCopies")) dgvBooks.Columns["AvailableCopies"].HeaderText = "Available";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading books: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearBookFields()
        {
            txtBookTitle.Clear();
            txtBookAuthor.Clear();
            numPublicationYear.Value = 2000; // Default or a sensible value
            txtBookISBN.Clear();
            numTotalCopies.Value = 1; // Default to 1 copy
            _selectedBookId = -1; // Reset selected book ID
            btnBookAdd.Enabled = true; // Enable Add button
            btnBookUpdate.Enabled = false; // Disable Update button
            btnBookDelete.Enabled = false; // Disable Delete button
        }

        private void dgvBooks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure a valid row is clicked (not header or empty space)
            if (e.RowIndex >= 0 && e.RowIndex < dgvBooks.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvBooks.Rows[e.RowIndex];

                // Check if BookID column exists and is not null
                if (selectedRow.Cells["BookID"].Value != null && int.TryParse(selectedRow.Cells["BookID"].Value.ToString(), out int bookId))
                {
                    _selectedBookId = bookId;

                    // Populate text boxes from selected row
                    txtBookTitle.Text = selectedRow.Cells["Title"].Value.ToString();
                    txtBookAuthor.Text = selectedRow.Cells["Author"].Value.ToString();
                    numPublicationYear.Value = Convert.ToInt32(selectedRow.Cells["PublicationYear"].Value);
                    txtBookISBN.Text = selectedRow.Cells["ISBN"].Value.ToString();
                    numTotalCopies.Value = Convert.ToInt32(selectedRow.Cells["TotalCopies"].Value);

                    btnBookAdd.Enabled = false; // Disable Add button when editing
                    btnBookUpdate.Enabled = true; // Enable Update button
                    btnBookDelete.Enabled = true; // Enable Delete button
                }
            }
        }

        private void btnBookAdd_Click(object sender, EventArgs e)
        {
            // Basic input validation
            if (string.IsNullOrWhiteSpace(txtBookTitle.Text) || string.IsNullOrWhiteSpace(txtBookAuthor.Text) || string.IsNullOrWhiteSpace(txtBookISBN.Text))
            {
                MessageBox.Show("Please fill in all required book details (Title, Author, ISBN).", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string title = txtBookTitle.Text.Trim();
                string author = txtBookAuthor.Text.Trim();
                int publicationYear = (int)numPublicationYear.Value;
                string isbn = txtBookISBN.Text.Trim();
                int totalCopies = (int)numTotalCopies.Value;

                bool success = _dbManager.AddBook(title, author, publicationYear, isbn, totalCopies);

                if (success)
                {
                    MessageBox.Show("Book added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBooksIntoDataGridView(); // Refresh DataGridView
                    ClearBookFields(); // Clear fields after adding
                    PopulateBookComboBox(); // Refresh book combo box
                }
                else
                {
                    // Error message would typically come from dbManager in a more robust app, or handled by the catch block
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBookUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedBookId == -1)
            {
                MessageBox.Show("Please select a book to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtBookTitle.Text) || string.IsNullOrWhiteSpace(txtBookAuthor.Text) || string.IsNullOrWhiteSpace(txtBookISBN.Text))
            {
                MessageBox.Show("Please fill in all required book details (Title, Author, ISBN).", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string title = txtBookTitle.Text.Trim();
                string author = txtBookAuthor.Text.Trim();
                int publicationYear = (int)numPublicationYear.Value;
                string isbn = txtBookISBN.Text.Trim();
                int totalCopies = (int)numTotalCopies.Value;

                bool success = _dbManager.UpdateBook(_selectedBookId, title, author, publicationYear, isbn, totalCopies);

                if (success)
                {
                    MessageBox.Show("Book updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBooksIntoDataGridView(); // Refresh DataGridView
                    ClearBookFields(); // Clear fields after updating
                    PopulateBookComboBox(); // Refresh book combo box
                }
                else
                {
                    // Error message would typically come from dbManager in a more robust app, or handled by the catch block
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBookDelete_Click(object sender, EventArgs e)
        {
            if (_selectedBookId == -1)
            {
                MessageBox.Show("Please select a book to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this book?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    bool success = _dbManager.DeleteBook(_selectedBookId);

                    if (success)
                    {
                        MessageBox.Show("Book deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBooksIntoDataGridView(); // Refresh DataGridView
                        ClearBookFields(); // Clear fields after deleting
                        PopulateBookComboBox(); // Refresh book combo box
                    }
                    else
                    {
                        // Error message would typically come from dbManager in a more robust app, or handled by the catch block
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while deleting the book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBookClear_Click(object sender, EventArgs e)
        {
            ClearBookFields();
        }

        // --- Borrowers Management Methods ---

        private void LoadBorrowersIntoDataGridView()
        {
            try
            {
                DataTable borrowersTable = _dbManager.GetAllBorrowers();
                dgvBorrowers.DataSource = borrowersTable;

                // Rename column headers for better display (optional)
                if (dgvBorrowers.Columns.Contains("BorrowerID")) dgvBorrowers.Columns["BorrowerID"].HeaderText = "ID";
                if (dgvBorrowers.Columns.Contains("Name")) dgvBorrowers.Columns["Name"].HeaderText = "Name";
                if (dgvBorrowers.Columns.Contains("Email")) dgvBorrowers.Columns["Email"].HeaderText = "Email";
                if (dgvBorrowers.Columns.Contains("Phone")) dgvBorrowers.Columns["Phone"].HeaderText = "Phone";
                if (dgvBorrowers.Columns.Contains("Address")) dgvBorrowers.Columns["Address"].HeaderText = "Address";
                if (dgvBorrowers.Columns.Contains("RegistrationDate")) dgvBorrowers.Columns["RegistrationDate"].HeaderText = "Reg. Date";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading borrowers: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearBorrowerFields()
        {
            txtBorrowerName.Clear();
            txtBorrowerEmail.Clear();
            txtBorrowerPhone.Clear();
            txtBorrowerAddress.Clear();
            _selectedBorrowerId = -1; // Reset selected borrower ID
            btnBorrowerAdd.Enabled = true; // Enable Add button
            btnBorrowerUpdate.Enabled = false; // Disable Update button
            btnBorrowerDelete.Enabled = false; // Disable Delete button
        }

        private void dgvBorrowers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvBorrowers.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvBorrowers.Rows[e.RowIndex];

                if (selectedRow.Cells["BorrowerID"].Value != null && int.TryParse(selectedRow.Cells["BorrowerID"].Value.ToString(), out int borrowerId))
                {
                    _selectedBorrowerId = borrowerId;

                    txtBorrowerName.Text = selectedRow.Cells["Name"].Value.ToString();
                    txtBorrowerEmail.Text = selectedRow.Cells["Email"].Value.ToString();
                    txtBorrowerPhone.Text = selectedRow.Cells["Phone"].Value.ToString();
                    txtBorrowerAddress.Text = selectedRow.Cells["Address"].Value.ToString();

                    btnBorrowerAdd.Enabled = false;
                    btnBorrowerUpdate.Enabled = true;
                    btnBorrowerDelete.Enabled = true;
                }
            }
        }

        private void btnBorrowerAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBorrowerName.Text) || string.IsNullOrWhiteSpace(txtBorrowerEmail.Text))
            {
                MessageBox.Show("Please fill in borrower's Name and Email.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string name = txtBorrowerName.Text.Trim();
                string email = txtBorrowerEmail.Text.Trim();
                string phone = txtBorrowerPhone.Text.Trim();
                string address = txtBorrowerAddress.Text.Trim();

                bool success = _dbManager.AddBorrower(name, email, phone, address);

                if (success)
                {
                    MessageBox.Show("Borrower added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBorrowersIntoDataGridView();
                    ClearBorrowerFields();
                    PopulateBorrowerComboBox(); // Refresh borrower combo box
                }
                else
                {
                    // Error message would typically come from dbManager
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the borrower: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBorrowerUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedBorrowerId == -1)
            {
                MessageBox.Show("Please select a borrower to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtBorrowerName.Text) || string.IsNullOrWhiteSpace(txtBorrowerEmail.Text))
            {
                MessageBox.Show("Please fill in borrower's Name and Email.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string name = txtBorrowerName.Text.Trim();
                string email = txtBorrowerEmail.Text.Trim();
                string phone = txtBorrowerPhone.Text.Trim();
                string address = txtBorrowerAddress.Text.Trim();

                bool success = _dbManager.UpdateBorrower(_selectedBorrowerId, name, email, phone, address);

                if (success)
                {
                    MessageBox.Show("Borrower updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBorrowersIntoDataGridView();
                    ClearBorrowerFields();
                    PopulateBorrowerComboBox(); // Refresh borrower combo box
                }
                else
                {
                    // Error message would typically come from dbManager
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the borrower: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBorrowerDelete_Click(object sender, EventArgs e)
        {
            if (_selectedBorrowerId == -1)
            {
                MessageBox.Show("Please select a borrower to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this borrower?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    bool success = _dbManager.DeleteBorrower(_selectedBorrowerId);

                    if (success)
                    {
                        MessageBox.Show("Borrower deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBorrowersIntoDataGridView();
                        ClearBorrowerFields();
                        PopulateBorrowerComboBox(); // Refresh borrower combo box
                        LoadIssuedBooksIntoDataGridView(); // Refresh issued books as well
                    }
                    else
                    {
                        // Error message would typically come from dbManager
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while deleting the borrower: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBorrowerClear_Click(object sender, EventArgs e)
        {
            ClearBorrowerFields();
        }

        // --- Issued Books Management Methods ---

        private void LoadIssuedBooksIntoDataGridView()
        {
            try
            {
                // This method will be added to LibraryDatabaseManager.cs in the next step
                DataTable issuedBooksTable = _dbManager.GetAllIssuedBooks();
                dgvIssuedBooks.DataSource = issuedBooksTable;

                // Rename column headers for better display
                if (dgvIssuedBooks.Columns.Contains("IssueID")) dgvIssuedBooks.Columns["IssueID"].HeaderText = "Issue ID";
                if (dgvIssuedBooks.Columns.Contains("BookTitle")) dgvIssuedBooks.Columns["BookTitle"].HeaderText = "Book Title";
                if (dgvIssuedBooks.Columns.Contains("BorrowerName")) dgvIssuedBooks.Columns["BorrowerName"].HeaderText = "Borrower Name";
                if (dgvIssuedBooks.Columns.Contains("IssueDate")) dgvIssuedBooks.Columns["IssueDate"].HeaderText = "Issue Date";
                if (dgvIssuedBooks.Columns.Contains("DueDate")) dgvIssuedBooks.Columns["DueDate"].HeaderText = "Due Date";
                if (dgvIssuedBooks.Columns.Contains("ReturnDate")) dgvIssuedBooks.Columns["ReturnDate"].HeaderText = "Return Date";
                if (dgvIssuedBooks.Columns.Contains("BookID")) dgvIssuedBooks.Columns["BookID"].Visible = false; // Hide FK
                if (dgvIssuedBooks.Columns.Contains("BorrowerID")) dgvIssuedBooks.Columns["BorrowerID"].Visible = false; // Hide FK
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading issued books: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateBookComboBox()
        {
            try
            {
                DataTable books = _dbManager.GetAllBooksForComboBox(); // Get all books (Title and ID)
                cmbBooks.DataSource = books;
                cmbBooks.DisplayMember = "Title"; // What the user sees
                cmbBooks.ValueMember = "BookID"; // The actual value (BookID)
                cmbBooks.SelectedIndex = -1; // No item selected initially
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error populating book combo box: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateBorrowerComboBox()
        {
            try
            {
                DataTable borrowers = _dbManager.GetAllBorrowersForComboBox(); // Get all borrowers (Name and ID)
                cmbBorrowers.DataSource = borrowers;
                cmbBorrowers.DisplayMember = "Name"; // What the user sees
                cmbBorrowers.ValueMember = "BorrowerID"; // The actual value (BorrowerID)
                cmbBorrowers.SelectedIndex = -1; // No item selected initially
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error populating borrower combo box: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearIssuedBookFields()
        {
            cmbBooks.SelectedIndex = -1;
            cmbBorrowers.SelectedIndex = -1;
            dtpIssueDate.Value = DateTime.Today; // Default to today
            dtpDueDate.Value = DateTime.Today.AddDays(14); // Default to 2 weeks from today
            _selectedIssueId = -1;
            _selectedIssuedBookIdForReturn = -1;
            btnIssueBook.Enabled = true;
            btnReturnBook.Enabled = false;
        }

        private void dgvIssuedBooks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvIssuedBooks.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvIssuedBooks.Rows[e.RowIndex];

                if (selectedRow.Cells["IssueID"].Value != null && int.TryParse(selectedRow.Cells["IssueID"].Value.ToString(), out int issueId))
                {
                    _selectedIssueId = issueId;

                    // Retrieve book and borrower IDs from the hidden columns
                    if (selectedRow.Cells["BookID"].Value != null && int.TryParse(selectedRow.Cells["BookID"].Value.ToString(), out int bookId))
                    {
                        cmbBooks.SelectedValue = bookId;
                        _selectedIssuedBookIdForReturn = bookId; // Store this for returning
                    }
                    if (selectedRow.Cells["BorrowerID"].Value != null && int.TryParse(selectedRow.Cells["BorrowerID"].Value.ToString(), out int borrowerId))
                    {
                        cmbBorrowers.SelectedValue = borrowerId;
                    }

                    dtpIssueDate.Value = Convert.ToDateTime(selectedRow.Cells["IssueDate"].Value);
                    dtpDueDate.Value = Convert.ToDateTime(selectedRow.Cells["DueDate"].Value);

                    // If the book has already been returned, disable return button
                    if (selectedRow.Cells["ReturnDate"].Value != DBNull.Value)
                    {
                        btnReturnBook.Enabled = false;
                        btnIssueBook.Enabled = true; // Can issue new book
                    }
                    else
                    {
                        btnReturnBook.Enabled = true;
                        btnIssueBook.Enabled = false; // Cannot issue while an item is selected for return
                    }
                }
            }
        }

        private void btnIssueBook_Click(object sender, EventArgs e)
        {
            if (cmbBooks.SelectedValue == null || cmbBorrowers.SelectedValue == null)
            {
                MessageBox.Show("Please select both a Book and a Borrower.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int bookId = (int)cmbBooks.SelectedValue;
            int borrowerId = (int)cmbBorrowers.SelectedValue;
            DateTime issueDate = dtpIssueDate.Value.Date;
            DateTime dueDate = dtpDueDate.Value.Date;

            if (dueDate < issueDate)
            {
                MessageBox.Show("Due Date cannot be before Issue Date.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // This method will be added to LibraryDatabaseManager.cs in the next step
                bool success = _dbManager.IssueBook(bookId, borrowerId, issueDate, dueDate);

                if (success)
                {
                    MessageBox.Show("Book issued successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadIssuedBooksIntoDataGridView(); // Refresh issued books
                    LoadBooksIntoDataGridView(); // Refresh books (available copies changed)
                    ClearIssuedBookFields(); // Clear fields after issuing
                }
                else
                {
                    // Error message from dbManager would provide more details
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while issuing the book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReturnBook_Click(object sender, EventArgs e)
        {
            if (_selectedIssueId == -1)
            {
                MessageBox.Show("Please select an issued book to return.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to return this book?", "Confirm Return", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    // This method will be added to LibraryDatabaseManager.cs in the next step
                    bool success = _dbManager.ReturnBook(_selectedIssueId, _selectedIssuedBookIdForReturn);

                    if (success)
                    {
                        MessageBox.Show("Book returned successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadIssuedBooksIntoDataGridView(); // Refresh issued books
                        LoadBooksIntoDataGridView(); // Refresh books (available copies changed)
                        ClearIssuedBookFields(); // Clear fields after returning
                    }
                    else
                    {
                        // Error message from dbManager would provide more details
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while returning the book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        // --- Tab Control Event ---
        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Refresh data when switching tabs
            if (tabControlMain.SelectedTab == tabPageBooks)
            {
                LoadBooksIntoDataGridView();
                ClearBookFields();
            }
            else if (tabControlMain.SelectedTab == tabPageBorrowers)
            {
                LoadBorrowersIntoDataGridView();
                ClearBorrowerFields();
            }
            else if (tabControlMain.SelectedTab == tabPageIssuedBooks)
            {
                LoadIssuedBooksIntoDataGridView();
                PopulateBookComboBox();
                PopulateBorrowerComboBox();
                ClearIssuedBookFields();
            }
        }
    }
}