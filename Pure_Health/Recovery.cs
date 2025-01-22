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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Pure_Health
{
    public partial class Recovery : Form
    {
        public Recovery()
        {
            InitializeComponent();
        }

        private void Recovery_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Enter Email Address";
            textBox1.ForeColor = Color.Gray;

            textBox1.Enter += email_Enter;
            textBox1.Leave += email_Leave;
        }

        private void email_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Enter Email Address")
            {
                textBox1.Text = "";

            }
        }

        private void email_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Enter Email Address";

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=PC-MARKDAVID;Database=Purehealth;Trusted_Connection=True;";
            string email = textBox1.Text.Trim();

            // SQL query to check if email exists
            string query = "SELECT COUNT(1) FROM dbo.Table_7 WHERE Email = @Email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        int count = Convert.ToInt32(command.ExecuteScalar());

                        if (count == 1)
                        {
                            MessageBox.Show("Email confirmed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Pass the email to NewPasswordForm
                            NewPasswordForm newPasswordForm = new NewPasswordForm(email);
                            newPasswordForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Email not found in the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
