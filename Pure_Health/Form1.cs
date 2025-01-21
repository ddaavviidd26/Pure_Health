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
{
                // Connection string - Replace placeholders with actual values
                string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
                

                // Get input from textboxes
                string username = Username.Text.Trim();
            string password = Password.Text.Trim();

            // SQL query to check credentials
            string query = "SELECT COUNT(1) FROM dbo.Table_7 WHERE Username = @Username AND Password = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL Injection
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        int count = Convert.ToInt32(command.ExecuteScalar());

                        if (count == 1)
                        {
                            MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Navigate to the next form
                                Form3 form3 = new Form3();
                                form3.Show();
                                this.Hide();
                            }
                        else
                        {
                            MessageBox.Show("Invalid username or password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
        public bool AuthenticateUser(string username, out string specialization)
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string query = "SELECT Specialization FROM dbo.Table_3 WHERE UniqueID = @Username";

            specialization = null; // Initialize specialization

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar(); // Get the specialization value

                    if (result != null && result != DBNull.Value)
                    {
                        specialization = result.ToString(); // Assign the specialization
                        return true; // Username found
                    }
                    else
                    {
                        return false; // Username not found
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}");
                    return false;
                }
            }
        }

        private void password(object sender, EventArgs e)
        {

        }

        private void Password_MouseLeave_1(object sender, EventArgs e)
        {
            Password.PasswordChar = '●'; // Hide the password
        }

        private void Password_MouseEnter(object sender, EventArgs e)
        {
            Password.PasswordChar = '\0'; // Reveal the password
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            CreateAcc createAcc = new CreateAcc();
            createAcc.Show();
            this.Hide();
        }
    }
}
