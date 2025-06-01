namespace LibraryApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageBooks = new System.Windows.Forms.TabPage();
            this.btnBookClear = new System.Windows.Forms.Button();
            this.btnBookDelete = new System.Windows.Forms.Button();
            this.btnBookUpdate = new System.Windows.Forms.Button();
            this.btnBookAdd = new System.Windows.Forms.Button();
            this.groupBoxBookDetails = new System.Windows.Forms.GroupBox();
            this.numTotalCopies = new System.Windows.Forms.NumericUpDown();
            this.numPublicationYear = new System.Windows.Forms.NumericUpDown();
            this.txtBookISBN = new System.Windows.Forms.TextBox();
            this.txtBookAuthor = new System.Windows.Forms.TextBox();
            this.txtBookTitle = new System.Windows.Forms.TextBox();
            this.lblTotalCopies = new System.Windows.Forms.Label();
            this.lblBookISBN = new System.Windows.Forms.Label();
            this.lblPublicationYear = new System.Windows.Forms.Label();
            this.lblBookAuthor = new System.Windows.Forms.Label();
            this.lblBookTitle = new System.Windows.Forms.Label();
            this.dgvBooks = new System.Windows.Forms.DataGridView();
            this.tabPageBorrowers = new System.Windows.Forms.TabPage();
            this.btnBorrowerClear = new System.Windows.Forms.Button();
            this.btnBorrowerDelete = new System.Windows.Forms.Button();
            this.btnBorrowerUpdate = new System.Windows.Forms.Button();
            this.btnBorrowerAdd = new System.Windows.Forms.Button();
            this.groupBoxBorrowerDetails = new System.Windows.Forms.GroupBox();
            this.txtBorrowerAddress = new System.Windows.Forms.TextBox();
            this.txtBorrowerPhone = new System.Windows.Forms.TextBox();
            this.txtBorrowerEmail = new System.Windows.Forms.TextBox();
            this.txtBorrowerName = new System.Windows.Forms.TextBox();
            this.lblBorrowerAddress = new System.Windows.Forms.Label();
            this.lblBorrowerPhone = new System.Windows.Forms.Label();
            this.lblBorrowerEmail = new System.Windows.Forms.Label();
            this.lblBorrowerName = new System.Windows.Forms.Label();
            this.dgvBorrowers = new System.Windows.Forms.DataGridView();
            this.tabPageIssuedBooks = new System.Windows.Forms.TabPage();
            this.btnReturnBook = new System.Windows.Forms.Button();
            this.btnIssueBook = new System.Windows.Forms.Button();
            this.groupBoxIssueDetails = new System.Windows.Forms.GroupBox();
            this.dtpIssueDate = new System.Windows.Forms.DateTimePicker();
            this.dtpDueDate = new System.Windows.Forms.DateTimePicker();
            this.cmbBorrowers = new System.Windows.Forms.ComboBox();
            this.cmbBooks = new System.Windows.Forms.ComboBox();
            this.lblDueDate = new System.Windows.Forms.Label();
            this.lblIssueDate = new System.Windows.Forms.Label();
            this.lblIssuedBorrower = new System.Windows.Forms.Label();
            this.lblIssuedBook = new System.Windows.Forms.Label();
            this.dgvIssuedBooks = new System.Windows.Forms.DataGridView();
            this.tabControlMain.SuspendLayout();
            this.tabPageBooks.SuspendLayout();
            this.groupBoxBookDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalCopies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPublicationYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).BeginInit();
            this.tabPageBorrowers.SuspendLayout();
            this.groupBoxBorrowerDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowers)).BeginInit();
            this.tabPageIssuedBooks.SuspendLayout();
            this.groupBoxIssueDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIssuedBooks)).BeginInit();
            this.SuspendLayout();
            //
            // tabControlMain
            //
            this.tabControlMain.Controls.Add(this.tabPageBooks);
            this.tabControlMain.Controls.Add(this.tabPageBorrowers);
            this.tabControlMain.Controls.Add(this.tabPageIssuedBooks);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(984, 561);
            this.tabControlMain.TabIndex = 0;
            this.tabControlMain.SelectedIndexChanged += new System.EventHandler(this.tabControlMain_SelectedIndexChanged);
            //
            // tabPageBooks
            //
            this.tabPageBooks.Controls.Add(this.btnBookClear);
            this.tabPageBooks.Controls.Add(this.btnBookDelete);
            this.tabPageBooks.Controls.Add(this.btnBookUpdate);
            this.tabPageBooks.Controls.Add(this.btnBookAdd);
            this.tabPageBooks.Controls.Add(this.groupBoxBookDetails);
            this.tabPageBooks.Controls.Add(this.dgvBooks);
            this.tabPageBooks.Location = new System.Drawing.Point(4, 22);
            this.tabPageBooks.Name = "tabPageBooks";
            this.tabPageBooks.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBooks.Size = new System.Drawing.Size(976, 535);
            this.tabPageBooks.TabIndex = 0;
            this.tabPageBooks.Text = "Books Management";
            this.tabPageBooks.UseVisualStyleBackColor = true;
            //
            // btnBookClear
            //
            this.btnBookClear.Location = new System.Drawing.Point(232, 290);
            this.btnBookClear.Name = "btnBookClear";
            this.btnBookClear.Size = new System.Drawing.Size(75, 30);
            this.btnBookClear.TabIndex = 5;
            this.btnBookClear.Text = "Clear";
            this.btnBookClear.UseVisualStyleBackColor = true;
            this.btnBookClear.Click += new System.EventHandler(this.btnBookClear_Click);
            //
            // btnBookDelete
            //
            this.btnBookDelete.Location = new System.Drawing.Point(151, 290);
            this.btnBookDelete.Name = "btnBookDelete";
            this.btnBookDelete.Size = new System.Drawing.Size(75, 30);
            this.btnBookDelete.TabIndex = 4;
            this.btnBookDelete.Text = "Delete";
            this.btnBookDelete.UseVisualStyleBackColor = true;
            this.btnBookDelete.Click += new System.EventHandler(this.btnBookDelete_Click);
            //
            // btnBookUpdate
            //
            this.btnBookUpdate.Location = new System.Drawing.Point(70, 290);
            this.btnBookUpdate.Name = "btnBookUpdate";
            this.btnBookUpdate.Size = new System.Drawing.Size(75, 30);
            this.btnBookUpdate.TabIndex = 3;
            this.btnBookUpdate.Text = "Update";
            this.btnBookUpdate.UseVisualStyleBackColor = true;
            this.btnBookUpdate.Click += new System.EventHandler(this.btnBookUpdate_Click);
            //
            // btnBookAdd
            //
            this.btnBookAdd.Location = new System.Drawing.Point(20, 290);
            this.btnBookAdd.Name = "btnBookAdd";
            this.btnBookAdd.Size = new System.Drawing.Size(75, 30);
            this.btnBookAdd.TabIndex = 2;
            this.btnBookAdd.Text = "Add";
            this.btnBookAdd.UseVisualStyleBackColor = true;
            this.btnBookAdd.Click += new System.EventHandler(this.btnBookAdd_Click);
            //
            // groupBoxBookDetails
            //
            this.groupBoxBookDetails.Controls.Add(this.numTotalCopies);
            this.groupBoxBookDetails.Controls.Add(this.numPublicationYear);
            this.groupBoxBookDetails.Controls.Add(this.txtBookISBN);
            this.groupBoxBookDetails.Controls.Add(this.txtBookAuthor);
            this.groupBoxBookDetails.Controls.Add(this.txtBookTitle);
            this.groupBoxBookDetails.Controls.Add(this.lblTotalCopies);
            this.groupBoxBookDetails.Controls.Add(this.lblBookISBN);
            this.groupBoxBookDetails.Controls.Add(this.lblPublicationYear);
            this.groupBoxBookDetails.Controls.Add(this.lblBookAuthor);
            this.groupBoxBookDetails.Controls.Add(this.lblBookTitle);
            this.groupBoxBookDetails.Location = new System.Drawing.Point(8, 15);
            this.groupBoxBookDetails.Name = "groupBoxBookDetails";
            this.groupBoxBookDetails.Size = new System.Drawing.Size(320, 250);
            this.groupBoxBookDetails.TabIndex = 1;
            this.groupBoxBookDetails.TabStop = false;
            this.groupBoxBookDetails.Text = "Book Details";
            //
            // numTotalCopies
            //
            this.numTotalCopies.Location = new System.Drawing.Point(130, 190);
            this.numTotalCopies.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTotalCopies.Name = "numTotalCopies";
            this.numTotalCopies.Size = new System.Drawing.Size(170, 20);
            this.numTotalCopies.TabIndex = 9;
            this.numTotalCopies.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            //
            // numPublicationYear
            //
            this.numPublicationYear.Location = new System.Drawing.Point(130, 120);
            this.numPublicationYear.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.numPublicationYear.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numPublicationYear.Name = "numPublicationYear";
            this.numPublicationYear.Size = new System.Drawing.Size(170, 20);
            this.numPublicationYear.TabIndex = 7;
            this.numPublicationYear.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            //
            // txtBookISBN
            //
            this.txtBookISBN.Location = new System.Drawing.Point(130, 155);
            this.txtBookISBN.Name = "txtBookISBN";
            this.txtBookISBN.Size = new System.Drawing.Size(170, 20);
            this.txtBookISBN.TabIndex = 8;
            //
            // txtBookAuthor
            //
            this.txtBookAuthor.Location = new System.Drawing.Point(130, 85);
            this.txtBookAuthor.Name = "txtBookAuthor";
            this.txtBookAuthor.Size = new System.Drawing.Size(170, 20);
            this.txtBookAuthor.TabIndex = 6;
            //
            // txtBookTitle
            //
            this.txtBookTitle.Location = new System.Drawing.Point(130, 50);
            this.txtBookTitle.Name = "txtBookTitle";
            this.txtBookTitle.Size = new System.Drawing.Size(170, 20);
            this.txtBookTitle.TabIndex = 5;
            //
            // lblTotalCopies
            //
            this.lblTotalCopies.AutoSize = true;
            this.lblTotalCopies.Location = new System.Drawing.Point(20, 190);
            this.lblTotalCopies.Name = "lblTotalCopies";
            this.lblTotalCopies.Size = new System.Drawing.Size(71, 13);
            this.lblTotalCopies.TabIndex = 4;
            this.lblTotalCopies.Text = "Total Copies:";
            //
            // lblBookISBN
            //
            this.lblBookISBN.AutoSize = true;
            this.lblBookISBN.Location = new System.Drawing.Point(20, 155);
            this.lblBookISBN.Name = "lblBookISBN";
            this.lblBookISBN.Size = new System.Drawing.Size(35, 13);
            this.lblBookISBN.TabIndex = 3;
            this.lblBookISBN.Text = "ISBN:";
            //
            // lblPublicationYear
            //
            this.lblPublicationYear.AutoSize = true;
            this.lblPublicationYear.Location = new System.Drawing.Point(20, 120);
            this.lblPublicationYear.Name = "lblPublicationYear";
            this.lblPublicationYear.Size = new System.Drawing.Size(89, 13);
            this.lblPublicationYear.TabIndex = 2;
            this.lblPublicationYear.Text = "Publication Year:";
            //
            // lblBookAuthor
            //
            this.lblBookAuthor.AutoSize = true;
            this.lblBookAuthor.Location = new System.Drawing.Point(20, 85);
            this.lblBookAuthor.Name = "lblBookAuthor";
            this.lblBookAuthor.Size = new System.Drawing.Size(41, 13);
            this.lblBookAuthor.TabIndex = 1;
            this.lblBookAuthor.Text = "Author:";
            //
            // lblBookTitle
            //
            this.lblBookTitle.AutoSize = true;
            this.lblBookTitle.Location = new System.Drawing.Point(20, 50);
            this.lblBookTitle.Name = "lblBookTitle";
            this.lblBookTitle.Size = new System.Drawing.Size(30, 13);
            this.lblBookTitle.TabIndex = 0;
            this.lblBookTitle.Text = "Title:";
            //
            // dgvBooks
            //
            this.dgvBooks.AllowUserToAddRows = false;
            this.dgvBooks.AllowUserToDeleteRows = false;
            this.dgvBooks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBooks.Location = new System.Drawing.Point(340, 15);
            this.dgvBooks.Name = "dgvBooks";
            this.dgvBooks.ReadOnly = true;
            this.dgvBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBooks.Size = new System.Drawing.Size(620, 500);
            this.dgvBooks.TabIndex = 0;
            this.dgvBooks.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBooks_CellClick);
            //
            // tabPageBorrowers
            //
            this.tabPageBorrowers.Controls.Add(this.btnBorrowerClear);
            this.tabPageBorrowers.Controls.Add(this.btnBorrowerDelete);
            this.tabPageBorrowers.Controls.Add(this.btnBorrowerUpdate);
            this.tabPageBorrowers.Controls.Add(this.btnBorrowerAdd);
            this.tabPageBorrowers.Controls.Add(this.groupBoxBorrowerDetails);
            this.tabPageBorrowers.Controls.Add(this.dgvBorrowers);
            this.tabPageBorrowers.Location = new System.Drawing.Point(4, 22);
            this.tabPageBorrowers.Name = "tabPageBorrowers";
            this.tabPageBorrowers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBorrowers.Size = new System.Drawing.Size(976, 535);
            this.tabPageBorrowers.TabIndex = 1;
            this.tabPageBorrowers.Text = "Borrowers Management";
            this.tabPageBorrowers.UseVisualStyleBackColor = true;
            //
            // btnBorrowerClear
            //
            this.btnBorrowerClear.Location = new System.Drawing.Point(232, 290);
            this.btnBorrowerClear.Name = "btnBorrowerClear";
            this.btnBorrowerClear.Size = new System.Drawing.Size(75, 30);
            this.btnBorrowerClear.TabIndex = 11;
            this.btnBorrowerClear.Text = "Clear";
            this.btnBorrowerClear.UseVisualStyleBackColor = true;
            this.btnBorrowerClear.Click += new System.EventHandler(this.btnBorrowerClear_Click);
            //
            // btnBorrowerDelete
            //
            this.btnBorrowerDelete.Location = new System.Drawing.Point(151, 290);
            this.btnBorrowerDelete.Name = "btnBorrowerDelete";
            this.btnBorrowerDelete.Size = new System.Drawing.Size(75, 30);
            this.btnBorrowerDelete.TabIndex = 10;
            this.btnBorrowerDelete.Text = "Delete";
            this.btnBorrowerDelete.UseVisualStyleBackColor = true;
            this.btnBorrowerDelete.Click += new System.EventHandler(this.btnBorrowerDelete_Click);
            //
            // btnBorrowerUpdate
            //
            this.btnBorrowerUpdate.Location = new System.Drawing.Point(70, 290);
            this.btnBorrowerUpdate.Name = "btnBorrowerUpdate";
            this.btnBorrowerUpdate.Size = new System.Drawing.Size(75, 30);
            this.btnBorrowerUpdate.TabIndex = 9;
            this.btnBorrowerUpdate.Text = "Update";
            this.btnBorrowerUpdate.UseVisualStyleBackColor = true;
            this.btnBorrowerUpdate.Click += new System.EventHandler(this.btnBorrowerUpdate_Click);
            //
            // btnBorrowerAdd
            //
            this.btnBorrowerAdd.Location = new System.Drawing.Point(20, 290);
            this.btnBorrowerAdd.Name = "btnBorrowerAdd";
            this.btnBorrowerAdd.Size = new System.Drawing.Size(75, 30);
            this.btnBorrowerAdd.TabIndex = 8;
            this.btnBorrowerAdd.Text = "Add";
            this.btnBorrowerAdd.UseVisualStyleBackColor = true;
            this.btnBorrowerAdd.Click += new System.EventHandler(this.btnBorrowerAdd_Click);
            //
            // groupBoxBorrowerDetails
            //
            this.groupBoxBorrowerDetails.Controls.Add(this.txtBorrowerAddress);
            this.groupBoxBorrowerDetails.Controls.Add(this.txtBorrowerPhone);
            this.groupBoxBorrowerDetails.Controls.Add(this.txtBorrowerEmail);
            this.groupBoxBorrowerDetails.Controls.Add(this.txtBorrowerName);
            this.groupBoxBorrowerDetails.Controls.Add(this.lblBorrowerAddress);
            this.groupBoxBorrowerDetails.Controls.Add(this.lblBorrowerPhone);
            this.groupBoxBorrowerDetails.Controls.Add(this.lblBorrowerEmail);
            this.groupBoxBorrowerDetails.Controls.Add(this.lblBorrowerName);
            this.groupBoxBorrowerDetails.Location = new System.Drawing.Point(8, 15);
            this.groupBoxBorrowerDetails.Name = "groupBoxBorrowerDetails";
            this.groupBoxBorrowerDetails.Size = new System.Drawing.Size(320, 250);
            this.groupBoxBorrowerDetails.TabIndex = 7;
            this.groupBoxBorrowerDetails.TabStop = false;
            this.groupBoxBorrowerDetails.Text = "Borrower Details";
            //
            // txtBorrowerAddress
            //
            this.txtBorrowerAddress.Location = new System.Drawing.Point(130, 155);
            this.txtBorrowerAddress.Name = "txtBorrowerAddress";
            this.txtBorrowerAddress.Size = new System.Drawing.Size(170, 20);
            this.txtBorrowerAddress.TabIndex = 7;
            //
            // txtBorrowerPhone
            //
            this.txtBorrowerPhone.Location = new System.Drawing.Point(130, 120);
            this.txtBorrowerPhone.Name = "txtBorrowerPhone";
            this.txtBorrowerPhone.Size = new System.Drawing.Size(170, 20);
            this.txtBorrowerPhone.TabIndex = 6;
            //
            // txtBorrowerEmail
            //
            this.txtBorrowerEmail.Location = new System.Drawing.Point(130, 85);
            this.txtBorrowerEmail.Name = "txtBorrowerEmail";
            this.txtBorrowerEmail.Size = new System.Drawing.Size(170, 20);
            this.txtBorrowerEmail.TabIndex = 5;
            //
            // txtBorrowerName
            //
            this.txtBorrowerName.Location = new System.Drawing.Point(130, 50);
            this.txtBorrowerName.Name = "txtBorrowerName";
            this.txtBorrowerName.Size = new System.Drawing.Size(170, 20);
            this.txtBorrowerName.TabIndex = 4;
            //
            // lblBorrowerAddress
            //
            this.lblBorrowerAddress.AutoSize = true;
            this.lblBorrowerAddress.Location = new System.Drawing.Point(20, 155);
            this.lblBorrowerAddress.Name = "lblBorrowerAddress";
            this.lblBorrowerAddress.Size = new System.Drawing.Size(48, 13);
            this.lblBorrowerAddress.TabIndex = 3;
            this.lblBorrowerAddress.Text = "Address:";
            //
            // lblBorrowerPhone
            //
            this.lblBorrowerPhone.AutoSize = true;
            this.lblBorrowerPhone.Location = new System.Drawing.Point(20, 120);
            this.lblBorrowerPhone.Name = "lblBorrowerPhone";
            this.lblBorrowerPhone.Size = new System.Drawing.Size(41, 13);
            this.lblBorrowerPhone.TabIndex = 2;
            this.lblBorrowerPhone.Text = "Phone:";
            //
            // lblBorrowerEmail
            //
            this.lblBorrowerEmail.AutoSize = true;
            this.lblBorrowerEmail.Location = new System.Drawing.Point(20, 85);
            this.lblBorrowerEmail.Name = "lblBorrowerEmail";
            this.lblBorrowerEmail.Size = new System.Drawing.Size(35, 13);
            this.lblBorrowerEmail.TabIndex = 1;
            this.lblBorrowerEmail.Text = "Email:";
            //
            // lblBorrowerName
            //
            this.lblBorrowerName.AutoSize = true;
            this.lblBorrowerName.Location = new System.Drawing.Point(20, 50);
            this.lblBorrowerName.Name = "lblBorrowerName";
            this.lblBorrowerName.Size = new System.Drawing.Size(38, 13);
            this.lblBorrowerName.TabIndex = 0;
            this.lblBorrowerName.Text = "Name:";
            //
            // dgvBorrowers
            //
            this.dgvBorrowers.AllowUserToAddRows = false;
            this.dgvBorrowers.AllowUserToDeleteRows = false;
            this.dgvBorrowers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBorrowers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBorrowers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBorrowers.Location = new System.Drawing.Point(340, 15);
            this.dgvBorrowers.Name = "dgvBorrowers";
            this.dgvBorrowers.ReadOnly = true;
            this.dgvBorrowers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBorrowers.Size = new System.Drawing.Size(620, 500);
            this.dgvBorrowers.TabIndex = 6;
            this.dgvBorrowers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBorrowers_CellClick);
            //
            // tabPageIssuedBooks
            //
            this.tabPageIssuedBooks.Controls.Add(this.btnReturnBook);
            this.tabPageIssuedBooks.Controls.Add(this.btnIssueBook);
            this.tabPageIssuedBooks.Controls.Add(this.groupBoxIssueDetails);
            this.tabPageIssuedBooks.Controls.Add(this.dgvIssuedBooks);
            this.tabPageIssuedBooks.Location = new System.Drawing.Point(4, 22);
            this.tabPageIssuedBooks.Name = "tabPageIssuedBooks";
            this.tabPageIssuedBooks.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageIssuedBooks.Size = new System.Drawing.Size(976, 535);
            this.tabPageIssuedBooks.TabIndex = 2;
            this.tabPageIssuedBooks.Text = "Issued Books Management";
            this.tabPageIssuedBooks.UseVisualStyleBackColor = true;
            //
            // btnReturnBook
            //
            this.btnReturnBook.Location = new System.Drawing.Point(170, 240);
            this.btnReturnBook.Name = "btnReturnBook";
            this.btnReturnBook.Size = new System.Drawing.Size(120, 30);
            this.btnReturnBook.TabIndex = 3;
            this.btnReturnBook.Text = "Return Book";
            this.btnReturnBook.UseVisualStyleBackColor = true;
            this.btnReturnBook.Click += new System.EventHandler(this.btnReturnBook_Click);
            //
            // btnIssueBook
            //
            this.btnIssueBook.Location = new System.Drawing.Point(40, 240);
            this.btnIssueBook.Name = "btnIssueBook";
            this.btnIssueBook.Size = new System.Drawing.Size(120, 30);
            this.btnIssueBook.TabIndex = 2;
            this.btnIssueBook.Text = "Issue Book";
            this.btnIssueBook.UseVisualStyleBackColor = true;
            this.btnIssueBook.Click += new System.EventHandler(this.btnIssueBook_Click);
            //
            // groupBoxIssueDetails
            //
            this.groupBoxIssueDetails.Controls.Add(this.dtpIssueDate);
            this.groupBoxIssueDetails.Controls.Add(this.dtpDueDate);
            this.groupBoxIssueDetails.Controls.Add(this.cmbBorrowers);
            this.groupBoxIssueDetails.Controls.Add(this.cmbBooks);
            this.groupBoxIssueDetails.Controls.Add(this.lblDueDate);
            this.groupBoxIssueDetails.Controls.Add(this.lblIssueDate);
            this.groupBoxIssueDetails.Controls.Add(this.lblIssuedBorrower);
            this.groupBoxIssueDetails.Controls.Add(this.lblIssuedBook);
            this.groupBoxIssueDetails.Location = new System.Drawing.Point(8, 15);
            this.groupBoxIssueDetails.Name = "groupBoxIssueDetails";
            this.groupBoxIssueDetails.Size = new System.Drawing.Size(320, 200);
            this.groupBoxIssueDetails.TabIndex = 1;
            this.groupBoxIssueDetails.TabStop = false;
            this.groupBoxIssueDetails.Text = "Issue/Return Details";
            //
            // dtpIssueDate
            //
            this.dtpIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIssueDate.Location = new System.Drawing.Point(130, 120);
            this.dtpIssueDate.Name = "dtpIssueDate";
            this.dtpIssueDate.Size = new System.Drawing.Size(170, 20);
            this.dtpIssueDate.TabIndex = 7;
            //
            // dtpDueDate
            //
            this.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDueDate.Location = new System.Drawing.Point(130, 155);
            this.dtpDueDate.Name = "dtpDueDate";
            this.dtpDueDate.Size = new System.Drawing.Size(170, 20);
            this.dtpDueDate.TabIndex = 6;
            //
            // cmbBorrowers
            //
            this.cmbBorrowers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBorrowers.FormattingEnabled = true;
            this.cmbBorrowers.Location = new System.Drawing.Point(130, 85);
            this.cmbBorrowers.Name = "cmbBorrowers";
            this.cmbBorrowers.Size = new System.Drawing.Size(170, 21);
            this.cmbBorrowers.TabIndex = 5;
            //
            // cmbBooks
            //
            this.cmbBooks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBooks.FormattingEnabled = true;
            this.cmbBooks.Location = new System.Drawing.Point(130, 50);
            this.cmbBooks.Name = "cmbBooks";
            this.cmbBooks.Size = new System.Drawing.Size(170, 21);
            this.cmbBooks.TabIndex = 4;
            //
            // lblDueDate
            //
            this.lblDueDate.AutoSize = true;
            this.lblDueDate.Location = new System.Drawing.Point(20, 155);
            this.lblDueDate.Name = "lblDueDate";
            this.lblDueDate.Size = new System.Drawing.Size(56, 13);
            this.lblDueDate.TabIndex = 3;
            this.lblDueDate.Text = "Due Date:";
            //
            // lblIssueDate
            //
            this.lblIssueDate.AutoSize = true;
            this.lblIssueDate.Location = new System.Drawing.Point(20, 120);
            this.lblIssueDate.Name = "lblIssueDate";
            this.lblIssueDate.Size = new System.Drawing.Size(61, 13);
            this.lblIssueDate.TabIndex = 2;
            this.lblIssueDate.Text = "Issue Date:";
            //
            // lblIssuedBorrower
            //
            this.lblIssuedBorrower.AutoSize = true;
            this.lblIssuedBorrower.Location = new System.Drawing.Point(20, 85);
            this.lblIssuedBorrower.Name = "lblIssuedBorrower";
            this.lblIssuedBorrower.Size = new System.Drawing.Size(51, 13);
            this.lblIssuedBorrower.TabIndex = 1;
            this.lblIssuedBorrower.Text = "Borrower:";
            //
            // lblIssuedBook
            //
            this.lblIssuedBook.AutoSize = true;
            this.lblIssuedBook.Location = new System.Drawing.Point(20, 50);
            this.lblIssuedBook.Name = "lblIssuedBook";
            this.lblIssuedBook.Size = new System.Drawing.Size(35, 13);
            this.lblIssuedBook.TabIndex = 0;
            this.lblIssuedBook.Text = "Book:";
            //
            // dgvIssuedBooks
            //
            this.dgvIssuedBooks.AllowUserToAddRows = false;
            this.dgvIssuedBooks.AllowUserToDeleteRows = false;
            this.dgvIssuedBooks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvIssuedBooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvIssuedBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIssuedBooks.Location = new System.Drawing.Point(340, 15);
            this.dgvIssuedBooks.Name = "dgvIssuedBooks";
            this.dgvIssuedBooks.ReadOnly = true;
            this.dgvIssuedBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvIssuedBooks.Size = new System.Drawing.Size(620, 500);
            this.dgvIssuedBooks.TabIndex = 0;
            this.dgvIssuedBooks.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvIssuedBooks_CellClick);
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.tabControlMain);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Library Management System";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageBooks.ResumeLayout(false);
            this.groupBoxBookDetails.ResumeLayout(false);
            this.groupBoxBookDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalCopies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPublicationYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).EndInit();
            this.tabPageBorrowers.ResumeLayout(false);
            this.groupBoxBorrowerDetails.ResumeLayout(false);
            this.groupBoxBorrowerDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowers)).EndInit();
            this.tabPageIssuedBooks.ResumeLayout(false);
            this.groupBoxIssueDetails.ResumeLayout(false);
            this.groupBoxIssueDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIssuedBooks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageBooks;
        private System.Windows.Forms.TabPage tabPageBorrowers;
        private System.Windows.Forms.DataGridView dgvBooks;
        private System.Windows.Forms.GroupBox groupBoxBookDetails;
        private System.Windows.Forms.Label lblBookTitle;
        private System.Windows.Forms.Label lblTotalCopies;
        private System.Windows.Forms.Label lblBookISBN;
        private System.Windows.Forms.Label lblPublicationYear;
        private System.Windows.Forms.Label lblBookAuthor;
        private System.Windows.Forms.TextBox txtBookISBN;
        private System.Windows.Forms.TextBox txtBookAuthor;
        private System.Windows.Forms.TextBox txtBookTitle;
        private System.Windows.Forms.NumericUpDown numTotalCopies;
        private System.Windows.Forms.NumericUpDown numPublicationYear;
        private System.Windows.Forms.Button btnBookClear;
        private System.Windows.Forms.Button btnBookDelete;
        private System.Windows.Forms.Button btnBookUpdate;
        private System.Windows.Forms.Button btnBookAdd;
        private System.Windows.Forms.Button btnBorrowerClear;
        private System.Windows.Forms.Button btnBorrowerDelete;
        private System.Windows.Forms.Button btnBorrowerUpdate;
        private System.Windows.Forms.Button btnBorrowerAdd;
        private System.Windows.Forms.GroupBox groupBoxBorrowerDetails;
        private System.Windows.Forms.TextBox txtBorrowerAddress;
        private System.Windows.Forms.TextBox txtBorrowerPhone;
        private System.Windows.Forms.TextBox txtBorrowerEmail;
        private System.Windows.Forms.TextBox txtBorrowerName;
        private System.Windows.Forms.Label lblBorrowerAddress;
        private System.Windows.Forms.Label lblBorrowerPhone;
        private System.Windows.Forms.Label lblBorrowerEmail;
        private System.Windows.Forms.Label lblBorrowerName;
        private System.Windows.Forms.DataGridView dgvBorrowers;
        private System.Windows.Forms.TabPage tabPageIssuedBooks;
        private System.Windows.Forms.DataGridView dgvIssuedBooks;
        private System.Windows.Forms.Button btnReturnBook;
        private System.Windows.Forms.Button btnIssueBook;
        private System.Windows.Forms.GroupBox groupBoxIssueDetails;
        private System.Windows.Forms.Label lblDueDate;
        private System.Windows.Forms.Label lblIssueDate;
        private System.Windows.Forms.Label lblIssuedBorrower;
        private System.Windows.Forms.Label lblIssuedBook;
        private System.Windows.Forms.DateTimePicker dtpIssueDate;
        private System.Windows.Forms.DateTimePicker dtpDueDate;
        private System.Windows.Forms.ComboBox cmbBorrowers;
        private System.Windows.Forms.ComboBox cmbBooks;
    }
}