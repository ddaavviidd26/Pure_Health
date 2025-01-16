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
        public void RefreshGross(DateTime date, decimal price)
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string updateGrossQuery = @"
        UPDATE dbo.Table_6 
        SET GROSS = GROSS + @Price
        WHERE Date = @Date";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(updateGrossQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@Date", date);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                // Reload the DataGridView in Form 2 to reflect the updated Gross
                LoadDailyData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void LoadDailyData()
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string query = @"
        SELECT Date, SUM(Price) AS Gross, SUM(CASE WHEN Lab = 1 THEN Price ELSE 0 END) AS Lab, 
               SUM(CASE WHEN UTZ = 1 THEN Price ELSE 0 END) AS UTZ, 
               SUM(CASE WHEN XRAY = 1 THEN Price ELSE 0 END) AS XRAY, 
               SUM(CASE WHEN ECG = 1 THEN Price ELSE 0 END) AS ECG, 
               SUM(CASE WHEN Echo = 1 THEN Price ELSE 0 END) AS Echo
        FROM dbo.Table_1 
        WHERE [Date Today] = @Date
        GROUP BY [Date Today]";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@Date", DateTime.Now.Date);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

    }

}
