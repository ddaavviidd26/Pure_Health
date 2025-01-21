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
    public partial class CreateAcc : Form
    {
        public CreateAcc()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void CreateAcc_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                // Connection string - Replace placeholders with actual values
                string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";

                // Get inputs from textboxes
                string username = Username.Text.Trim();
                string password = Password.Text.Trim();
                string enteredCode = textBox1.Text.Trim();
                string UniqueCode = "Purehealth2025";

                // Validate unique code
                if (enteredCode != UniqueCode)
                {
                    MessageBox.Show("Invalid unique code!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate username and password inputs
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Username and password cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // SQL query to insert new user
                string query = "INSERT INTO dbo.Table_7 (Username, Password) VALUES (@Username, @Password)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Add parameters to prevent SQL Injection
                            command.Parameters.AddWithValue("@Username", username);
                            command.Parameters.AddWithValue("@Password", password); // Note: Use hashing for password security

                            int result = command.ExecuteNonQuery();

                            if (result > 0)
                            {
                                MessageBox.Show("Account created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close(); // Close the form after successful creation
                                
                                Form1 form1 = new Form1();
                                form1.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Failed to create account.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void label2_MouseLeave(object sender, EventArgs e)
        {

        }

        private void Password_MouseLeave(object sender, EventArgs e)
        {
            Password.PasswordChar = '●'; // Hide the password
        }
        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            Password.PasswordChar = '●'; // Hide the password
        }
        private void Password_MouseEnter(object sender, EventArgs e)
        {
            Password.PasswordChar = '\0'; // Reveal the password
        }
        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
