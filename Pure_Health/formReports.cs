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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void UpdateGrossValue(DateTime dateValue, float price, List<string> categories)
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string query = "SELECT * FROM dbo.Table_1 WHERE [Date today] = @DateValue";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open(); // Ensure the connection is open
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@DateValue", dateValue);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        var row = dataTable.Rows[0];
                        float currentGross = Convert.ToSingle(row["GROSS"]);
                        row["GROSS"] = currentGross + price;

                        foreach (var category in categories)
                        {
                            if (category == "UTZ")
                                row["UTZ"] = Convert.ToInt32(row["UTZ"]) + 1;
                            else if (category == "LAB")
                                row["LAB"] = Convert.ToInt32(row["LAB"]) + 1;
                            else if (category == "XRAY")
                                row["XRAY"] = Convert.ToInt32(row["XRAY"]) + 1;
                            else if (category == "ECG")
                                row["ECG"] = Convert.ToInt32(row["ECG"]) + 1;
                            else if (category == "ECHO")
                                row["ECHO"] = Convert.ToInt32(row["ECHO"]) + 1;
                        }

                        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                        adapter.Update(dataTable);

                        // Reload the DataGridView in formReports
                        if (Owner is formReports formReports)
                        {
                            formReports.LoadDataIntoDataGridView();
                        }
                    }
                    else
                    {
                        string insertQuery = @"
                INSERT INTO dbo.Table_6 (Date, GROSS, UTZ, LAB, XRAY, ECG, ECHO) 
                VALUES (@Date, @GROSS, @UTZ, @LAB, @XRAY, @ECG, @ECHO)";

                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@Date", dateValue);
                            insertCommand.Parameters.AddWithValue("@GROSS", price);
                            insertCommand.Parameters.AddWithValue("@UTZ", categories.Count(c => c == "UTZ"));
                            insertCommand.Parameters.AddWithValue("@LAB", categories.Count(c => c == "LAB"));
                            insertCommand.Parameters.AddWithValue("@XRAY", categories.Count(c => c == "XRAY"));
                            insertCommand.Parameters.AddWithValue("@ECG", categories.Count(c => c == "ECG"));
                            insertCommand.Parameters.AddWithValue("@ECHO", categories.Count(c => c == "ECHO"));

                            insertCommand.ExecuteNonQuery();

                            // Reload the DataGridView in formReports
                            if (Owner is formReports formReports)
                            {
                                formReports.LoadDataIntoDataGridView();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
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





    }

}



