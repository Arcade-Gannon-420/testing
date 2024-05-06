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

namespace testing
{
    public partial class modifyAccount : UserControl
    {
        private string connectionString = @"Data Source=DESKTOP-0U6GMF4\SQLEXPRESS;Initial Catalog=testv2;Integrated Security=True";

        public modifyAccount()
        {
            InitializeComponent();

            txtUsername.Enabled = false;
            txtUserid.Enabled = false;
        }

        //MODIFY ACCOUNT BUTTON
        private void btnModify_Click(object sender, EventArgs e)
        {
            string userId = txtUserid.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string privilege = cmbPrivilege.SelectedItem.ToString();
            string firstname = txtFirstname.Text.Trim();
            string lastname = txtLastname.Text.Trim();

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname))
            {
                MessageBox.Show("Please enter all fields");
                return;
            }

            // Call the stored procedure to modify the account
            if (ModifyExistingAccount(userId,username, password, privilege, firstname, lastname))
            {
                MessageBox.Show("Account modified successfully!");
                // Clear the textboxes after successful modification
                ClearFields();
            }
            else
            {
                MessageBox.Show("Failed to modify account");
            }
        }


        //MODIFY ACCOUNT FUNCTION
        private bool ModifyExistingAccount(string userId, string username, string password, string privilege, string firstname, string lastname)
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
                        command.Parameters.AddWithValue("@Action", "Edit");

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

        //DELETE ACCOUNT BUTTON
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string userId = txtUserid.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string privilege = cmbPrivilege.SelectedItem.ToString();
            string firstname = txtFirstname.Text.Trim();
            string lastname = txtLastname.Text.Trim();

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname))
            {
                MessageBox.Show("Please enter all fields");
                return;
            }

            if (DeleteAccount(userId, username, password, privilege, firstname, lastname))
            {
                MessageBox.Show("Account Deleted successfully!");
                ClearFields();
            }
            else
            {
                MessageBox.Show("Failed to Delete account");
            }



        }

        //DELETING AN ACCOUNT
        private bool DeleteAccount(string userId, string username, string password, string privilege, string firstname, string lastname)
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

                        // UserId is of type int
                        command.Parameters.AddWithValue("@Action", "Delete");

                        // Remove the @Username parameter if it's not needed for the delete operation
                        connection.Open();                        
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }

        }

        //View All admin account


        //TO CLEAR INPUTS
        private void ClearFields()
        {
            txtIDsearch.Text = "";
            txtUserid.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            cmbPrivilege.SelectedIndex = 0;
            txtFirstname.Text = "";
            txtLastname.Text = "";
        }


        //AUTO POPULATE FEATURE
        private void btnSearch_Click(object sender, EventArgs e)
        {
            int userId;
            if (int.TryParse(txtIDsearch.Text, out userId))
            {
                GetUserDetails(userId);
            }
            else
            {
                MessageBox.Show("Please enter a valid UserId");
            }
        }
        private void GetUserDetails(int userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE UserId = @UserId", connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            txtUserid.Text = reader["UserId"].ToString();
                            txtFirstname.Text = reader["Firstname"].ToString();
                            txtLastname.Text = reader["Lastname"].ToString();
                            txtUsername.Text = reader["Username"].ToString();
                            txtPassword.Text = reader["Password"].ToString();
                            cmbPrivilege.SelectedItem = reader["Privilege"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("User not found");
                            ClearFields();
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


    }
}
