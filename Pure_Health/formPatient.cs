using System;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Pure_Health
{
   
    public partial class formPatient : Form
    {

        public formPatient()
        {
            InitializeComponent();
            CustomizeSearchButton();
            this.Paint += Form1_Paint;
            this.Load += formPatient_Load;
            Anchor = AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

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
        
        private void formPatient_Load(object sender, EventArgs e)
        {
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

                    label12.Text = $"{popup.TotalPrice:C}";
                    textBox5.Text = string.Join(Environment.NewLine, popup.SelectedItems);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string query = @"
        INSERT INTO dbo.Table_1 ([Patient name], Address,[Contact no.], Age, [Test to conduct], Price, Birthdate, Gender, Referral) 
        VALUES (@Text1, @Text2, @Text3, @Text4, @Text5, @Text6, @DateValue, @Combo1, @Combo2)";
            try
            {
                // Gather input from TextBoxes
                string text1 = textBox1.Text;
                string text2 = textBox2.Text;
                string text3 = textBox3.Text;
                string text4 = textBox4.Text;
                string text5 = textBox5.Text;
                string text6 = label12.Text;

                // Get value from DateTimePicker
                DateTime dateValue = dateTimePicker1.Value;

                // Gather input from ComboBoxes
                string combo1 = comboBox1.SelectedItem?.ToString() ?? "";
                string combo2 = comboBox2.SelectedItem?.ToString() ?? "";

                // Validation (optional)
                if (string.IsNullOrWhiteSpace(text1) || string.IsNullOrWhiteSpace(combo1))
                {
                    MessageBox.Show("Please fill in all required fields.");
                    return;
                }

                // Create a connection to SQL Server
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a command object
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@Text1", text1);
                        command.Parameters.AddWithValue("@Text2", text2);
                        command.Parameters.AddWithValue("@Text3", text3);
                        command.Parameters.AddWithValue("@Text4", text4);
                        command.Parameters.AddWithValue("@Text5", text5);
                        command.Parameters.AddWithValue("@Text6", text6);
                        command.Parameters.AddWithValue("@DateValue", dateValue);
                        command.Parameters.AddWithValue("@Combo1", combo1);
                        command.Parameters.AddWithValue("@Combo2", combo2);

                        // Execute the command
                        int rowsAffected = command.ExecuteNonQuery();

                        // Provide feedback to the user
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data saved successfully!");

                            // Refresh the DataGridView
                            LoadDataIntoDataGridView();

                            ClearInputFields();

                        }
                        else
                        {
                            MessageBox.Show("Failed to save data.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle errors
                MessageBox.Show("An error occurred: " + ex.Message);
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

            // Reset ComboBoxes
            comboBox1.SelectedIndex = -1; // Deselect any selected item
            comboBox2.SelectedIndex = -1;

            // Clear label text if needed
            label12.Text = "";
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

        -- Step 1: Create a temporary table with the same schema but without the IDENTITY property
        SELECT ROW_NUMBER() OVER (ORDER BY Id) AS NewId, [Patient name], Address, [Contact no.], Age, [Test to conduct], Price, Birthdate, Gender, Referral
        INTO #TempTable
        FROM dbo.Table_1;

        -- Step 2: Disable identity insert on the original table
        SET IDENTITY_INSERT dbo.Table_1 ON;

        -- Step 3: Truncate the original table
        TRUNCATE TABLE dbo.Table_1;

        -- Step 4: Insert the data back into the original table with sequential IDs
        INSERT INTO dbo.Table_1 (Id, [Patient name], Address, [Contact no.], Age, [Test to conduct], Price, Birthdate, Gender, Referral)
        SELECT NewId, [Patient name], Address, [Contact no.], Age, [Test to conduct], Price, Birthdate, Gender, Referral
        FROM #TempTable;

        -- Step 5: Drop the temporary table
        DROP TABLE #TempTable;

        -- Step 6: Reset the identity seed
        DBCC CHECKIDENT ('dbo.Table_1', RESEED, 0);

        -- Step 7: Re-enable identity insert
        SET IDENTITY_INSERT dbo.Table_1 OFF;

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


        private void button4_Click(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Populate controls with selected row's data
                textBox1.Text = dataGridView1.SelectedRows[0].Cells["Patient name"].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells["Address"].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells["Contact no."].Value.ToString();
                textBox4.Text = dataGridView1.SelectedRows[0].Cells["Age"].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells["Test to conduct"].Value.ToString();
                label12.Text = dataGridView1.SelectedRows[0].Cells["Price"].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["Birthdate"].Value);
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
            decimal price = decimal.Parse(label12.Text);
            DateTime birthdate = dateTimePicker1.Value;
            string gender = comboBox1.SelectedItem?.ToString();
            string referral = comboBox2.SelectedItem?.ToString();

            // Validation (optional)
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            // Update the record in the database
            UpdateRecordInDatabase(name, address, contactNo, age, testToConduct, price, birthdate, gender, referral);

            // Refresh the DataGridView
            LoadDataIntoDataGridView();
        }
        private void UpdateRecordInDatabase(string name, string address, string contactNo, int age, string testToConduct, decimal price, DateTime birthdate, string gender, string referral)
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string query = @"
        UPDATE dbo.Table_1 
        SET Address = @Address, [Contact no.] = @ContactNo, Age = @Age, [Test to conduct] = @TestToConduct, 
            Price = @Price, Birthdate = @Birthdate, Gender = @Gender, Referral = @Referral
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
                        command.Parameters.AddWithValue("@Birthdate", birthdate);
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
            txtSearch.Size = new Size(150, 35); // Set size of the text box
            txtSearch.Padding = new Padding(10, 5, 10, 5); // Padding to give it a modern feel
            txtSearch.Cursor = Cursors.IBeam; // Text cursor when typing
            txtSearch.TextAlign = HorizontalAlignment.Left; // Text alignment
            txtSearch.MaxLength = 60; // Set a maximum character limit
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
                query += " AND ([Patient name] LIKE @SearchTerm OR Address LIKE @SearchTerm OR Gender LIKE @SearchTerm)"; // Search by Name, Address, or Gender
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
    
