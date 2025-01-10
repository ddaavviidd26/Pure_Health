using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pure_Health
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {         
                // Optional: Confirm exit
                var result = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Close the form
                    this.Close();
                }
            }

        private void button1_Click(object sender, EventArgs e)
        {

            string username = Username.Text;
            string password = Password.Text;

            if (AuthenticateUser(username, password))
            {
                MessageBox.Show("Login successful!");
                // Navigate to the next form or dashboard
                Form3 form3 = new Form3();
                form3.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Username_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public bool AuthenticateUser(string username, string password)
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string hashedPassword = "purehealth";

            string query = "SELECT COUNT(1) FROM dbo.Table_3 WHERE [Employees name] = @Username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                connection.Open();
                int result = Convert.ToInt32(command.ExecuteScalar());
                return result == 1;
            }
        }

    }
}
