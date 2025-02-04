using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Pure_Health
{
    public partial class NewPasswordForm : Form
    {
        private string _email;
        public NewPasswordForm(string email)
        {
            InitializeComponent();
            _email = email;
        }
        private void NewPasswordForm_Load(object sender, EventArgs e)
        {
            // Set initial placeholder text and color for each TextBox
            
            this.ActiveControl = button1; // Replace 'label1' with any control that is not an input box
            // Attach event handlers for placeholders
            newPass.Enter += (s, ev) => RemovePlaceholder(newPass, "Enter new password");
            newPass.Leave += (s, ev) => SetPlaceholder(newPass, "Enter new password");

            textBox1.Enter += (s, ev) => RemovePlaceholder(textBox1, "Confirm password");
            textBox1.Leave += (s, ev) => SetPlaceholder(textBox1, "Confirm password");

            textBox3.Enter += (s, ev) => RemovePlaceholder(textBox3, "Enter purehealth code");
            textBox3.Leave += (s, ev) => SetPlaceholder(textBox3, "Enter purehealth code");


        }
        private void SetPlaceholder(TextBox textBox, string placeholder)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = placeholder;
                textBox.ForeColor = Color.Gray; // Use a gray color for placeholder text
            }

        }
        private void RemovePlaceholder(TextBox textBox, string placeholder)
        {
            if (textBox.Text == placeholder)
            {
                textBox.Text = string.Empty;
                textBox.ForeColor = Color.Black; // Reset to normal text color
            }
        }

        private void newPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void newPassword_Enter(object sender, EventArgs e)
        {
           
        }

        private void newPassword_Leave(object sender, EventArgs e)
        {
            
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
           
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string newPassword = newPass.Text.Trim();
            string confirmPassword = textBox1.Text.Trim();
            string purehealthCode = textBox3.Text.Trim();

            // Validate input
            if (newPassword.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long!", "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (confirmPassword.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long!", "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(purehealthCode))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords do not match. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate the Purehealth Code and update the password
            if (purehealthCode != "Purehealth2025") // Replace with actual validation logic if needed
            {
                MessageBox.Show("Invalid Purehealth Code. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            

            string query = @"
           UPDATE dbo.Table_7 
           SET Password = @Password 
           WHERE Email = @Email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters
                        command.Parameters.AddWithValue("@Password", newPassword);
                        command.Parameters.AddWithValue("@Email", _email);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Password updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();

                            Form1 form1 = new Form1();
                            form1.Show();
                            this.Hide();


                        }
                        else
                        {
                            MessageBox.Show("Email not found. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form1 form1= new Form1();
            form1.Show();
            this.Hide();
        }
    }
 }
