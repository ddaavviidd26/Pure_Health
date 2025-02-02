using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pure_Health
{
    public partial class formInventory : Form
    {
        private string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
        public formInventory()
        {

            InitializeComponent();
            CustomizeDataGridView();
            this.Paint += Form1_Paint;
            this.Load += formInventory_Load;
            Anchor = AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            textBox4.TextChanged += ValidateQuantity;
            CustomizeSearchButton();
        }
        private void CustomizeSearchButton()
        {
            // Set button properties


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
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
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
        private void ValidateQuantity(object sender, EventArgs e)
        {
            TextBox textBox4 = sender as TextBox;
            if (!int.TryParse(textBox4.Text, out _)) // If not a number
            {
                textBox4.ForeColor = Color.Red;
            }
            else
            {
                textBox4.ForeColor = Color.Black;
            }
        }
        private void formInventory_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string query = "SELECT * FROM dbo.Table_2";
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

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";

            try
            {
                // Gather input from TextBoxes
                string text1 = textBox1.Text; // Supply name
                string text2 = textBox2.Text; // Description
                string text4 = textBox4.Text; // Quantity

                // Validate quantity
                if (!Regex.IsMatch(text4, @"^\d+$"))
                {
                    MessageBox.Show("Invalid. Only numbers are allowed.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(text1))
                {
                    MessageBox.Show("Please fill in all required fields.");
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL query to check and update or insert
                    string query = @"
                IF EXISTS (SELECT 1 FROM dbo.Table_2 WHERE [Supply name] = @Text1)
                BEGIN
                    UPDATE dbo.Table_2
                     SET Description = @Text2, Quantity = @Text4
                     WHERE [Supply name] = @Text1
                END
                ELSE
                BEGIN
                    INSERT INTO dbo.Table_2 ([Supply name], Description, Quantity) 
                    VALUES (@Text1, @Text2, @Text4)
                END";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandTimeout = 120; // Increase timeout to 120 seconds

                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@Text1", text1);
                        command.Parameters.AddWithValue("@Text2", text2);
                        command.Parameters.AddWithValue("@Text4", int.Parse(text4)); // Ensure Quantity is an integer

                        // Execute the command
                        int rowsAffected = command.ExecuteNonQuery();

                        // Provide feedback to the user
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data updated successfully!");

                            // Refresh the DataGridView
                            ReorganizeIDs();
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

        private void LoadDataIntoDataGridView()
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string query = "SELECT * FROM dbo.Table_2";

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
            
            textBox4.Clear();
            


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
            string query = "DELETE FROM dbo.Table_2 WHERE ID = @Id";

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

    -- Step 1: Create a temporary table with sequential IDs
    WITH Renumbered AS (
        SELECT 
            ROW_NUMBER() OVER (ORDER BY ID) AS NewId, 
            [Supply name], Description, Quantity
        FROM dbo.Table_2
    )
    SELECT * INTO #TempTable FROM Renumbered;

    -- Step 2: Delete all records from the original table
    DELETE FROM dbo.Table_2;

    -- Step 3: Reset identity to 0
    DBCC CHECKIDENT ('dbo.Table_2', RESEED, 0);

    -- Step 4: Enable identity insert to manually insert new IDs
    SET IDENTITY_INSERT dbo.Table_2 ON;

    -- Step 5: Insert the renumbered data
    INSERT INTO dbo.Table_2 (ID, [Supply name], Description, Quantity)
    SELECT NewId, [Supply name], Description, Quantity
    FROM #TempTable;

    -- Step 6: Disable identity insert
    SET IDENTITY_INSERT dbo.Table_2 OFF;

    -- Step 7: Drop the temporary table
    DROP TABLE #TempTable;

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
            // Check if a row is selected
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Populate controls with selected row's data
                textBox1.Text = dataGridView1.SelectedRows[0].Cells["Supply name"].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells["Description"].Value.ToString();
                
                textBox4.Text = dataGridView1.SelectedRows[0].Cells["Quantity"].Value.ToString();
               

                MessageBox.Show("Data loaded for editing.");
            }
            else
            {
                MessageBox.Show("Please select a row to edit.");
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            LoadPatientData(searchTerm);
        }
        private void LoadPatientData(string searchTerm)
        {
            string query = "SELECT * FROM dbo.Table_2 WHERE 1=1";

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                if (int.TryParse(searchTerm, out _))
                {
                    query += " AND ID = @SearchTerm"; // Numeric ID search
                }
                else
                {
                    query += " AND [Supply name] LIKE @SearchTerm"; // Fix the syntax
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        if (int.TryParse(searchTerm, out _))
                        {
                            cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%"); // Use wildcard for LIKE search
                        }
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
            }
        }
    }
}    

