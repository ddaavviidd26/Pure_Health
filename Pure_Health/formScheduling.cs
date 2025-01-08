using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

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
            string query = @"
                INSERT INTO dbo.Table_4 ([Patient name],[Contact no.], Price,[Schedule time], [Test to conduct], Doctor) 
                VALUES (@Text1, @Text2, @Text6, @DateValue, @Combo1, @Combo2)";
            try
            {
                string text1 = textBox1.Text;
                string text2 = textBox2.Text;
                string text6 = label12.Text.Replace("Price: ₱", ""); // Remove label text prefix
                DateTime dateValue = dateTimePicker1.Value;
                string combo1 = comboBox1.SelectedItem?.ToString() ?? "";
                string combo2 = comboBox2.SelectedItem?.ToString() ?? "";

                if (string.IsNullOrWhiteSpace(text1) || string.IsNullOrWhiteSpace(combo1))
                {
                    MessageBox.Show("Please fill in all required fields.");
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Text1", text1);
                        command.Parameters.AddWithValue("@Text2", text2);
                        command.Parameters.AddWithValue("@Text6", text6);
                        command.Parameters.AddWithValue("@DateValue", dateValue);
                        command.Parameters.AddWithValue("@Combo1", combo1);
                        command.Parameters.AddWithValue("@Combo2", combo2);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data saved successfully!");
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
                MessageBox.Show("An error occurred: " + ex.Message);
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
    }
}
