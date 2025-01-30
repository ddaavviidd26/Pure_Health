﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pure_Health
{
   
    public partial class formPatient : Form
    {

        private string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
        public formPatient()
        {
            InitializeComponent();        
            this.Paint += Form1_Paint;
            this.Load += formPatient_Load;
            Anchor = AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            textBox4.TextChanged += ValidateAge;
            textBox3.TextChanged += ValidateContactNumber;
            LoadPatientData(""); // Load all data initially
            txtSearch.TextChanged += TxtSearch_TextChanged;

        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Define the radius for rounded corners
            int radius = 15;

            // Create a GraphicsPath to define the rounded corners
            GraphicsPath path = new GraphicsPath();

            // Add rounded corners to the path
            path.AddArc(0, 0, radius, radius, 180, 90);  // Top-left corner
            path.AddArc(dataGridView1.Width - radius - 1, 0, radius, radius, 270, 90);  // Top-right corner
            path.AddArc(dataGridView1.Width - radius - 1, dataGridView1.Height - radius - 1, radius, radius, 0, 90);  // Bottom-right corner
            path.AddArc(0, dataGridView1.Height - radius - 1, radius, radius, 90, 90);  // Bottom-left corner
            path.CloseFigure();

            // Apply the path as the region for the DataGridView (this gives it rounded corners)
            dataGridView1.Region = new Region(path);
        }

        private void CustomizeDataGridView()
        {

            // General Appearance
            dataGridView1.BackgroundColor = Color.FromArgb(242, 240, 230);
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.GridColor = Color.FromArgb(202, 186, 153);
            dataGridView1.EnableHeadersVisualStyles = false;

            // Header Style
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(104, 141, 94);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Cambria", 13, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersHeight = 50;

            // Cell Style
            dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(231, 224, 202);
            dataGridView1.DefaultCellStyle.ForeColor = Color.FromArgb(74, 54, 35);
            dataGridView1.DefaultCellStyle.Font = new Font("Cambria", 12);
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(181, 201, 143);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.Padding = new Padding(5);

            // Alternating Rows
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(219, 212, 190);

            // Row Template
            dataGridView1.RowTemplate.Height = 35;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Column Behavior
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            LoadPatientData(searchTerm);
        }
        private void LoadPatientData(string searchTerm)
        {
            string query = "SELECT * FROM dbo.Table_1 WHERE 1=1";

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                if (int.TryParse(searchTerm, out _))
                {
                    query += " AND ID = @SearchTerm"; // Numeric ID search
                }
                else
                {
                    query += " AND ([Patient name] LIKE @SearchTerm OR Address LIKE @SearchTerm OR Gender LIKE @SearchTerm OR [Date today] LIKE @SearchTerm)";
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        cmd.Parameters.AddWithValue("@SearchTerm", searchTerm.All(char.IsDigit) ? searchTerm : $"%{searchTerm}%");
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
            }
        }
    
    private void formPatient_Load(object sender, EventArgs e)
        {
            textBox4.ForeColor = Color.Black;
            textBox3.ForeColor = Color.Black;
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;

            comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;            

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd"; // Display format for the date
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "yyyy-MM-dd"; // Display format for the date
            CustomizeDataGridView();
            this.ControlBox = false;
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string query = "SELECT * FROM dbo.Table_1";
            try
            {
                // Create a connection object
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Create a data adapter
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                    // Create a DataTable to hold the data
                    DataTable dataTable = new DataTable();

                    // Fill the DataTable
                    adapter.Fill(dataTable);

                    // Bind the DataTable to the DataGridView
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        // Enable DataGridView Editing (Set in Form Load or Constructor)

        // Event: Auto-Save Data When User Edits a Cell

        private void ValidateAge(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!int.TryParse(textBox.Text, out _)) // If not a number
            {
                textBox.ForeColor = Color.Red;
            }
            else
            {
                textBox.ForeColor = Color.Black;
            }
        }

        private void ValidateContactNumber(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!System.Text.RegularExpressions.Regex.IsMatch(textBox.Text, @"^\d{10,12}$")) // 10-12 digit number
            {
                textBox.ForeColor = Color.Red;
            }
            else
            {
                textBox.ForeColor = Color.Black;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var popup = new checklistboxform())
            {
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    // Update the total price and selected items
                    textBox6.Text = popup.TotalPrice.ToString("F2");
                    textBox5.Text = string.Join(Environment.NewLine, popup.SelectedItems);

                    // Get selected categories (LAB, XRAY, etc.)
                    List<string> labItems = popup.SelectedItems.Where(item => item.Contains("LAB")).ToList();
                    List<string> utzItems = popup.SelectedItems.Where(item => item.Contains("UTZ")).ToList();

                    // Reflect categories in textBox7
                    textBox7.Clear(); // Clear the existing text in textBox7 before adding new ones
                    foreach (var item in popup.SelectedItems)
                    {
                        var itemName = item.Split('(')[1].Replace(")", "").Trim(); // Extract the category (LAB, XRAY, etc.)
                        textBox7.Text += itemName + Environment.NewLine;
                    }
 
                   
                }
            }
        }




        public void UpdateSummary(List<string> selectedItems, float totalPrice)
        {
            // Update a TextBox with the selected items and total price
            textBox6.Text = string.Join(", ", selectedItems);
            textBox5.Text = totalPrice.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                DateTime selectedDate = dateTimePicker1.Value.Date;

                string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
                string queryTable1Insert = @"
    INSERT INTO dbo.Table_1 ([Patient name], Address, [Contact no.], Age, [Test to conduct], Price, categories, Birthdate, [Date today], Gender, Referral) 
    VALUES (@Text1, @Text2, @Text3, @Text4, @Text5, @Text6, @Text7, @DateValue, @DateValue1, @Combo1, @Combo2)";

                string queryTable1Update = @"
    UPDATE dbo.Table_1
    SET Address = @Text2, [Contact no.] = @Text3, Age = @Text4, [Test to conduct] = @Text5, Price = @Text6, categories = @Text7, Birthdate = @DateValue, 
        [Date today] = @DateValue1, Gender = @Combo1, Referral = @Combo2
    WHERE [Patient name] = @Text1";

                try
                {
                    // Gather input from TextBoxes
                    string text1 = textBox1.Text; // Patient Name
                    string text2 = textBox2.Text; // Address
                    string text3 = textBox3.Text; // Contact no.
                    string text4 = textBox4.Text; // Age
                    string text5 = textBox5.Text; // Test to conduct

                    // Convert textBox6 value to a float for Price
                    string text6 = textBox6.Text;
                    if (!float.TryParse(text6, out float priceValue))
                    {
                        MessageBox.Show("Invalid price entered. Please enter a valid numeric value for the price.");
                        return;
                    }
                    if (!long.TryParse(text3, out _) || text3.Length != 10)
                    {
                        MessageBox.Show("Invalid Contact Number. Please enter exactly 10 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (!int.TryParse(text4, out int ageValue) || text4.Length > 3)
                    {
                        MessageBox.Show("Invalid Age. Please enter a valid number with a maximum of 3 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    // Split multiline input in textBox7 into individual categories
                    string[] categories = textBox7.Text
                        .Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(c => c.Trim())
                        .ToArray();

                    // Get values from DateTimePicker
                    DateTime dateValue = dateTimePicker1.Value; // Birthdate
                    DateTime dateValue1 = DateTime.Today; // Date today                

                    // Ensure "Date Today" is not in the past
                    if (dateValue1 < DateTime.Today)
                    {
                        MessageBox.Show("You cannot record data for past dates. Please select a future date.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // Exit method if the date is in the past
                    }

                    // Gather input from ComboBoxes
                    string combo1 = comboBox1.SelectedItem?.ToString() ?? ""; // Gender
                    string combo2 = comboBox2.SelectedItem?.ToString() ?? ""; // Referral

                    // Validation
                    if (string.IsNullOrWhiteSpace(text1) || string.IsNullOrWhiteSpace(combo1))
                    {
                        MessageBox.Show("Please fill in all required fields.");
                        return;
                    }

                    // Create a connection to SQL Server
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Start a transaction to ensure consistency
                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                // Check if the Patient name already exists in the database
                                string queryCheckExistence = "SELECT COUNT(*) FROM dbo.Table_1 WHERE [Patient name] = @Text1";
                                int count;
                                using (SqlCommand checkCommand = new SqlCommand(queryCheckExistence, connection, transaction))
                                {
                                    checkCommand.Parameters.AddWithValue("@Text1", text1);
                                    count = (int)checkCommand.ExecuteScalar();
                                }

                                if (count > 0)
                                {
                                    // If the Patient name exists, update the record
                                    using (SqlCommand updateCommand = new SqlCommand(queryTable1Update, connection, transaction))
                                    {
                                        updateCommand.Parameters.AddWithValue("@Text1", text1);
                                        updateCommand.Parameters.AddWithValue("@Text2", text2);
                                        updateCommand.Parameters.AddWithValue("@Text3", text3);
                                        updateCommand.Parameters.AddWithValue("@Text4", text4);
                                        updateCommand.Parameters.AddWithValue("@Text5", text5);
                                        updateCommand.Parameters.AddWithValue("@Text6", priceValue);
                                        updateCommand.Parameters.AddWithValue("@Text7", string.Join(", ", categories)); // Store all categories as a comma-separated string
                                        updateCommand.Parameters.AddWithValue("@DateValue", dateValue);
                                        updateCommand.Parameters.AddWithValue("@DateValue1", dateValue1);
                                        updateCommand.Parameters.AddWithValue("@Combo1", combo1);
                                        updateCommand.Parameters.AddWithValue("@Combo2", combo2);

                                        updateCommand.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    // If the Patient name does not exist, insert a new row
                                    using (SqlCommand insertCommand = new SqlCommand(queryTable1Insert, connection, transaction))
                                    {
                                        insertCommand.Parameters.AddWithValue("@Text1", text1);
                                        insertCommand.Parameters.AddWithValue("@Text2", text2);
                                        insertCommand.Parameters.AddWithValue("@Text3", text3);
                                        insertCommand.Parameters.AddWithValue("@Text4", text4);
                                        insertCommand.Parameters.AddWithValue("@Text5", text5);
                                        insertCommand.Parameters.AddWithValue("@Text6", priceValue);
                                        insertCommand.Parameters.AddWithValue("@Text7", string.Join(", ", categories)); // Store all categories as a comma-separated string
                                        insertCommand.Parameters.AddWithValue("@DateValue", dateValue);
                                        insertCommand.Parameters.AddWithValue("@DateValue1", dateValue1);
                                        insertCommand.Parameters.AddWithValue("@Combo1", combo1);
                                        insertCommand.Parameters.AddWithValue("@Combo2", combo2);

                                        insertCommand.ExecuteNonQuery();
                                    }
                                }

                                // Update or Insert into Table_6 (same as in your original code)
                                string queryCheckDate = "SELECT COUNT(*) FROM dbo.Table_6 WHERE [Date] = @DateToday";
                                int countTable6;
                                using (SqlCommand checkCommand = new SqlCommand(queryCheckDate, connection, transaction))
                                {
                                    checkCommand.Parameters.AddWithValue("@DateToday", dateValue1);
                                    countTable6 = (int)checkCommand.ExecuteScalar();
                                }

                                string queryUpdateOrInsertTable6;
                                if (countTable6 > 0)
                                {
                                    // Update existing row in Table_6
                                    queryUpdateOrInsertTable6 = @"
                        UPDATE dbo.Table_6
                        SET 
                            GROSS = GROSS + @Price,
                            UTZ = UTZ + @UTZCount,
                            LAB = LAB + @LABCount,
                            XRAY = XRAY + @XRAYCount,
                            ECG = ECG + @ECGCount,
                            ECHO = ECHO + @ECHOCount
                        WHERE [Date] = @DateToday";
                                }
                                else
                                {
                                    // Insert new row into Table_6
                                    queryUpdateOrInsertTable6 = @"
                        INSERT INTO dbo.Table_6 ([Date], GROSS, UTZ, LAB, XRAY, ECG, ECHO)
                        VALUES (@DateToday, @Price, @UTZCount, @LABCount, @XRAYCount, @ECGCount, @ECHOCount)";
                                }

                                // Count the occurrences of each category
                                int utzCount = categories.Count(c => c.Equals("UTZ", StringComparison.OrdinalIgnoreCase));
                                int labCount = categories.Count(c => c.Equals("LAB", StringComparison.OrdinalIgnoreCase));
                                int xrayCount = categories.Count(c => c.Equals("XRAY", StringComparison.OrdinalIgnoreCase));
                                int ecgCount = categories.Count(c => c.Equals("ECG", StringComparison.OrdinalIgnoreCase));
                                int echoCount = categories.Count(c => c.Equals("ECHO", StringComparison.OrdinalIgnoreCase));

                                using (SqlCommand updateOrInsertCommand = new SqlCommand(queryUpdateOrInsertTable6, connection, transaction))
                                {
                                    updateOrInsertCommand.Parameters.AddWithValue("@DateToday", dateValue1);
                                    updateOrInsertCommand.Parameters.AddWithValue("@Price", priceValue);
                                    updateOrInsertCommand.Parameters.AddWithValue("@UTZCount", utzCount);
                                    updateOrInsertCommand.Parameters.AddWithValue("@LABCount", labCount);
                                    updateOrInsertCommand.Parameters.AddWithValue("@XRAYCount", xrayCount);
                                    updateOrInsertCommand.Parameters.AddWithValue("@ECGCount", ecgCount);
                                    updateOrInsertCommand.Parameters.AddWithValue("@ECHOCount", echoCount);

                                    int rowsAffected = updateOrInsertCommand.ExecuteNonQuery();
                                    if (rowsAffected > 0)
                                    {
                                        // Successfully updated or inserted into Table_6
                                    }
                                    else
                                    {
                                        MessageBox.Show("Failed to update Table_6. Please check your input.");
                                    }
                                }

                                // Commit transaction
                                transaction.Commit();

                                // Provide feedback to the user
                                MessageBox.Show("Data saved successfully!");

                                // Refresh DataGridViews
                                ReorganizeIDs();
                                LoadDataIntoDataGridView();

                                // Clear input fields after adding data
                                ClearInputFields();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                MessageBox.Show($"An error occurred: {ex.Message}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }


          // Method to Load Data into DataGridView
            private void LoadDataIntoDataGridView()
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string query = "SELECT * FROM dbo.Table_1";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a data adapter to fetch data
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);

                    // Create a DataTable to hold the data
                    System.Data.DataTable dataTable = new System.Data.DataTable();

                    // Fill the DataTable with data from the database
                    dataAdapter.Fill(dataTable);

                    // Bind the DataTable to the DataGridView
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                // Handle errors
                MessageBox.Show("An error occurred while loading data: " + ex.Message);
            }
        }
        private void ClearInputFields()
        {
            // Clear TextBoxes
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            

            // Reset DateTimePicker
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;

            // Reset ComboBoxes
            comboBox1.SelectedIndex = -1; // Deselect any selected item
            comboBox2.SelectedIndex = -1;

            // Clear label text if needed
            textBox6.Text = "";
            textBox7.Clear();
        }


        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {



        }
        private void UpdateRowNumbers()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells["RowNumber"].Value = (i + 1).ToString();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            // Check if any row is selected
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the primary key (or unique identifier) of the selected row
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                // Ask for confirmation
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?",
                                                      "Confirm Delete",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Delete the record from the database
                    DeleteRecordFromDatabase(id);


                    ReorganizeIDs();



                    // Refresh the DataGridView
                    LoadDataIntoDataGridView();


                }
            }
            else
            {
                // No row selected
                MessageBox.Show("Please select a row to delete.");
            }

        }
        private void DeleteRecordFromDatabase(int id)
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string query = "DELETE FROM dbo.Table_1 WHERE Id = @Id";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add the parameter to the query
                        command.Parameters.AddWithValue("@Id", id);

                        // Open the connection and execute the query
                        connection.Open();
                        command.ExecuteNonQuery();

                        MessageBox.Show("Record deleted successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle errors
                MessageBox.Show("An error occurred while deleting the record: " + ex.Message);
            }
        }
        private void ReorganizeIDs()
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";

            string reorganizeQuery = @"
    BEGIN TRANSACTION;

    SELECT ROW_NUMBER() OVER (ORDER BY Id) AS NewId, [Patient name], Address, [Contact no.], Age, [Test to conduct], Price, categories,Birthdate, [Date today], Gender, Referral
    INTO #TempTable
    FROM dbo.Table_1;

    TRUNCATE TABLE dbo.Table_1;

    SET IDENTITY_INSERT dbo.Table_1 ON;

    INSERT INTO dbo.Table_1 (Id, [Patient name], Address, [Contact no.], Age, [Test to conduct], Price, categories,Birthdate, [Date today], Gender, Referral)
    SELECT NewId, [Patient name], Address, [Contact no.], Age, [Test to conduct], Price, categories,Birthdate, [Date today], Gender, Referral
    FROM #TempTable;

    SET IDENTITY_INSERT dbo.Table_1 OFF;

    DROP TABLE #TempTable;

    DBCC CHECKIDENT ('dbo.Table_1', RESEED, 0);

    COMMIT TRANSACTION;
";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(reorganizeQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during reorganization: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Populate controls with selected row's data
                textBox1.Text = dataGridView1.SelectedRows[0].Cells["Patient name"].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells["Address"].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells["Contact no."].Value.ToString();
                textBox4.Text = dataGridView1.SelectedRows[0].Cells["Age"].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells["Test to conduct"].Value.ToString();
                textBox6.Text = dataGridView1.SelectedRows[0].Cells["Price"].Value.ToString();
                textBox7.Text = dataGridView1.SelectedRows[0].Cells["categories"].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["Birthdate"].Value);
                dateTimePicker2.Value = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["Date today"].Value);
                comboBox1.SelectedItem = dataGridView1.SelectedRows[0].Cells["Gender"].Value.ToString();
                comboBox2.SelectedItem = dataGridView1.SelectedRows[0].Cells["Referral"].Value.ToString();

                MessageBox.Show("Data loaded for editing.");
            }
            else
            {
                MessageBox.Show("Please select a row to edit.");
            }
        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            // Get updated values from controls
            string name = textBox1.Text;
            string address = textBox2.Text;
            string contactNo = textBox3.Text;          
            int age = int.Parse(textBox4.Text);
            string testToConduct = textBox5.Text;
            decimal price = decimal.Parse(textBox6.Text);
            string categories = textBox7.Text;
            DateTime birthdate = dateTimePicker1.Value;
            DateTime Datetoday = dateTimePicker2.Value;
            string gender = comboBox1.SelectedItem?.ToString();
            string referral = comboBox2.SelectedItem?.ToString();

            // Validation (optional)
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            // Update the record in the database
            UpdateRecordInDatabase(name, address, contactNo, age, testToConduct,  price, categories, birthdate,Datetoday, gender, referral);

            // Refresh the DataGridView
            LoadDataIntoDataGridView();
        }
        private void UpdateRecordInDatabase(string name, string address, string contactNo, int age, string testToConduct, decimal price, string categories, DateTime birthdate, DateTime datetoday, string gender, string referral)
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string query = @"
        UPDATE dbo.Table_1 
        SET Address = @Address, [Contact no.] = @ContactNo, Age = @Age, [Test to conduct] = @TestToConduct, 
            Price = @Price,categories = @categories, Birthdate = @Birthdate, [Date today] = @Datetoday, Gender = @Gender, Referral = @Referral
        WHERE [Patient name] = @Name";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the query
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Address", address);
                        command.Parameters.AddWithValue("@ContactNo", contactNo);
                        command.Parameters.AddWithValue("@Age", age);
                        command.Parameters.AddWithValue("@TestToConduct", testToConduct);                       
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@categories", categories);
                        command.Parameters.AddWithValue("@Birthdate", birthdate);
                        command.Parameters.AddWithValue("@Datetoday", datetoday);
                        command.Parameters.AddWithValue("@Gender", gender);
                        command.Parameters.AddWithValue("@Referral", referral);

                        // Execute the query
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record updated successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Failed to update the record. Please check the data.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle errors
                MessageBox.Show("An error occurred while updating the record: " + ex.Message);
            }
        }
        

        private void button5_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string searchTerm = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchTerm))
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                MessageBox.Show("Please enter a search term.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }else
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
            string.Format("[Patient name] LIKE '%{0}%' OR Address LIKE '%{0}%' OR Gender LIKE '%{0}%'",
            searchTerm);
            }

            string query = "SELECT * FROM dbo.Table_1  WHERE 1=1"; // Start with a base condition

            // Check if the search term is numeric (ID search)
            if (int.TryParse(searchTerm, out int id))
            {
                query += " AND ID = @ID"; // Search by ID
            }
            else
            {
                query += " AND ([Patient name] LIKE @SearchTerm OR Address LIKE @SearchTerm OR Gender LIKE @SearchTerm) OR [Date today] LIKE @SearchTerm"; // Search by Name, Address, or Gender
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters
                    cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%"); // Search term for Name, Address, and Gender

                    // If the search term is numeric, add it for ID
                    if (query.Contains("ID"))
                    {
                        cmd.Parameters.AddWithValue("@ID", searchTerm); // Use the numeric search term for ID
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No records found matching the search criteria.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }


       

    }

}
    
