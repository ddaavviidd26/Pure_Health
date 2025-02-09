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
    public partial class formArchive : Form
    {
        private string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
        public formArchive()
        {
            this.Paint += Form1_Paint;
            this.Load += formArchive_Load;          
            InitializeComponent();
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
        private void formArchive_Load(object sender, EventArgs e)
        {
            CustomizeSearchButton();
            CustomizeDataGridView();
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string query = "SELECT * FROM dbo.Table_5";
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

        private void CustomizeDataGridView()
        {
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
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
            if (dataGridView1.Columns.Contains("Id"))
                dataGridView1.Columns["Id"].Width = 30;

            if (dataGridView1.Columns.Contains("Age"))
                dataGridView1.Columns["Age"].Width = 40;

            if (dataGridView1.Columns.Contains("Price"))
                dataGridView1.Columns["Price"].Width = 59;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView();
        }

        public void LoadDataIntoDataGridView()
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string query = "SELECT * FROM dbo.Table_5"; // Adjust the query as needed

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable; // Replace myDataGridView with your actual DataGridView name
            }
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
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            LoadPatientData(searchTerm);
        }
        private void LoadPatientData(string searchTerm)
        {
            string query = "SELECT * FROM dbo.Table_5 WHERE 1=1";

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                if (int.TryParse(searchTerm, out _))
                {
                    query += " AND ID = @SearchTerm"; // Numeric ID search
                }
                else
                {
                    query += " AND ([Patient name] LIKE @SearchTerm OR Address LIKE @SearchTerm OR Gender LIKE @SearchTerm OR [Test to conduct] LIKE @SearchTerm OR Referral LIKE @SearchTerm)";
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
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
             string searchTerm = txtSearch.Text.Trim();
            LoadPatientData(searchTerm);
        }
    }
}
