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
    public partial class formReports : Form
    {

        public formReports()
        {
            InitializeComponent();
            Anchor = AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;
        }

        private void formReports_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            CustomizeDataGridView();
            LoadDataIntoDataGridView();
            this.ControlBox = false;
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string query = "SELECT * FROM dbo.Table_6";
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

            // General Appearance sda
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void LoadDataIntoDataGridView()
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string query = "SELECT * FROM dbo.Table_6"; // Adjust the query as needed

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable; // Replace myDataGridView with your actual DataGridView name
            }
        }
        public void RefreshTable6()
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string query = "SELECT [Date], GROSS, UTZ, LAB, XRAY, ECG, ECHO FROM dbo.Table_6";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable table6Data = new DataTable();
                        adapter.Fill(table6Data);

                        dataGridView1.DataSource = table6Data;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while refreshing Table_6: {ex.Message}");
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            {
                string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
                string queryTotals = @"
    SELECT 
        SUM(GROSS) AS TotalGross,
        SUM(UTZ) AS TotalUTZ,
        SUM(LAB) AS TotalLAB,
        SUM(XRAY) AS TotalXRAY,
        SUM(ECG) AS TotalECG,
        SUM(ECHO) AS TotalECHO
    FROM dbo.Table_6";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(queryTotals, connection))
                        {
                            command.CommandTimeout = 120; // Increase timeout to 120 seconds

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Update labels with retrieved totals
                                    label3.Text = reader["TotalGross"] != DBNull.Value ? reader["TotalGross"].ToString() : "0";
                                    label4.Text = reader["TotalUTZ"] != DBNull.Value ? reader["TotalUTZ"].ToString() : "0";
                                    label5.Text = reader["TotalLAB"] != DBNull.Value ? reader["TotalLAB"].ToString() : "0";
                                    label6.Text = reader["TotalXRAY"] != DBNull.Value ? reader["TotalXRAY"].ToString() : "0";
                                    label7.Text = reader["TotalECG"] != DBNull.Value ? reader["TotalECG"].ToString() : "0";
                                    label8.Text = reader["TotalECHO"] != DBNull.Value ? reader["TotalECHO"].ToString() : "0";

                                    MessageBox.Show("Totals updated successfully!");
                                }
                                else
                                {
                                    MessageBox.Show("No data found in Table_6.");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while updating totals: {ex.Message}");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Confirm with the user
            var confirmResult = MessageBox.Show(
                "Are you sure you want to delete all data in this Table? This action cannot be undone.",
                "Confirm Clear",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
                string deleteQuery = "DELETE FROM dbo.Table_6";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {

                        connection.Open();

                        using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                        {
                            command.CommandTimeout = 120; // Increase timeout to 120 seconds

                            int rowsAffected = command.ExecuteNonQuery();

                            MessageBox.Show(
                                rowsAffected > 0
                                ? $"{rowsAffected} rows deleted successfully."
                                : "This table is already empty.",
                                "Clear Successful",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                    }

                    // Refresh the display to reflect the cleared table
                    RefreshTable6();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while clearing Table_6: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        }
}



