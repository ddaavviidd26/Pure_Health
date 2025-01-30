﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace Pure_Health
{
    public partial class formScheduling : Form
    {

        public formScheduling()
        {
            InitializeComponent();
            CustomizeSearchButton();
            // Initialize ComboBox with items and prices
            InitializeComboBoxWithPrices();
        }

        private void InitializeComboBoxWithPrices()
        {
            // Define items with prices
            var items = new[]
            {
                new ComboBoxItem("Ultrasound", 500m),
                new ComboBoxItem("Blood Chemistry", 250m),
                
            };

            // Bind items to ComboBox1
            comboBox1.DataSource = items;
            comboBox1.DisplayMember = "Name"; // Display name in ComboBox
            comboBox1.ValueMember = "Price"; // Value represents the price

            // Attach event handler for selection change
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        }
        private void CustomizeSearchButton()
        {
            // Set button properties
            btnSearch.Text = "Search";
            btnSearch.Font = new Font("Cambria", 14, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.BackColor = Color.FromArgb(104, 141, 94); // Modern green color
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.FlatAppearance.BorderSize = 0; // Remove border
            btnSearch.FlatAppearance.MouseOverBackColor = Color.FromArgb(81, 111, 72); // Darker green on hover
            btnSearch.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 85, 55); // Even darker green on click
            btnSearch.Size = new Size(100, 40); // Set button size
            btnSearch.Cursor = Cursors.Hand; // Hand cursor on hover

            txtSearch.BorderStyle = BorderStyle.None; // Remove default border
            txtSearch.Font = new Font("Cambria", 16, FontStyle.Regular);
            txtSearch.ForeColor = Color.FromArgb(74, 54, 35); // Light brown color for text
            txtSearch.BackColor = Color.FromArgb(231, 224, 202); // Soft beige background
            txtSearch.Size = new Size(100, 35); // Set size of the text box
            txtSearch.Padding = new Padding(10, 5, 10, 5); // Padding to give it a modern feel
            txtSearch.Cursor = Cursors.IBeam; // Text cursor when typing
            txtSearch.TextAlign = HorizontalAlignment.Left; // Text alignment
            txtSearch.MaxLength = 60; // Set a maximum character limit
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
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected item
            if (comboBox1.SelectedItem is ComboBoxItem selectedItem)
            {
                // Display the price in Label12
                label12.Text = $"₱{selectedItem.Price:F2}";
            }
        }

        // Custom class for ComboBox items
        public class ComboBoxItem
        {
            public string Name { get; set; }
            public decimal Price { get; set; }

            public ComboBoxItem(string name, decimal price)
            {
                Name = name;
                Price = price;
            }

            // Override ToString to display the item name in the ComboBox
            public override string ToString()
            {
                return Name;
            }
        }

        private void formScheduling_Load(object sender, EventArgs e)
        {
            CustomizeDataGridView();
            this.ControlBox = false;
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string query = "SELECT * FROM dbo.Table_4";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string insertQuery = @"
        INSERT INTO dbo.Table_4 ([Patient name],[Contact no.], Price,[Schedule time], [Test to conduct], Doctor) 
        VALUES (@Text1, @Text2, @Text6, @DateValue, @Combo1, @Combo2)";

            string updateTable6Query = @"
        IF EXISTS (SELECT 1 FROM dbo.Table_6 WHERE [Date] = @DateToday)
        BEGIN
            UPDATE dbo.Table_6
            SET 
                GROSS = GROSS + @Price,
                UTZ = UTZ + @UTZCount,
                LAB = LAB + @LABCount,
                XRAY = XRAY + @XRAYCount,
                ECG = ECG + @ECGCount,
                ECHO = ECHO + @ECHOCount
            WHERE [Date] = @DateToday
        END
        ELSE
        BEGIN
             INSERT INTO dbo.Table_6 ([Date], GROSS, UTZ, LAB, XRAY, ECG, ECHO)
            VALUES (@DateToday, @Price, @UTZCount, @LABCount, @XRAYCount, @ECGCount, @ECHOCount)
        END";

            try
            {
                string text1 = textBox1.Text; // Patient Name
                string text2 = textBox2.Text; // Contact No.

                // Extract and convert text6 (Price)
                string text6 = label12.Text;
               string cleanedText6 = Regex.Replace(text6, @"[^\d.]", ""); // Keep only digits and a single decimal point
if (decimal.TryParse(cleanedText6, out decimal priceValue))
{
    // Convert to integer by truncating the decimal portion
    int intPriceValue = (int)Math.Floor(priceValue);
}
else
{
    MessageBox.Show("Invalid price. Please ensure the price is numeric.");
    return;
}

                DateTime dateValue = dateTimePicker1.Value; // Schedule Time
                DateTime dateToday = DateTime.Now.Date; // Today's Date
                if (dateValue < DateTime.Today)
                {
                    MessageBox.Show("Invalid Date", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit method if the date is in the past
                }

                string combo1 = comboBox1.SelectedItem?.ToString() ?? ""; // Test to Conduct
                string combo2 = comboBox2.SelectedItem?.ToString() ?? ""; // Doctor

                if (string.IsNullOrWhiteSpace(text1) || string.IsNullOrWhiteSpace(combo1))
                {
                    MessageBox.Show("Please fill in all required fields.");
                    return;
                }

                // Determine counts for UTZ and LAB based on comboBox1 value
                int utzCount = combo1.Equals("Ultrasound", StringComparison.OrdinalIgnoreCase) ? 1 : 0;
                int labCount = combo1.Equals("Blood Chemistry", StringComparison.OrdinalIgnoreCase) ? 1 : 0;
                int xrayCount = combo1.Equals("", StringComparison.OrdinalIgnoreCase) ? 1 : 0;
                int ecgCount = combo1.Equals("", StringComparison.OrdinalIgnoreCase) ? 1 : 0;
                int echoCount = combo1.Equals("", StringComparison.OrdinalIgnoreCase) ? 1 : 0;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Insert into Table_4
                            using (SqlCommand command = new SqlCommand(insertQuery, connection, transaction))
                            {
                                command.CommandTimeout = 120; // Increase timeout to 120 seconds

                                command.Parameters.AddWithValue("@Text1", text1);
                                command.Parameters.AddWithValue("@Text2", text2);
                                command.Parameters.AddWithValue("@Text6", priceValue);
                                command.Parameters.AddWithValue("@DateValue", dateValue);
                                command.Parameters.AddWithValue("@Combo1", combo1);
                                command.Parameters.AddWithValue("@Combo2", combo2);

                                command.ExecuteNonQuery();
                            }

                            // Update or Insert into Table_6
                            using (SqlCommand command = new SqlCommand(updateTable6Query, connection, transaction))
                            {
                                command.CommandTimeout = 120; // Increase timeout to 120 seconds

                                command.Parameters.AddWithValue("@DateToday", dateToday);
                                command.Parameters.AddWithValue("@Price", priceValue);
                                command.Parameters.AddWithValue("@UTZCount", utzCount);
                                command.Parameters.AddWithValue("@LABCount", labCount);
                                command.Parameters.AddWithValue("@XRAYCount", xrayCount);
                                command.Parameters.AddWithValue("@ECGCount", ecgCount);
                                command.Parameters.AddWithValue("@ECHOCount", echoCount);

                                command.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Data saved successfully!");
                            LoadDataIntoDataGridView(); // Refresh DataGridView
                            ClearInputFields(); // Clear input fields
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



        private void LoadDataIntoDataGridView()
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string query = "SELECT * FROM dbo.Table_4";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading data: " + ex.Message);
            }
        }

        private void ClearInputFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            dateTimePicker1.Value = DateTime.Now;
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            label12.Text = "";
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
            string query = "DELETE FROM dbo.Table_4 WHERE ID = @Id";

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

        -- Step 1: Create a temporary table with the same schema but without the IDENTITY property
        SELECT ROW_NUMBER() OVER (ORDER BY ID) AS NewId, [Patient name],[Contact no.], Price,[Schedule time], [Test to conduct], Doctor
        INTO #TempTable
        FROM dbo.Table_4;

        -- Step 2: Disable identity insert on the original table
        SET IDENTITY_INSERT dbo.Table_4 ON;

        -- Step 3: Truncate the original table
        TRUNCATE TABLE dbo.Table_4;

        -- Step 4: Insert the data back into the original table with sequential IDs
        INSERT INTO dbo.Table_4 (ID, [Patient name],[Contact no.], Price,[Schedule time], [Test to conduct], Doctor)
        SELECT NewId, [Patient name],[Contact no.], Price,[Schedule time], [Test to conduct], Doctor
        FROM #TempTable;

        -- Step 5: Drop the temporary table
        DROP TABLE #TempTable;

        -- Step 6: Reset the identity seed
        DBCC CHECKIDENT ('dbo.Table_4', RESEED, 0);

        -- Step 7: Re-enable identity insert
        SET IDENTITY_INSERT dbo.Table_4 OFF;

        COMMIT TRANSACTION;
    ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(reorganizeQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {


        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Populate controls with selected row's data
                textBox1.Text = dataGridView1.SelectedRows[0].Cells["Patient name"].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells["Contact no."].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["Schedule time"].Value);
               
                comboBox1.SelectedItem = dataGridView1.SelectedRows[0].Cells["Test to conduct"].Value.ToString();
                comboBox2.SelectedItem = dataGridView1.SelectedRows[0].Cells["Doctor"].Value.ToString();

                MessageBox.Show("Data loaded for editing.");
            }
            else
            {
                MessageBox.Show("Please select a row to edit.");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string searchTerm = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchTerm))
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                MessageBox.Show("Please enter a search term.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
            string.Format("[Patient name] LIKE '%{0}%'",
            searchTerm);
            }

            string query = "SELECT * FROM dbo.Table_4  WHERE 1=1"; // Start with a base condition

            // Check if the search term is numeric (ID search)
            if (int.TryParse(searchTerm, out int id))
            {
                query += " AND ID = @ID"; // Search by ID
            }
            else
            {
                query += " AND ([Patient name] LIKE @SearchTerm)"; // Search by Name, Address, or Gender
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
    
   

