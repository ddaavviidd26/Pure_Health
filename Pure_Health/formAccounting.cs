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

namespace Pure_Health
{
    public partial class formAccounting : Form
    {
        public formAccounting()
        {
            InitializeComponent();
            this.Paint += Form1_Paint;
            this.Load += formAccounting_Load;
            Anchor = AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;
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
        private void formAccounting_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            CustomizeDataGridView();
            MaskPasswordColumn(dataGridView1, "Password");
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string query = "SELECT * FROM dbo.Table_7";
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
        private void LoadDataIntoDataGridView()
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string query = "SELECT * FROM dbo.Table_7";

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
        private void MaskPasswordColumn(DataGridView dgv, string columnName)
        {
            // Check if the column exists
            if (dgv.Columns[columnName] != null)
            {
                // Handle the CellFormatting event to mask the password
                dgv.CellFormatting += (s, e) =>
                {
                    if (e.ColumnIndex == dgv.Columns[columnName].Index && e.Value != null)
                    {
                        // Replace the text with asterisks or any masking character
                        e.Value = new string('*', e.Value.ToString().Length);
                        e.FormattingApplied = true;
                    }
                };
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            {
                // Ensure a row is selected
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a row to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string username = selectedRow.Cells["Username"].Value?.ToString();
                string password = selectedRow.Cells["Password"].Value?.ToString(); // Actual password from the database

                // Prompt user for password and PureHealth code
                string enteredPassword = Prompt.ShowDialog("Enter the password for the selected user:", "Confirm Password");
                string enteredPureHealthCode = Prompt.ShowDialog("Enter the PureHealth code:", "Confirm PureHealth Code");

                // Hardcoded PureHealth code for validation
                string pureHealthCode = "Purehealth2025";

                // Validate the entered password and PureHealth code
                if (enteredPassword == password && enteredPureHealthCode == pureHealthCode)
                {
                    // Delete the row from the database
                    string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
                    string query = "DELETE FROM dbo.Table_7 WHERE Username = @Username";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@Username", username);
                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Row deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    // Remove the row from the DataGridView
                                    dataGridView1.Rows.Remove(selectedRow);
                                    ReorganizeIDs();
                                    RefreshUserAccountsGrid();
                                }
                                else
                                {
                                    MessageBox.Show("Failed to delete the row. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid password or PureHealth code. Deletion canceled.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void ReorganizeIDs()
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";

            string reorganizeQuery = @"
        BEGIN TRANSACTION;

        -- Step 1: Create a temporary table with the same schema but without the IDENTITY property
        SELECT ROW_NUMBER() OVER (ORDER BY Id) AS NewId, Username, Password, Email
        INTO #TempTable
        FROM dbo.Table_7;

        -- Step 2: Disable identity insert on the original table
        SET IDENTITY_INSERT dbo.Table_7 ON;

        -- Step 3: Truncate the original table
        TRUNCATE TABLE dbo.Table_7;

        -- Step 4: Insert the data back into the original table with sequential IDs
        INSERT INTO dbo.Table_7 (Id,Username, Password, Email)
        SELECT NewId,Username, Password, Email
        FROM #TempTable;

        -- Step 5: Drop the temporary table
        DROP TABLE #TempTable;

        -- Step 6: Reset the identity seed
        DBCC CHECKIDENT ('dbo.Table_7', RESEED, 0);

        -- Step 7: Re-enable identity insert
        SET IDENTITY_INSERT dbo.Table_7 OFF;

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
        public static class Prompt
        {
            public static string ShowDialog(string text, string caption)
            {
                // Create the form
                Form prompt = new Form
                {
                    Width = 350,
                    Height = 200,
                    FormBorderStyle = FormBorderStyle.None, // Remove default border
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen,
                    BackColor = Color.FromArgb(240, 240, 240) // Light gray background
                };

                // Rounded corners
                prompt.Paint += (sender, e) =>
                {
                    Rectangle rect = new Rectangle(0, 0, prompt.Width, prompt.Height);
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    e.Graphics.DrawRectangle(new Pen(Color.Gray, 1), rect);
                    e.Graphics.FillRectangle(new SolidBrush(prompt.BackColor), rect);
                };

                // Add label for message
                Label textLabel = new Label
                {
                    Left = 20,
                    Top = 30,
                    Text = text,
                    Font = new Font("Cambria", 10),
                    AutoSize = true,
                    ForeColor = Color.Black
                };

                // Add input textbox
                TextBox inputBox = new TextBox
                {
                    Left = 20,
                    Top = 70,
                    Width = 300,
                    Font = new Font("Cambria", 10),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    BorderStyle = BorderStyle.FixedSingle,
                    UseSystemPasswordChar = true,
                };

                // Add OK button
                Button confirmation = new Button
                {
                    Text = "OK",
                    Left = 220,
                    Width = 100,
                    Top = 120,
                    Height = 35,
                    Font = new Font("Cambria", 10),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(100, 149, 237), // Cornflower Blue
                    ForeColor = Color.White,
                    Cursor = Cursors.Hand
                };
                confirmation.FlatAppearance.BorderSize = 0; // No border
                confirmation.Click += (sender, e) => { prompt.DialogResult = DialogResult.OK; };

                // Add Cancel button
                Button cancelButton = new Button
                {
                    Text = "Cancel",
                    Left = 100,
                    Width = 100,
                    Top = 120,
                    Height = 35,
                    Font = new Font("Cambria", 10),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(220, 20, 60), // Crimson Red
                    ForeColor = Color.White,
                    Cursor = Cursors.Hand
                };
                cancelButton.FlatAppearance.BorderSize = 0; // No border
                cancelButton.Click += (sender, e) => { prompt.DialogResult = DialogResult.Cancel; };

                // Add controls to the form
                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(inputBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(cancelButton);

                // Show the dialog and return the input
                return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : string.Empty;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Createacc1 createAccForm = new Createacc1();

            // Show the form as a dialog and check if it was closed with DialogResult.OK
            if (createAccForm.ShowDialog() == DialogResult.OK)
            {
                // After the form is closed, refresh the DataGridView or take any other necessary actions
                ReorganizeIDs();
                RefreshUserAccountsGrid();
            }
        }

        private void RefreshUserAccountsGrid()
        {
            // Logic to refresh or reload data into the DataGridView
            // Example:
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string query = "SELECT * FROM dbo.Table_7";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;  // Assuming your DataGridView is named userDataGridView
            }
        }
        private void Createacc1_AccountCreated(object sender, EventArgs e)
        {
            RefreshUserAccountsGrid();
        }

    }      
    
}