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
using static Pure_Health.formAccounting;

namespace Pure_Health
{
    public partial class Createacc1 : Form
    {
        public event EventHandler AccountCreated;
        public Createacc1()
        {
            InitializeComponent();
        }

        private void Createacc1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Enter Email Address";
            textBox1.ForeColor = Color.Gray;

            Password.Text = "Enter Password";
            Password.ForeColor = Color.Gray;

            Username.Text = "Enter Username";
            Username.ForeColor = Color.Gray;

            textBox1.Enter += textBox1_Enter;
            textBox1.Leave += textBox1_Leave;

            Password.Enter += Password_Enter;
            Password.Leave += Password_Leave;

            Username.Enter += Username_Enter;
            Username.Leave += Username_Leave;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string username = Username.Text.Trim();
            string password = Password.Text.Trim();
            string email = textBox1.Text.Trim();

            if (!email.Contains("@"))
            {
                MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method if the email is not valid
            }
            // Prompt for Purehealth code
            string enteredCode = Prompt.ShowDialog("Enter Purehealth Code", "Authentication");

            // Replace with your actual Purehealth code
            const string purehealthCode = "Purehealth2025";

            if (enteredCode != purehealthCode)
            {
                MessageBox.Show("Invalid Purehealth Code. Account creation denied.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method if the code is invalid
            }

            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string query = "INSERT INTO dbo.Table_7 (Username, Password, Email) VALUES (@Username, @Password, @Email)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@Email", email);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Account created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Close the form and return DialogResult.OK to indicate success
                this.DialogResult = DialogResult.OK;
                this.Close();
                
            }
            catch (Exception ex)
            {
                // Handle exceptions gracefully
                MessageBox.Show("An error occurred while creating the account: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static class Prompt
        {
            public static string ShowDialog(string text, string caption)
            {
                Form prompt = new Form
                {
                    Width = 400,
                    Height = 200,
                    FormBorderStyle = FormBorderStyle.FixedSingle,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen,
                    BackColor = Color.White,
                    Font = new Font("Cambria", 10),
                };

                Label textLabel = new Label
                {
                    Left = 20,
                    Top = 20,
                    Width = 350,
                    Text = text,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Cambria", 10, FontStyle.Regular),
                };

                TextBox inputBox = new TextBox
                {
                    Left = 20,
                    Top = 60,
                    Width = 350,
                    UseSystemPasswordChar = true,
                    BorderStyle = BorderStyle.FixedSingle,
                    Font = new Font("Cambria", 10),
                };

                Button confirmation = new Button
                {
                    Text = "OK",
                    Left = 270,
                    Width = 100,
                    Top = 120,
                    BackColor = Color.FromArgb(41, 128, 185),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Cambria", 10),
                };
                confirmation.FlatAppearance.BorderSize = 0;

                confirmation.Click += (sender, e) => { prompt.DialogResult = DialogResult.OK; prompt.Close(); };

                Button cancel = new Button
                {
                    Text = "Cancel",
                    Left = 150,
                    Width = 100,
                    Top = 120,
                    BackColor = Color.FromArgb(192, 57, 43),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Cambria", 10),
                };
                cancel.FlatAppearance.BorderSize = 0;

                cancel.Click += (sender, e) => { prompt.DialogResult = DialogResult.Cancel; prompt.Close(); };

                // Add controls to the form
                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(inputBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(cancel);

                // Focus on the input box by default
                inputBox.Focus();

                return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : string.Empty;
            }
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

        }

        private void textBox2_Leave(object sender, EventArgs e)
        {

        }

    }
}
