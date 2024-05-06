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

namespace testing
{
    public partial class registerAccount : UserControl
    {
        private string connectionString = @"Data Source=DESKTOP-0U6GMF4\SQLEXPRESS;Initial Catalog=testv2;Integrated Security=True";

        public registerAccount()
        {
            InitializeComponent();

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            int userId;
            if (!int.TryParse(txtUserid.Text.Trim(), out userId))
            {
                MessageBox.Show("UserId must be a valid ID Number.");
                return;
            }

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string privilege = cmbPrivilege.SelectedItem?.ToString();
            string firstname = txtFirstname.Text.Trim();
            string lastname = txtLastname.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(privilege) || string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname))
            {
                MessageBox.Show("Please enter all fields");
                return;
            }

            // Call the stored procedure to register the account
            if (RegisterNewAccount(userId, username, password, privilege, firstname, lastname))
            {
                MessageBox.Show("Account registered successfully!");
                // Clear the textboxes after successful registration
                ClearFields();
            }
            else
            {
                MessageBox.Show("Failed to register account");
            }
        }

        private bool RegisterNewAccount(int userId, string username, string password, string privilege, string firstname, string lastname)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("ManageUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@Privilege", privilege);
                        command.Parameters.AddWithValue("@Firstname", firstname);
                        command.Parameters.AddWithValue("@Lastname", lastname);
                        command.Parameters.AddWithValue("@Action", "Register");

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (SqlException ex)
            {
                // Check if the exception is due to a duplicate username
                if (ex.Number == 2627) // SQL Server error code for unique constraint violation
                {
                    MessageBox.Show("Username already exists. Please choose a different username.");
                }
                else
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                return false;
            }
        }

        private void ClearFields()
        {
            txtUserid.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtFirstname.Text = "";
            txtLastname.Text = "";
            cmbPrivilege.Text = "";
        }


    }
 }
