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
            textBox1.Text = "Enter Email Address";
            textBox1.ForeColor = Color.Gray;

            textBox2.Text = "Enter Purehealth code";
            textBox2.ForeColor = Color.Gray;

            Password.Text = "Enter Password";
            Password.ForeColor = Color.Gray;

            Username.Text = "Enter Username";
            Username.ForeColor = Color.Gray;

            textBox2.Enter += textBox2_Enter;
            textBox2.Leave += textBox2_Leave;

            textBox1.Enter += textBox1_Enter;
            textBox1.Leave += textBox1_Leave;

            Password.Enter += Password_Enter;
            Password.Leave += Password_Leave;

            Username.Enter += Username_Enter;
            Username.Leave += Username_Leave;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                // Connection string - Replace placeholders with actual values
                string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";

                // Get inputs from textboxes
                string username = Username.Text.Trim();
                string password = Password.Text.Trim();
                string email = textBox1.Text.Trim();
                string enteredCode = textBox2.Text.Trim();
                string UniqueCode = "Purehealth2025";

                if (!email.Contains("@"))
                {
                    MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Exit the method if the email is not valid
                }

                // Validate unique code
                if (enteredCode != UniqueCode)
                {
                    MessageBox.Show("Invalid Purehealth code!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate username, password, and email inputs
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email))
                {
                    MessageBox.Show("Username, Password, and Email cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check if the password is at least 8 characters long
                if (password.Length < 8)
                {
                    MessageBox.Show("Password must be at least 8 characters long!", "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // SQL query to insert new user
                string query = "INSERT INTO dbo.Table_7 (Username, Password, Email) VALUES (@Username, @Password, @Email)";

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
                            command.Parameters.AddWithValue("@Email", email);

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
            
        }
        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            
        }
        private void Password_MouseEnter(object sender, EventArgs e)
        {
            
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

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Enter Email Address")
            {
                textBox1.Text = "";
                
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Enter Email Address";
                
            }
        }

        private void Username_TextChanged(object sender, EventArgs e)
        {

        }

        private void Password_Enter(object sender, EventArgs e)
        {
            if (Password.Text == "Enter Password")
            {
                Password.Text = "";
               
            }
        }

        private void Password_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Password.Text))
            {
                Password.Text = "Enter Password";
                
            }
        }

        private void Username_Enter(object sender, EventArgs e)
        {
            if (Username.Text == "Enter Username")
            {
                Username.Text = "";

            }
        }

        private void Username_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Username.Text))
            {
                Username.Text = "Enter Username";

            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Enter Purehealth code")
            {
                textBox2.Text = "";

            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.Text = "Enter Purehealth code";

            }
        }
    }
}
